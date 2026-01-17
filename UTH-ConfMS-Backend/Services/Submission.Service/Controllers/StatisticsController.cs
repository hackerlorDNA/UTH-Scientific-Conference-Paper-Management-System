using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Submission.Service.DTOs.Common;
using Submission.Service.DTOs.Responses;
using Submission.Service.Interfaces.Services;

namespace Submission.Service.Controllers;

/// <summary>
/// Controller for submission statistics
/// </summary>
[ApiController]
[Route("api/submissions/statistics")]
[Authorize]
public class StatisticsController : ControllerBase
{
    private readonly ISubmissionService _submissionService;
    private readonly ILogger<StatisticsController> _logger;

    public StatisticsController(ISubmissionService submissionService, ILogger<StatisticsController> logger)
    {
        _submissionService = submissionService;
        _logger = logger;
    }

    /// <summary>
    /// Get submission statistics for conference
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "RequireConferenceChair")]
    public async Task<IActionResult> GetStatistics([FromQuery] Guid conferenceId)
    {
        try
        {
            var stats = await _submissionService.GetSubmissionStatisticsAsync(conferenceId);
            return Ok(new ApiResponse<SubmissionStatisticsDto>
            {
                Success = true,
                Data = stats
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get statistics failed for conference {ConferenceId}", conferenceId);
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
