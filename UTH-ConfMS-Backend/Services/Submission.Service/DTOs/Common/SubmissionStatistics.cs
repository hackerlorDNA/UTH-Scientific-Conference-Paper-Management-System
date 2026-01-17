namespace Submission.Service.DTOs.Common;

public class SubmissionStatistics
{
    public int TotalSubmissions { get; set; }
    public Dictionary<string, int> SubmissionsByStatus { get; set; } = new();
    public Dictionary<string, int> SubmissionsByTrack { get; set; } = new();
    public int AverageAuthorsPerPaper { get; set; }
    public decimal AveragePageCount { get; set; }
}
