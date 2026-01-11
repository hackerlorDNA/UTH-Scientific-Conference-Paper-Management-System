using Conference.Service.DTOs;
using Conference.Service.Entities;

namespace Conference.Service.Interfaces
{
    public interface ITrackService
    {
        // Lấy danh sách Track theo ID Hội nghị (Để frontend hiện dropdown cho user chọn)
        Task<IEnumerable<Track>> GetTracksByConferenceIdAsync(int conferenceId);

        // Tạo Track mới
        Task<(bool IsSuccess, string ErrorMessage, Track? Track)> CreateTrackAsync(CreateTrackDTO dto);
    }
}