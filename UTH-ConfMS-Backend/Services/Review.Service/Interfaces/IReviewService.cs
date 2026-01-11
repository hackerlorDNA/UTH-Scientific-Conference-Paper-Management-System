using Review.Service.DTOs;
using Review.Service.Entities;

namespace Review.Service.Interfaces
{
    public interface IReviewService
    {
        Task<Entities.Review> SubmitReviewAsync(SubmitReviewDTO dto);
        Task<IEnumerable<Entities.Review>> GetReviewsByPaperIdAsync(int paperId);
        Task<IEnumerable<object>> GetMyAssignedPapersAsync(int reviewerId); // Lấy bài được giao cho tôi
    }
}