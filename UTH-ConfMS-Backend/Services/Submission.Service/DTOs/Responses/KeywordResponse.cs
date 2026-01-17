namespace Submission.Service.DTOs.Responses;

/// <summary>
/// Response for keyword extraction
/// </summary>
public class KeywordResponse
{
    /// <summary>
    /// Extracted keywords
    /// </summary>
    public List<string> Keywords { get; set; } = new();
    
    /// <summary>
    /// Number of keywords extracted
    /// </summary>
    public int Count { get; set; }
}
