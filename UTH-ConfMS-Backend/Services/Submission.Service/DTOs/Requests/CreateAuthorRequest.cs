namespace Submission.Service.DTOs.Requests;

public record CreateAuthorRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Affiliation { get; set; }
    public int OrderIndex { get; set; }
    public bool IsCorresponding { get; set; }
}
