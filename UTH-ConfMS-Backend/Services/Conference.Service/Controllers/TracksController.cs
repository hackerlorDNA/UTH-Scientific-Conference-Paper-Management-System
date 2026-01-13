using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Conference.Service.DTOs.Common;
using Conference.Service.DTOs.Requests;
using Conference.Service.DTOs.Responses;
using Conference.Service.Interfaces.Services;

namespace Conference.Service.Controllers;

[ApiController]
[Route("api/conferences/{conferenceId:guid}/[controller]")]
[Authorize]
public class TracksController : ControllerBase
{
    private readonly IConferenceService _conferenceService;
    private readonly ILogger<TracksController> _logger;

    public TracksController(IConferenceService conferenceService, ILogger<TracksController> logger)
    {
        _conferenceService = conferenceService;
        _logger = logger;
    }

    /// <summary>
    /// Get all tracks for a conference
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTracks(Guid conferenceId)
    {
        try
        {
            var tracks = await _conferenceService.GetTracksAsync(conferenceId);
            return Ok(new ApiResponse<List<TrackDto>>
            {
                Success = true,
                Data = tracks
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get tracks failed for conference {ConferenceId}", conferenceId);
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                Message = "Conference not found"
            });
        }
    }

    /// <summary>
    /// Add a new track to conference
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "RequireConferenceManage")]
    public async Task<IActionResult> AddTrack(Guid conferenceId, [FromBody] CreateTrackRequest request)
    {
        try
        {
            var track = await _conferenceService.AddTrackAsync(conferenceId, request);
            return Ok(new ApiResponse<TrackDto>
            {
                Success = true,
                Message = "Track added successfully",
                Data = track
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add track failed for conference {ConferenceId}", conferenceId);
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
