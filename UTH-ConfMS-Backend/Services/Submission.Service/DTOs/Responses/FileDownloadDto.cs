namespace Submission.Service.DTOs.Responses;

public record FileDownloadDto(
    byte[] Content,
    string ContentType,
    string FileName
);
