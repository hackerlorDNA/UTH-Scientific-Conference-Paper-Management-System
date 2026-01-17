namespace Submission.Service.DTOs.Responses;

public record ReviewSummaryDto(
    Guid Id,
    decimal? OverallScore,
    string? Recommendation,
    string Status
);
