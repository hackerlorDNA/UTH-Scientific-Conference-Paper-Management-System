namespace Submission.Service.DTOs.Requests;

/// <summary>
/// Request for plagiarism check
/// </summary>
public class PlagiarismRequest
{
    /// <summary>
    /// Text content to check for plagiarism
    /// </summary>
    public string Text { get; set; } = string.Empty;
}
