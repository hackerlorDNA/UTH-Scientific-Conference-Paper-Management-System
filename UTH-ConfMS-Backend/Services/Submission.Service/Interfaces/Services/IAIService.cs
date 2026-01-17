using Submission.Service.DTOs.Responses;

namespace Submission.Service.Interfaces.Services;

/// <summary>
/// AI Service interface for text analysis (free implementations)
/// </summary>
public interface IAIService
{
    /// <summary>
    /// Check text similarity between two documents (plagiarism detection)
    /// Uses Cosine + Jaccard similarity algorithms
    /// </summary>
    Task<SimilarityResponse> CheckSimilarityAsync(string text1, string text2);
    
    /// <summary>
    /// Check spelling errors using LanguageTool API (free tier)
    /// Rate limit: 20 requests/minute
    /// </summary>
    Task<SpellCheckResponse> CheckSpellingAsync(string text, string language = "en-US");
    
    /// <summary>
    /// Generate extractive summary from text using TF-IDF scoring
    /// </summary>
    string GenerateSummary(string text, int maxSentences = 3);
    
    /// <summary>
    /// Extract keywords from text using TF-IDF approach
    /// </summary>
    List<string> ExtractKeywords(string text, int maxKeywords = 10);
    
    /// <summary>
    /// Calculate match score between paper keywords and reviewer expertise
    /// </summary>
    double CalculateReviewerMatchScore(List<string> paperKeywords, List<string> reviewerExpertise);
}
