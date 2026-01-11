using Submission.Service.DTOs;
using Submission.Service.Entities;

namespace Submission.Service.Interfaces
{
    public interface IPaperService
    {
        // Hàm tạo bài báo (Metadata) - Đang bị thiếu
        Task<int> CreatePaperAsync(CreatePaperDTO dto, int userId);

        // Hàm upload file (Submit)
        Task SubmitPaperAsync(int paperId, int userId, string filePath);

        // Hàm lấy danh sách bài (cho Dashboard)
        Task<IEnumerable<Paper>> GetMyPapersAsync(int userId);
    }
}