namespace Submission.Service.DTOs.Requests;

/// <summary>
/// Request for spell check
/// </summary>
public class SpellCheckRequest
{
    /// <summary>
    /// Text content to check spelling
    /// </summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// Language code (e.g., "en-US", "vi-VN")
    /// Default: "en-US"
    /// </summary>
    public string Language { get; set; } = "en-US";
}
