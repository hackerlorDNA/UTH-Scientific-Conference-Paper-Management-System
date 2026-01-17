using Submission.Service.Data;
using Submission.Service.Entities;
using Submission.Service.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Submission.Service.Repositories;

/// <summary>
/// Repository implementation for SubmissionFile entity operations
/// </summary>
public class SubmissionFileRepository : ISubmissionFileRepository
{
    private readonly SubmissionDbContext _context;

    public SubmissionFileRepository(SubmissionDbContext context)
    {
        _context = context;
    }

    public async Task<SubmissionFile?> GetByIdAsync(Guid fileId)
    {
        return await _context.SubmissionFiles.FindAsync(fileId);
    }

    public async Task<SubmissionFile?> GetBySubmissionAndIdAsync(Guid submissionId, Guid fileId)
    {
        return await _context.SubmissionFiles
            .FirstOrDefaultAsync(f => f.SubmissionId == submissionId && f.FileId == fileId);
    }

    public async Task<List<SubmissionFile>> GetBySubmissionIdAsync(Guid submissionId)
    {
        return await _context.SubmissionFiles
            .Where(f => f.SubmissionId == submissionId)
            .OrderBy(f => f.UploadedAt)
            .ToListAsync();
    }

    public async Task<SubmissionFile> CreateAsync(SubmissionFile file)
    {
        await _context.SubmissionFiles.AddAsync(file);
        return file;
    }

    public Task UpdateAsync(SubmissionFile file)
    {
        _context.SubmissionFiles.Update(file);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(SubmissionFile file)
    {
        _context.SubmissionFiles.Remove(file);
        return Task.CompletedTask;
    }
}
