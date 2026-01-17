namespace Submission.Service.DTOs.Responses;

/// <summary>
/// Response for plagiarism check
/// </summary>
public class PlagiarismResponse
{
    /// <summary>
    /// Submission ID that was checked
    /// </summary>
    public int SubmissionId { get; set; }
    
    /// <summary>
    /// Whether plagiarism check was completed
    /// </summary>
    public bool IsPlagiarismChecked { get; set; }
    
    /// <summary>
    /// Overall similarity score against existing submissions (0-100%)
    /// </summary>
    public double OverallSimilarityScore { get; set; }
    
    /// <summary>
    /// When the check was performed
    /// </summary>
    public DateTime CheckedAt { get; set; }
    
    /// <summary>
    /// Status message
    /// </summary>
    public string Status { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed matches found
    /// </summary>
    public List<PlagiarismMatchDto> Details { get; set; } = new();
}

/// <summary>
/// Details of a plagiarism match
/// </summary>
public class PlagiarismMatchDto
{
    /// <summary>
    /// ID of the matched submission
    /// </summary>
    public int MatchedSubmissionId { get; set; }
    
    /// <summary>
    /// Title of the matched submission
    /// </summary>
    public string MatchedTitle { get; set; } = string.Empty;
    
    /// <summary>
    /// Similarity score with this submission
    /// </summary>
    public double SimilarityScore { get; set; }
    
    /// <summary>
    /// Matching text excerpt
    /// </summary>
    public string MatchedText { get; set; } = string.Empty;
}
