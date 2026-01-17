namespace Submission.Service.DTOs.Responses;

public record SubmissionStatisticsDto(
    int TotalSubmissions,
    Dictionary<string, int> SubmissionsByStatus,
    Dictionary<string, int> SubmissionsByTrack,
    int AverageAuthorsPerPaper,
    decimal AveragePageCount
);
