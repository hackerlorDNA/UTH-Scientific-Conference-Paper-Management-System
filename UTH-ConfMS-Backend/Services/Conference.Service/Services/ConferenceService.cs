using Microsoft.EntityFrameworkCore;
using Conference.Service.Entities;
using Conference.Service.Interfaces;
using Conference.Service.Data;

namespace Conference.Service.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly ConferenceDbContext _context;

        public ConferenceService(ConferenceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entities.Conference>> GetAllConferencesAsync()
        {
            // Sắp xếp hội nghị mới nhất lên đầu
            return await _context.Conferences
                                 .OrderByDescending(c => c.StartDate)
                                 .ToListAsync();
        }

        public async Task<Entities.Conference?> GetConferenceByIdAsync(int id)
        {
            return await _context.Conferences.FindAsync(id);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, Entities.Conference? Data)> CreateConferenceAsync(Entities.Conference conference)
        {
            // 1. Kiểm tra ngày kết thúc phải sau ngày bắt đầu
            if (conference.EndDate < conference.StartDate)
            {
                return (false, "Ngày kết thúc không được nhỏ hơn ngày bắt đầu!", null);
            }

            // 2. Không được tạo hội nghị trong quá khứ (Tính theo UTC để chuẩn server)
            if (conference.StartDate < DateTime.UtcNow)
            {
                return (false, "Ngày bắt đầu hội nghị phải ở tương lai!", null);
            }

            // 3. Kiểm tra trùng tên (Optional)
            bool isDuplicate = await _context.Conferences.AnyAsync(c => c.Name == conference.Name);
            if (isDuplicate)
            {
                return (false, $"Hội nghị tên '{conference.Name}' đã tồn tại!", null);
            }

            // Nếu thỏa mãn tất cả -> Lưu vào DB
            try 
            {
                _context.Conferences.Add(conference);
                await _context.SaveChangesAsync();
                return (true, null, conference);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi Database: {ex.Message}", null);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateConferenceAsync(int id, Entities.Conference conference)
        {
            if (id != conference.Id) return (false, "ID không khớp!");

            // Logic validate ngày tháng khi update
            if (conference.EndDate < conference.StartDate)
                return (false, "Ngày kết thúc không hợp lệ!");

            _context.Entry(conference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Conferences.AnyAsync(e => e.Id == id))
                    return (false, "Không tìm thấy hội nghị để sửa!");
                else
                    throw;
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteConferenceAsync(int id)
        {
            var conference = await _context.Conferences.FindAsync(id);
            if (conference == null) return (false, "Không tìm thấy hội nghị!");
            
            // Kiểm tra ràng buộc nếu có (ví dụ: đã có bài báo đăng ký trong hội nghị)
            // if (conference.Papers.Count > 0) return (false, "Không thể xóa hội nghị đã có bài báo!");

            _context.Conferences.Remove(conference);
            await _context.SaveChangesAsync();

            return (true, null);
        }
    }
}