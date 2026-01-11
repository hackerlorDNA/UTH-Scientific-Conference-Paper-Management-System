using Microsoft.EntityFrameworkCore;
using Conference.Service.DTOs;
using Conference.Service.Entities;
using Conference.Service.Interfaces;
using Conference.Service.Data;

namespace Conference.Service.Services
{
    public class TrackService : ITrackService
    {
        private readonly ConferenceDbContext _context;

        public TrackService(ConferenceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Track>> GetTracksByConferenceIdAsync(int conferenceId)
        {
            return await _context.Tracks
                .Where(t => t.ConfId == conferenceId)
                .ToListAsync();
        }

        public async Task<(bool IsSuccess, string ErrorMessage, Track? Track)> CreateTrackAsync(CreateTrackDTO dto)
        {
            // 1. Kiểm tra Hội nghị có tồn tại không?
            var confExists = await _context.Conferences.AnyAsync(c => c.Id == dto.ConferenceId);
            if (!confExists)
            {
                return (false, "Hội nghị không tồn tại!", null);
            }

            // 2. Tạo Track
            var track = new Track
            {
                ConfId = dto.ConferenceId,
                Name = dto.Name,
                TopicKeywords = dto.TopicKeywords
            };

            try
            {
                _context.Tracks.Add(track);
                await _context.SaveChangesAsync();
                return (true, "", track);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
    }
}