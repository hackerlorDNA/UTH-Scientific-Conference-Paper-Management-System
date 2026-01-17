namespace Submission.Service.DTOs.Requests;

/// <summary>
/// Request for text similarity check
/// </summary>
public class SimilarityRequest
{
    /// <summary>
    /// First text to compare
    /// </summary>
    public string Text1 { get; set; } = string.Empty;
    
    /// <summary>
    /// Second text to compare
    /// </summary>
    public string Text2 { get; set; } = string.Empty;
}
