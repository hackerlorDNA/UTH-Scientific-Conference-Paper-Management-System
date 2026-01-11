using Microsoft.EntityFrameworkCore;
using Submission.Service.DTOs;
using Submission.Service.Entities;
using Submission.Service.Interfaces;
using Submission.Service.Data;

namespace Submission.Service.Services
{
    public class PaperService : IPaperService
    {
        private readonly SubmissionDbContext _context;

        public PaperService(SubmissionDbContext context)
        {
            _context = context;
        }

        // 1. Logic Tạo Bài Báo (MỚI THÊM)
        public async Task<int> CreatePaperAsync(CreatePaperDTO dto, int userId)
        {
            // Tạo entity Paper mới
            var newPaper = new Paper
            {
                Title = dto.Title,
                Abstract = dto.Abstract,
                TrackId = dto.TrackId,
                Status = "Draft",
                FilePath = null
            };

            _context.Papers.Add(newPaper);
            await _context.SaveChangesAsync(); // Save lần 1 để lấy ID

            // Link User hiện tại làm Tác giả
            var paperAuthor = new PaperAuthor
            {
                PaperId = newPaper.Id,
                UserId = userId,
                IsCorresponding = true, 
                AuthorOrder = 1
            };

            _context.PaperAuthors.Add(paperAuthor);
            await _context.SaveChangesAsync(); // Save lần 2

            return newPaper.Id;
        }

        // 2. Logic Upload File (CŨ)
        public async Task SubmitPaperAsync(int paperId, int userId, string filePath)
        {
            var paper = await _context.Papers
                .Include(p => p.Authors)
                .FirstOrDefaultAsync(p => p.Id == paperId);

            if (paper == null) throw new Exception("Không tìm thấy bài báo");
            
            // Check quyền tác giả
            bool isAuthor = paper.Authors.Any(a => a.UserId == userId);
            if (!isAuthor) throw new Exception("Bạn không phải tác giả của bài này");

            paper.FilePath = filePath;
            paper.Status = "Submitted"; 

            await _context.SaveChangesAsync();
        }

        // 3. Logic Lấy Danh Sách (CŨ)
        public async Task<IEnumerable<Paper>> GetMyPapersAsync(int userId)
        {
            return await _context.Papers
                // .Include(p => p.Track) // TODO: Get track info from Conference.Service API
                .Include(p => p.Authors)
                .Where(p => p.Authors.Any(a => a.UserId == userId))
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }
    }
}