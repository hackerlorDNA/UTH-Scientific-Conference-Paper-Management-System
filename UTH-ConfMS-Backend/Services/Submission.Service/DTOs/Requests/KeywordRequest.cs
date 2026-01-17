namespace Submission.Service.DTOs.Requests;

/// <summary>
/// Request for keyword extraction
/// </summary>
public class KeywordRequest
{
    /// <summary>
    /// Text content to extract keywords from
    /// </summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// Maximum number of keywords to extract (1-50)
    /// Default: 10
    /// </summary>
    public int MaxKeywords { get; set; } = 10;
}
