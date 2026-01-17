namespace Submission.Service.DTOs.Responses;

/// <summary>
/// Response for text summarization
/// </summary>
public class SummaryResponse
{
    /// <summary>
    /// Original text length in characters
    /// </summary>
    public int OriginalLength { get; set; }
    
    /// <summary>
    /// Summary text length in characters
    /// </summary>
    public int SummaryLength { get; set; }
    
    /// <summary>
    /// Compression ratio (summary/original * 100)
    /// </summary>
    public double CompressionRatio { get; set; }
    
    /// <summary>
    /// Generated summary text
    /// </summary>
    public string Summary { get; set; } = string.Empty;
}
