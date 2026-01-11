using Conference.Service.Entities;

namespace Conference.Service.Interfaces
{
    public interface IConferenceService
    {
        // Lấy danh sách
        Task<IEnumerable<Entities.Conference>> GetAllConferencesAsync();
        
        // Lấy 1 cái
        Task<Entities.Conference?> GetConferenceByIdAsync(int id);
        
        // Thêm mới (Trả về kèm thông báo lỗi nếu có)
        Task<(bool IsSuccess, string? ErrorMessage, Entities.Conference? Data)> CreateConferenceAsync(Entities.Conference conference);
        
        // Sửa
        Task<(bool IsSuccess, string? ErrorMessage)> UpdateConferenceAsync(int id, Entities.Conference conference);
        
        // Xóa
        Task<(bool IsSuccess, string? ErrorMessage)> DeleteConferenceAsync(int id);
    }
}