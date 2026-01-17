namespace Submission.Service.DTOs.Responses;

public record AuthorDto(
    Guid Id,
    string FullName,
    string Email,
    string? Affiliation,
    int AuthorOrder,
    bool IsCorresponding
);
