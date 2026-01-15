using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Review.Service.Data;
using Review.Service.DTOs;
using Review.Service.Entities;
using Review.Service.Interfaces;

namespace Review.Service.Services;

public class ReviewerService : IReviewerService
{
    private readonly ReviewDbContext _context;
    private readonly ILogger<ReviewerService> _logger;

    public ReviewerService(ReviewDbContext context, ILogger<ReviewerService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewerInvitation> InviteReviewerAsync(InviteReviewerDTO dto)
    {
        // Kiểm tra xem đã mời chưa
        var existing = await _context.ReviewerInvitations
            .FirstOrDefaultAsync(x => x.ConferenceId == dto.ConferenceId && x.Email == dto.Email);

        if (existing != null)
        {
            throw new Exception("Email này đã được gửi lời mời cho hội nghị này.");
        }

        // Kiểm tra xem đã là Reviewer chưa
        var isReviewer = await _context.Reviewers
            .AnyAsync(r => r.ConferenceId == dto.ConferenceId && r.Email == dto.Email);
        
        if (isReviewer)
        {
            throw new Exception("Người dùng này đã là Reviewer của hội nghị.");
        }

        var invitation = new ReviewerInvitation
        {
            ConferenceId = dto.ConferenceId,
            Email = dto.Email,
            FullName = dto.FullName,
            Status = "Pending",
            Token = Guid.NewGuid().ToString(),
            SentAt = DateTime.UtcNow
        };

        _context.ReviewerInvitations.Add(invitation);
        await _context.SaveChangesAsync();

        // TODO: Tích hợp Notification Service để gửi email thực tế tại đây (USCPMS-Notification)
        _logger.LogInformation($"Invitation created for {dto.Email} with token {invitation.Token}");
        
        return invitation;
    }

    public async Task<bool> RespondToInvitationAsync(InvitationResponseDTO dto, int? userId = null)
    {
        var invitation = await _context.ReviewerInvitations
            .FirstOrDefaultAsync(x => x.Token == dto.Token);

        if (invitation == null || invitation.Status != "Pending")
        {
            throw new Exception("Lời mời không hợp lệ hoặc đã được xử lý.");
        }

        invitation.RespondedAt = DateTime.UtcNow;
        invitation.Status = dto.IsAccepted ? "Accepted" : "Declined";

        if (dto.IsAccepted && userId.HasValue)
        {
            // Kiểm tra xem user đã là Reviewer chưa để tránh lỗi trùng lặp
            var alreadyExists = await _context.Reviewers
                .AnyAsync(r => r.UserId == userId.Value && r.ConferenceId == invitation.ConferenceId);

            if (!alreadyExists)
            {
                // Thêm vào bảng Reviewers
                var reviewer = new Reviewer
                {
                    UserId = userId.Value,
                    ConferenceId = invitation.ConferenceId,
                    Email = invitation.Email,
                    FullName = invitation.FullName,
                    Expertise = "General", // Mặc định, user sẽ cập nhật sau
                };
                _context.Reviewers.Add(reviewer);
            }
            else
            {
                _logger.LogWarning($"User {userId} is already a reviewer for conference {invitation.ConferenceId}.");
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Reviewer>> GetReviewersByConferenceAsync(int conferenceId)
        => await _context.Reviewers.Where(r => r.ConferenceId == conferenceId).ToListAsync();

    public async Task<List<ReviewerInvitation>> GetInvitationsByConferenceAsync(int conferenceId)
        => await _context.ReviewerInvitations.Where(i => i.ConferenceId == conferenceId).ToListAsync();
}