using Submission.Service.Entities;

namespace Submission.Service.Interfaces.Repositories;

/// <summary>
/// Repository interface for Author entity operations
/// </summary>
public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid authorId);
    Task<List<Author>> GetBySubmissionIdAsync(Guid submissionId);
    Task<Author> CreateAsync(Author author);
    Task CreateRangeAsync(IEnumerable<Author> authors);
    Task UpdateAsync(Author author);
    Task DeleteAsync(Author author);
    Task DeleteRangeAsync(IEnumerable<Author> authors);
}
