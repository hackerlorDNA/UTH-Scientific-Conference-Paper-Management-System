using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTHConfMS.Infra.Data;
using UTHConfMS.Core.Entities;

namespace UTHConfMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConferenceController(AppDbContext context)
        {
            _context = context;
        }

        // lấy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conference>>> GetConferences()
        {
            return await _context.Conferences.ToListAsync();
        }

        // thêm
        [HttpPost]
        public async Task<ActionResult<Conference>> CreateConference(Conference conference)
        {
            _context.Conferences.Add(conference);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConferences), new { id = conference.Id }, conference);
        }
    }
}