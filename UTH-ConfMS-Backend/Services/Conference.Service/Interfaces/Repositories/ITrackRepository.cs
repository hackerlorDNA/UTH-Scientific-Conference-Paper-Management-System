using Conference.Service.Entities;

namespace Conference.Service.Interfaces.Repositories;

/// <summary>
/// Repository interface for ConferenceTrack entity operations
/// </summary>
public interface ITrackRepository
{
    Task<ConferenceTrack?> GetByIdAsync(Guid trackId);
    Task<List<ConferenceTrack>> GetByConferenceIdAsync(Guid conferenceId);
    Task<ConferenceTrack> CreateAsync(ConferenceTrack track);
    Task UpdateAsync(ConferenceTrack track);
    Task DeleteAsync(ConferenceTrack track);
}
