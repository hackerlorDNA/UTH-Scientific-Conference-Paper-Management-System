using Microsoft.AspNetCore.Http;

namespace Submission.Service.DTOs.Requests;

public record UploadFileRequest
{
    public IFormFile File { get; set; } = null!;
    public string FileType { get; set; } = "PAPER";
    public int Version { get; set; } = 1;
}
