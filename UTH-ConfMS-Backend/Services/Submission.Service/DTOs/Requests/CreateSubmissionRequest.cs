using Microsoft.AspNetCore.Http;

namespace Submission.Service.DTOs.Requests;

public record CreateSubmissionRequest
{
    public Guid ConferenceId { get; set; }
    public Guid? TrackId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Abstract { get; set; } = string.Empty;
    public List<string> Keywords { get; set; } = new();
    public string PaperType { get; set; } = "FULL_PAPER";
    public Guid? PrimaryTopicId { get; set; }
    public IFormFile? File { get; set; }
    public List<CreateAuthorRequest> Authors { get; set; } = new();
}
