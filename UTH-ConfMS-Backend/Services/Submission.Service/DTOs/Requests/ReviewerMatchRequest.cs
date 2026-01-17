namespace Submission.Service.DTOs.Requests;

/// <summary>
/// Request for reviewer matching
/// </summary>
public class ReviewerMatchRequest
{
    /// <summary>
    /// Keywords from the paper
    /// </summary>
    public List<string> PaperKeywords { get; set; } = new();
    
    /// <summary>
    /// List of reviewers with their expertise
    /// </summary>
    public List<ReviewerInfoDto> Reviewers { get; set; } = new();
}

/// <summary>
/// Reviewer information for matching
/// </summary>
public class ReviewerInfoDto
{
    /// <summary>
    /// Reviewer's user ID
    /// </summary>
    public int ReviewerId { get; set; }
    
    /// <summary>
    /// Reviewer's name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Reviewer's areas of expertise (keywords)
    /// </summary>
    public List<string> Expertise { get; set; } = new();
}
