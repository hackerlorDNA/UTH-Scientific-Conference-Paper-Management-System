using Conference.Service.Data;
using Conference.Service.Entities;
using Conference.Service.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Conference.Service.Repositories;

/// <summary>
/// Repository implementation for CallForPapers entity operations
/// </summary>
public class CallForPapersRepository : ICallForPapersRepository
{
    private readonly ConferenceDbContext _context;

    public CallForPapersRepository(ConferenceDbContext context)
    {
        _context = context;
    }

    public async Task<CallForPapers?> GetByConferenceIdAsync(Guid conferenceId)
    {
        return await _context.CallForPapers
            .FirstOrDefaultAsync(c => c.ConferenceId == conferenceId);
    }

    public async Task<CallForPapers> CreateAsync(CallForPapers cfp)
    {
        await _context.CallForPapers.AddAsync(cfp);
        return cfp;
    }

    public Task UpdateAsync(CallForPapers cfp)
    {
        _context.CallForPapers.Update(cfp);
        return Task.CompletedTask;
    }
}
