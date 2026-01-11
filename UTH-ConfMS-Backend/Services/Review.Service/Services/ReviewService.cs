using Microsoft.EntityFrameworkCore;
using Review.Service.DTOs;
using Review.Service.Entities;
using Review.Service.Interfaces;
using Review.Service.Data;

namespace Review.Service.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewDbContext _context;

        public ReviewService(ReviewDbContext context)
        {
            _context = context;
        }

        public async Task<Entities.Review> SubmitReviewAsync(SubmitReviewDTO dto)
        {
            // 1. Tìm bản ghi phân công (Assignment) dựa trên PaperId và ReviewerId
            var assignment = await _context.Assignments
                .FirstOrDefaultAsync(a => a.PaperId == dto.PaperId && a.ReviewerId == dto.ReviewerId);

            if (assignment == null) throw new Exception("You are not assigned to review this paper (Assignment not found).");

            // 2. Tạo Review mới (Đã sửa lại tên biến cho khớp với Entity Review.cs)
            var review = new Entities.Review
            {
                // Thay vì PaperId/ReviewerId, ta lưu AssignmentId
                AssignmentId = assignment.Id, 

                // Map điểm số
                Score = dto.Score,

                // SỬA: Content -> CommentsForAuthor
                CommentsForAuthor = dto.Comments, 

                // THÊM: Map ConfidentialComments (nếu trong DTO có)
                ConfidentialComments = dto.ConfidentialComments,

                // SỬA: ReviewDate -> SubmittedAt
                SubmittedAt = DateTime.UtcNow 
            };

            _context.Reviews.Add(review);
            
            // 3. Cập nhật trạng thái Assignment thành Completed
            assignment.Status = "Completed";

            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<object>> GetMyAssignedPapersAsync(int reviewerId)
        {
            // Lấy danh sách bài báo mà user này được gán
            return await _context.Assignments
                // .Include(a => a.Paper) // TODO: Load Paper from Submission.Service API
                .Where(a => a.ReviewerId == reviewerId)
                .Select(a => new 
                {
                    AssignmentId = a.Id,
                    PaperId = a.PaperId, // Use PaperId instead of Paper navigation
                    // PaperTitle = a.Paper.Title, // TODO: Get title from Submission.Service API
                    Status = a.Status, // Pending / Completed
                    Deadline = "2024-12-31" 
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Entities.Review>> GetReviewsByPaperIdAsync(int paperId)
        {
            // SỬA QUERY: Vì Review không có PaperId, phải đi vòng qua Assignment
            return await _context.Reviews
                .Include(r => r.Assignment)
                .Where(r => r.Assignment.PaperId == paperId)
                .ToListAsync();
        }
    }
}