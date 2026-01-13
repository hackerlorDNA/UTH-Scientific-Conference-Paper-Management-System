namespace Conference.Service.DTOs.Requests;

public class UpdateCallForPapersRequest
{
    public string? Description { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Guidelines { get; set; }
    public string? SubmissionGuidelines { get; set; }
    public string? FormattingRequirements { get; set; }
    public int? MinPages { get; set; }
    public int? MaxPages { get; set; }
    public bool? IsPublished { get; set; }
    public List<string>? Topics { get; set; }
}
