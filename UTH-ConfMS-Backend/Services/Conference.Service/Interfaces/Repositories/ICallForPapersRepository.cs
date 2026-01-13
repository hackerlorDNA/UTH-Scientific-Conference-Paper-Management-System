using Conference.Service.Entities;

namespace Conference.Service.Interfaces.Repositories;

/// <summary>
/// Repository interface for CallForPapers entity operations
/// </summary>
public interface ICallForPapersRepository
{
    Task<CallForPapers?> GetByConferenceIdAsync(Guid conferenceId);
    Task<CallForPapers> CreateAsync(CallForPapers cfp);
    Task UpdateAsync(CallForPapers cfp);
}
