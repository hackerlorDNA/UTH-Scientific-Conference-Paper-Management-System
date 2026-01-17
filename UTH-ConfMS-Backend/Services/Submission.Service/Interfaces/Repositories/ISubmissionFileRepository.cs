using Submission.Service.Entities;

namespace Submission.Service.Interfaces.Repositories;

/// <summary>
/// Repository interface for SubmissionFile entity operations
/// </summary>
public interface ISubmissionFileRepository
{
    Task<SubmissionFile?> GetByIdAsync(Guid fileId);
    Task<SubmissionFile?> GetBySubmissionAndIdAsync(Guid submissionId, Guid fileId);
    Task<List<SubmissionFile>> GetBySubmissionIdAsync(Guid submissionId);
    Task<SubmissionFile> CreateAsync(SubmissionFile file);
    Task UpdateAsync(SubmissionFile file);
    Task DeleteAsync(SubmissionFile file);
}
