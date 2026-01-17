namespace Submission.Service.DTOs.Responses;

/// <summary>
/// Response for reviewer matching
/// </summary>
public class ReviewerMatchResponse
{
    /// <summary>
    /// Keywords from the paper
    /// </summary>
    public List<string> PaperKeywords { get; set; } = new();
    
    /// <summary>
    /// Sorted list of reviewers with match scores
    /// </summary>
    public List<ReviewerScoreDto> Matches { get; set; } = new();
    
    /// <summary>
    /// Best matching reviewer
    /// </summary>
    public ReviewerScoreDto? BestMatch { get; set; }
}

/// <summary>
/// Reviewer match score details
/// </summary>
public class ReviewerScoreDto
{
    /// <summary>
    /// Reviewer's user ID
    /// </summary>
    public int ReviewerId { get; set; }
    
    /// <summary>
    /// Reviewer's name
    /// </summary>
    public string ReviewerName { get; set; } = string.Empty;
    
    /// <summary>
    /// Match score (0-100%)
    /// </summary>
    public double MatchScore { get; set; }
    
    /// <summary>
    /// Keywords that match between paper and reviewer
    /// </summary>
    public List<string> MatchingKeywords { get; set; } = new();
}
