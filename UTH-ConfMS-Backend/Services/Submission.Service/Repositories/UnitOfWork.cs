using Submission.Service.Data;
using Submission.Service.Interfaces;
using Submission.Service.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Submission.Service.Repositories;

/// <summary>
/// Unit of Work implementation for managing transactions across repositories
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly SubmissionDbContext _context;
    private IDbContextTransaction? _transaction;
    
    private ISubmissionRepository? _submissions;
    private IAuthorRepository? _authors;
    private ISubmissionFileRepository? _submissionFiles;

    public UnitOfWork(SubmissionDbContext context)
    {
        _context = context;
    }

    public ISubmissionRepository Submissions => _submissions ??= new SubmissionRepository(_context);
    public IAuthorRepository Authors => _authors ??= new AuthorRepository(_context);
    public ISubmissionFileRepository SubmissionFiles => _submissionFiles ??= new SubmissionFileRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
