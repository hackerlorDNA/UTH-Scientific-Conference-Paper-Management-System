namespace Submission.Service.DTOs.Responses;

public record SubmissionFileDto(
    Guid FileId,
    string FileName,
    string OriginalFilename,
    string FilePath,
    long FileSizeBytes,
    string MimeType,
    int Version,
    DateTime UploadedAt
);
