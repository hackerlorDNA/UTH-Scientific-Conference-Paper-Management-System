namespace Submission.Service.DTOs.Requests;

public record CreateSubmissionFileRequest
{
    public string FileType { get; set; } = "PAPER";
    public string FileName { get; set; } = string.Empty;
    public string OriginalFilename { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public int Version { get; set; } = 1;
    public Guid UploadedBy { get; set; }
}
