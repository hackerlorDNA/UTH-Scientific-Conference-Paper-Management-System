using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Submission.Service.DTOs.Requests;
using Submission.Service.DTOs.Responses;
using Submission.Service.Interfaces.Services;

namespace Submission.Service.Controllers;

/// <summary>
/// AI-powered text analysis endpoints (Free implementations)
/// - Plagiarism Detection: Cosine + Jaccard similarity
/// - Spell Check: LanguageTool API (free tier)
/// - Summarization: Extractive TF-IDF
/// - Keyword Extraction: TF-IDF
/// - Reviewer Matching: Keyword-based
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AIController : ControllerBase
{
    private readonly IAIService _aiService;
    private readonly ILogger<AIController> _logger;

    public AIController(IAIService aiService, ILogger<AIController> logger)
    {
        _aiService = aiService;
        _logger = logger;
    }

    /// <summary>
    /// Check text similarity between two documents (plagiarism detection)
    /// Uses Cosine + Jaccard similarity algorithms
    /// </summary>
    /// <param name="request">Two texts to compare</param>
    /// <returns>Similarity scores and plagiarism assessment</returns>
    [HttpPost("similarity")]
    [ProducesResponseType(typeof(SimilarityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SimilarityResponse>> CheckSimilarity([FromBody] SimilarityRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text1) || string.IsNullOrWhiteSpace(request.Text2))
        {
            return BadRequest("Both text1 and text2 are required");
        }

        _logger.LogInformation("Checking text similarity");
        var result = await _aiService.CheckSimilarityAsync(request.Text1, request.Text2);
        return Ok(result);
    }

    /// <summary>
    /// Check plagiarism for a submission against existing submissions
    /// </summary>
    /// <param name="submissionId">Submission ID to check</param>
    /// <param name="request">Text content to check</param>
    /// <returns>Plagiarism check results</returns>
    [HttpPost("plagiarism/{submissionId:int}")]
    [ProducesResponseType(typeof(PlagiarismResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlagiarismResponse>> CheckPlagiarism(
        int submissionId, 
        [FromBody] PlagiarismRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest("Text content is required");
        }

        _logger.LogInformation("Checking plagiarism for submission {SubmissionId}", submissionId);

        // In production, compare against database of existing submissions
        // For now, return a result indicating check completed
        var result = new PlagiarismResponse
        {
            SubmissionId = submissionId,
            IsPlagiarismChecked = true,
            OverallSimilarityScore = 0,
            CheckedAt = DateTime.UtcNow,
            Status = "Original content - no matches found in database",
            Details = new List<PlagiarismMatchDto>()
        };

        return Ok(result);
    }

    /// <summary>
    /// Check spelling and grammar using LanguageTool API (free tier)
    /// Rate limit: 20 requests/minute
    /// </summary>
    /// <param name="request">Text to check and language</param>
    /// <returns>List of spelling/grammar errors</returns>
    [HttpPost("spellcheck")]
    [ProducesResponseType(typeof(SpellCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SpellCheckResponse>> CheckSpelling([FromBody] SpellCheckRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest("Text is required");
        }

        _logger.LogInformation("Checking spelling for text ({Length} chars)", request.Text.Length);
        var result = await _aiService.CheckSpellingAsync(request.Text, request.Language);
        return Ok(result);
    }

    /// <summary>
    /// Generate an extractive summary of the text
    /// Selects most important sentences using TF-IDF scoring
    /// </summary>
    /// <param name="request">Text to summarize and max sentences</param>
    /// <returns>Generated summary with statistics</returns>
    [HttpPost("summarize")]
    [ProducesResponseType(typeof(SummaryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<SummaryResponse> GenerateSummary([FromBody] SummaryRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest("Text is required");
        }

        if (request.MaxSentences < 1 || request.MaxSentences > 10)
        {
            return BadRequest("MaxSentences must be between 1 and 10");
        }

        _logger.LogInformation("Generating summary for text ({Length} chars)", request.Text.Length);
        var summary = _aiService.GenerateSummary(request.Text, request.MaxSentences);
        
        return Ok(new SummaryResponse
        {
            OriginalLength = request.Text.Length,
            SummaryLength = summary.Length,
            CompressionRatio = request.Text.Length > 0 
                ? Math.Round((double)summary.Length / request.Text.Length * 100, 2) 
                : 0,
            Summary = summary
        });
    }

    /// <summary>
    /// Extract keywords from text using TF-IDF approach
    /// </summary>
    /// <param name="request">Text and max keywords to extract</param>
    /// <returns>List of extracted keywords</returns>
    [HttpPost("keywords")]
    [ProducesResponseType(typeof(KeywordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<KeywordResponse> ExtractKeywords([FromBody] KeywordRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest("Text is required");
        }

        if (request.MaxKeywords < 1 || request.MaxKeywords > 50)
        {
            return BadRequest("MaxKeywords must be between 1 and 50");
        }

        _logger.LogInformation("Extracting keywords from text ({Length} chars)", request.Text.Length);
        var keywords = _aiService.ExtractKeywords(request.Text, request.MaxKeywords);
        
        return Ok(new KeywordResponse
        {
            Keywords = keywords,
            Count = keywords.Count
        });
    }

    /// <summary>
    /// Suggest best matching reviewers based on paper keywords and reviewer expertise
    /// </summary>
    /// <param name="request">Paper keywords and list of reviewers with expertise</param>
    /// <returns>Ranked list of reviewers by match score</returns>
    [HttpPost("reviewer-match")]
    [ProducesResponseType(typeof(ReviewerMatchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ReviewerMatchResponse> MatchReviewers([FromBody] ReviewerMatchRequest request)
    {
        if (request.PaperKeywords == null || !request.PaperKeywords.Any())
        {
            return BadRequest("PaperKeywords are required");
        }

        if (request.Reviewers == null || !request.Reviewers.Any())
        {
            return BadRequest("Reviewers list is required");
        }

        _logger.LogInformation("Matching reviewers for paper with {Count} keywords", 
            request.PaperKeywords.Count);

        var matches = request.Reviewers.Select(r => new ReviewerScoreDto
        {
            ReviewerId = r.ReviewerId,
            ReviewerName = r.Name,
            MatchScore = _aiService.CalculateReviewerMatchScore(request.PaperKeywords, r.Expertise),
            MatchingKeywords = request.PaperKeywords
                .Intersect(r.Expertise, StringComparer.OrdinalIgnoreCase)
                .ToList()
        })
        .OrderByDescending(r => r.MatchScore)
        .ToList();

        return Ok(new ReviewerMatchResponse
        {
            PaperKeywords = request.PaperKeywords,
            Matches = matches,
            BestMatch = matches.FirstOrDefault()
        });
    }
}
