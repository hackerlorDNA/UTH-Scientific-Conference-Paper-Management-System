using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Submission.Service.DTOs.Responses;
using Submission.Service.Interfaces.Services;

namespace Submission.Service.Services;

/// <summary>
/// AI Service implementation for text analysis (free implementations)
/// - Plagiarism: Cosine + Jaccard similarity
/// - Spell Check: LanguageTool API (free tier)
/// - Summary: Extractive summarization with TF-IDF
/// - Keywords: TF-IDF extraction
/// - Reviewer Matching: Keyword-based matching
/// </summary>
public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AIService> _logger;
    
    // Common English stop words to filter out
    private static readonly HashSet<string> StopWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "a", "an", "the", "and", "or", "but", "in", "on", "at", "to", "for", "of", "with",
        "by", "from", "as", "is", "was", "are", "were", "been", "be", "have", "has", "had",
        "do", "does", "did", "will", "would", "could", "should", "may", "might", "must",
        "shall", "can", "need", "dare", "ought", "used", "it", "its", "this", "that",
        "these", "those", "i", "you", "he", "she", "we", "they", "what", "which", "who",
        "when", "where", "why", "how", "all", "each", "every", "both", "few", "more",
        "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so",
        "than", "too", "very", "just", "also", "now", "here", "there", "then", "once"
    };

    public AIService(HttpClient httpClient, ILogger<AIService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    #region Plagiarism Check (Text Similarity)
    
    /// <inheritdoc />
    public Task<SimilarityResponse> CheckSimilarityAsync(string text1, string text2)
    {
        if (string.IsNullOrWhiteSpace(text1) || string.IsNullOrWhiteSpace(text2))
        {
            return Task.FromResult(new SimilarityResponse 
            { 
                SimilarityScore = 0, 
                IsPlagiarized = false,
                Message = "One or both texts are empty"
            });
        }

        // Tokenize and normalize
        var words1 = Tokenize(text1);
        var words2 = Tokenize(text2);

        // Calculate Cosine Similarity
        var cosineSimilarity = CalculateCosineSimilarity(words1, words2);
        
        // Calculate Jaccard Similarity for additional check
        var jaccardSimilarity = CalculateJaccardSimilarity(words1, words2);
        
        // Average both scores
        var averageScore = (cosineSimilarity + jaccardSimilarity) / 2;
        
        var result = new SimilarityResponse
        {
            SimilarityScore = Math.Round(averageScore * 100, 2),
            CosineSimilarity = Math.Round(cosineSimilarity * 100, 2),
            JaccardSimilarity = Math.Round(jaccardSimilarity * 100, 2),
            IsPlagiarized = averageScore > 0.7, // >70% is considered plagiarism
            Message = GetSimilarityMessage(averageScore)
        };

        _logger.LogInformation("Similarity check completed: {Score}%", result.SimilarityScore);
        
        return Task.FromResult(result);
    }

    private List<string> Tokenize(string text)
    {
        // Remove special characters and split into words
        var cleanText = Regex.Replace(text.ToLower(), @"[^\w\s]", " ");
        return cleanText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                       .Where(w => w.Length > 2 && !StopWords.Contains(w))
                       .ToList();
    }

    private double CalculateCosineSimilarity(List<string> words1, List<string> words2)
    {
        var allWords = words1.Union(words2).Distinct().ToList();
        
        var vector1 = allWords.Select(w => (double)words1.Count(x => x == w)).ToArray();
        var vector2 = allWords.Select(w => (double)words2.Count(x => x == w)).ToArray();
        
        var dotProduct = vector1.Zip(vector2, (a, b) => a * b).Sum();
        var magnitude1 = Math.Sqrt(vector1.Sum(x => x * x));
        var magnitude2 = Math.Sqrt(vector2.Sum(x => x * x));
        
        if (magnitude1 == 0 || magnitude2 == 0) return 0;
        
        return dotProduct / (magnitude1 * magnitude2);
    }

    private double CalculateJaccardSimilarity(List<string> words1, List<string> words2)
    {
        var set1 = new HashSet<string>(words1);
        var set2 = new HashSet<string>(words2);
        
        var intersection = set1.Intersect(set2).Count();
        var union = set1.Union(set2).Count();
        
        return union == 0 ? 0 : (double)intersection / union;
    }

    private string GetSimilarityMessage(double score)
    {
        return score switch
        {
            > 0.9 => "Very high similarity - likely plagiarism",
            > 0.7 => "High similarity - needs review",
            > 0.5 => "Moderate similarity - some overlap detected",
            > 0.3 => "Low similarity - minor overlap",
            _ => "Original content"
        };
    }
    
    #endregion

    #region Spell Check (LanguageTool API - Free)
    
    /// <inheritdoc />
    public async Task<SpellCheckResponse> CheckSpellingAsync(string text, string language = "en-US")
    {
        var result = new SpellCheckResponse { Errors = new List<SpellingErrorDto>() };
        
        try
        {
            // LanguageTool public API (free tier: 20 requests/minute)
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("language", language)
            });

            var response = await _httpClient.PostAsync(
                "https://api.languagetool.org/v2/check", 
                content
            );

            if (response.IsSuccessStatusCode)
            {
                var ltResult = await response.Content.ReadFromJsonAsync<LanguageToolResponse>();
                
                if (ltResult?.Matches != null)
                {
                    result.Errors = ltResult.Matches.Select(m => new SpellingErrorDto
                    {
                        Message = m.Message,
                        Context = m.Context?.Text ?? "",
                        Offset = m.Offset,
                        Length = m.Length,
                        Suggestions = m.Replacements?.Take(3).Select(r => r.Value).ToList() ?? new List<string>(),
                        RuleId = m.Rule?.Id ?? "",
                        Category = m.Rule?.Category?.Name ?? "Unknown"
                    }).ToList();
                }
            }
            
            result.TotalErrors = result.Errors.Count;
            result.IsClean = result.TotalErrors == 0;
            
            _logger.LogInformation("Spell check completed: {ErrorCount} errors found", result.TotalErrors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Spell check failed");
            result.ErrorMessage = "Spell check service unavailable";
        }
        
        return result;
    }
    
    #endregion

    #region Text Summarization (Extractive)
    
    /// <inheritdoc />
    public string GenerateSummary(string text, int maxSentences = 3)
    {
        if (string.IsNullOrWhiteSpace(text)) return "";
        
        // Split into sentences
        var sentences = Regex.Split(text, @"(?<=[.!?])\s+")
                            .Where(s => s.Length > 20)
                            .ToList();
        
        if (sentences.Count <= maxSentences)
            return text;
        
        // Calculate word frequencies (TF)
        var allWords = sentences.SelectMany(s => Tokenize(s)).ToList();
        var wordFreq = allWords.GroupBy(w => w)
                              .ToDictionary(g => g.Key, g => (double)g.Count() / allWords.Count);
        
        // Score each sentence based on word importance
        var scoredSentences = sentences.Select((sentence, index) => new
        {
            Index = index,
            Sentence = sentence,
            Score = CalculateSentenceScore(sentence, wordFreq)
        })
        .OrderByDescending(s => s.Score)
        .Take(maxSentences)
        .OrderBy(s => s.Index) // Maintain original order
        .Select(s => s.Sentence);
        
        var summary = string.Join(" ", scoredSentences);
        
        _logger.LogInformation("Generated summary: {Length} chars from {Original} chars", 
            summary.Length, text.Length);
        
        return summary;
    }

    private double CalculateSentenceScore(string sentence, Dictionary<string, double> wordFreq)
    {
        var words = Tokenize(sentence);
        if (words.Count == 0) return 0;
        
        return words.Where(w => wordFreq.ContainsKey(w))
                   .Sum(w => wordFreq[w]) / words.Count;
    }
    
    #endregion

    #region Keyword Extraction
    
    /// <inheritdoc />
    public List<string> ExtractKeywords(string text, int maxKeywords = 10)
    {
        if (string.IsNullOrWhiteSpace(text)) return new List<string>();
        
        var words = Tokenize(text);
        
        // Calculate term frequency
        var wordCounts = words.GroupBy(w => w)
                             .ToDictionary(g => g.Key, g => g.Count());
        
        // Filter and sort by frequency
        var keywords = wordCounts
            .Where(kv => kv.Key.Length > 3) // At least 4 characters
            .OrderByDescending(kv => kv.Value)
            .Take(maxKeywords)
            .Select(kv => kv.Key)
            .ToList();
        
        _logger.LogInformation("Extracted {Count} keywords", keywords.Count);
        
        return keywords;
    }
    
    #endregion

    #region Reviewer Matching
    
    /// <inheritdoc />
    public double CalculateReviewerMatchScore(List<string> paperKeywords, List<string> reviewerExpertise)
    {
        if (!paperKeywords.Any() || !reviewerExpertise.Any()) return 0;
        
        // Normalize to lowercase
        var normalizedPaper = paperKeywords.Select(k => k.ToLower()).ToHashSet();
        var normalizedExpertise = reviewerExpertise.Select(k => k.ToLower()).ToHashSet();
        
        // Calculate Jaccard similarity
        var intersection = normalizedPaper.Intersect(normalizedExpertise).Count();
        var union = normalizedPaper.Union(normalizedExpertise).Count();
        
        var score = union == 0 ? 0 : (double)intersection / union * 100;
        
        _logger.LogDebug("Reviewer match score: {Score}%", Math.Round(score, 2));
        
        return Math.Round(score, 2);
    }
    
    #endregion
}

#region LanguageTool API Response Models

internal class LanguageToolResponse
{
    public List<LTMatch>? Matches { get; set; }
}

internal class LTMatch
{
    public string Message { get; set; } = "";
    public int Offset { get; set; }
    public int Length { get; set; }
    public LTContext? Context { get; set; }
    public List<LTReplacement>? Replacements { get; set; }
    public LTRule? Rule { get; set; }
}

internal class LTContext
{
    public string Text { get; set; } = "";
}

internal class LTReplacement
{
    public string Value { get; set; } = "";
}

internal class LTRule
{
    public string Id { get; set; } = "";
    public LTCategory? Category { get; set; }
}

internal class LTCategory
{
    public string Name { get; set; } = "";
}

#endregion
