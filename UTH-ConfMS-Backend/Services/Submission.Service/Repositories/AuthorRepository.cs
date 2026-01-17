using Submission.Service.Data;
using Submission.Service.Entities;
using Submission.Service.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Submission.Service.Repositories;

/// <summary>
/// Repository implementation for Author entity operations
/// </summary>
public class AuthorRepository : IAuthorRepository
{
    private readonly SubmissionDbContext _context;

    public AuthorRepository(SubmissionDbContext context)
    {
        _context = context;
    }

    public async Task<Author?> GetByIdAsync(Guid authorId)
    {
        return await _context.Authors.FindAsync(authorId);
    }

    public async Task<List<Author>> GetBySubmissionIdAsync(Guid submissionId)
    {
        return await _context.Authors
            .Where(a => a.SubmissionId == submissionId)
            .OrderBy(a => a.AuthorOrder)
            .ToListAsync();
    }

    public async Task<Author> CreateAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        return author;
    }

    public async Task CreateRangeAsync(IEnumerable<Author> authors)
    {
        await _context.Authors.AddRangeAsync(authors);
    }

    public Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Author author)
    {
        _context.Authors.Remove(author);
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<Author> authors)
    {
        _context.Authors.RemoveRange(authors);
        return Task.CompletedTask;
    }
}
