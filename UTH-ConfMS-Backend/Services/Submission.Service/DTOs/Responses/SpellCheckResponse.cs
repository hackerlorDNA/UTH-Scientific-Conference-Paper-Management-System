namespace Submission.Service.DTOs.Responses;

/// <summary>
/// Response for spell check
/// </summary>
public class SpellCheckResponse
{
    /// <summary>
    /// List of spelling/grammar errors found
    /// </summary>
    public List<SpellingErrorDto> Errors { get; set; } = new();
    
    /// <summary>
    /// Total number of errors found
    /// </summary>
    public int TotalErrors { get; set; }
    
    /// <summary>
    /// Whether the text has no errors
    /// </summary>
    public bool IsClean { get; set; }
    
    /// <summary>
    /// Error message if service failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Details of a spelling/grammar error
/// </summary>
public class SpellingErrorDto
{
    /// <summary>
    /// Error description message
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Context around the error
    /// </summary>
    public string Context { get; set; } = string.Empty;
    
    /// <summary>
    /// Character offset where error starts
    /// </summary>
    public int Offset { get; set; }
    
    /// <summary>
    /// Length of the erroneous text
    /// </summary>
    public int Length { get; set; }
    
    /// <summary>
    /// Suggested corrections
    /// </summary>
    public List<string> Suggestions { get; set; } = new();
    
    /// <summary>
    /// Rule ID that triggered this error
    /// </summary>
    public string RuleId { get; set; } = string.Empty;
    
    /// <summary>
    /// Error category (spelling, grammar, style, etc.)
    /// </summary>
    public string Category { get; set; } = string.Empty;
}
