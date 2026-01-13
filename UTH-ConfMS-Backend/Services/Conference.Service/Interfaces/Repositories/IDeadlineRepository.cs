using Conference.Service.Entities;

namespace Conference.Service.Interfaces.Repositories;

/// <summary>
/// Repository interface for ConferenceDeadline entity operations
/// </summary>
public interface IDeadlineRepository
{
    Task<ConferenceDeadline?> GetByIdAsync(Guid deadlineId);
    Task<List<ConferenceDeadline>> GetByConferenceIdAsync(Guid conferenceId);
    Task<ConferenceDeadline> CreateAsync(ConferenceDeadline deadline);
    Task UpdateAsync(ConferenceDeadline deadline);
    Task DeleteAsync(ConferenceDeadline deadline);
}
