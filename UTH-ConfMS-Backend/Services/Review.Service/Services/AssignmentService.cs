using Microsoft.EntityFrameworkCore;
using Review.Service.DTOs;
using Review.Service.Entities;
using Review.Service.Interfaces;
using Review.Service.Data;

namespace Review.Service.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly ReviewDbContext _context;

        public AssignmentService(ReviewDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AssignReviewerAsync(AssignReviewerDTO dto)
        {
            // 1. Kiểm tra bài báo tồn tại
            // TODO: Call Submission.Service API to check if paper exists
            // var paper = await _context.Papers.FindAsync(dto.PaperId);
            // if (paper == null) throw new Exception("Paper not found");

            // 2. Kiểm tra reviewer tồn tại
            // TODO: Call Identity.Service API to check if reviewer exists
            // var reviewer = await _context.Users.FindAsync(dto.ReviewerId);
            // if (reviewer == null) throw new Exception("Reviewer not found");

            // 3. Kiểm tra xem đã phân công chưa (tránh trùng)
            var exists = await _context.Assignments
                .AnyAsync(a => a.PaperId == dto.PaperId && a.ReviewerId == dto.ReviewerId);
            
            if (exists) return false;

            // 4. Tạo phân công
            var assignment = new Assignment
            {
                PaperId = dto.PaperId,
                ReviewerId = dto.ReviewerId,
                AssignedDate = DateTime.UtcNow,
                Status = "Pending" // Reviewer chưa Accept
            };

            _context.Assignments.Add(assignment);
            
            // TODO: Call Submission.Service API to update paper status to "Under Review"
            // paper.Status = "Under Review";
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<object>> GetReviewersForPaperAsync(int paperId)
        {
            return await _context.Assignments
                .Where(a => a.PaperId == paperId)
                .Select(a => new {
                    a.Id,
                    ReviewerId = a.ReviewerId, // TODO: Get reviewer name from Identity.Service API
                    // ReviewerName = a.Reviewer.FullName,
                    a.Status,
                    a.AssignedDate
                }).ToListAsync();
        }
    }
}