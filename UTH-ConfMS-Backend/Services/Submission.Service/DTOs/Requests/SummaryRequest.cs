namespace Submission.Service.DTOs.Requests;

/// <summary>
/// Request for text summarization
/// </summary>
public class SummaryRequest
{
    /// <summary>
    /// Text content to summarize
    /// </summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// Maximum number of sentences in summary (1-10)
    /// Default: 3
    /// </summary>
    public int MaxSentences { get; set; } = 3;
}
