namespace Conference.Service.DTOs.Responses;

public class CallForPapersDto
{
    public Guid CfpId { get; set; }
    public Guid ConferenceId { get; set; }
    public string? Description { get; set; }
    public string? Guidelines { get; set; }
    public List<string> Topics { get; set; } = new();
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
}
