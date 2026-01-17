using Submission.Service.Interfaces.Repositories;

namespace Submission.Service.Interfaces;

/// <summary>
/// Unit of Work interface for managing transactions across repositories
/// </summary>
public interface IUnitOfWork : IDisposable
{
    ISubmissionRepository Submissions { get; }
    IAuthorRepository Authors { get; }
    ISubmissionFileRepository SubmissionFiles { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
