using Conference.Service.DTOs.Common; // Ensure this namespace exists or fix later
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Conference.Service.Integrations;

public class HttpIdentityIntegration : IIdentityIntegration
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpIdentityIntegration> _logger;
    private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

    public HttpIdentityIntegration(HttpClient httpClient, ILogger<HttpIdentityIntegration> logger, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<UserDto>> GetUsersByIdsAsync(List<Guid> userIds)
    {
        if (userIds == null || !userIds.Any()) return new List<UserDto>();

        try
        {
             // Propagate Bearer Token
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            }

            var json = JsonSerializer.Serialize(userIds);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/users/batch", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<UserDto>>>(responseContent, options);

            return apiResponse?.Data ?? new List<UserDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch users from Identity Service. BaseUrl: {BaseUrl}", _httpClient.BaseAddress);
            // Return empty list instead of throwing to avoid breaking entire flow
            return new List<UserDto>();
        }
    }
}

// Wrapper for API Response 
internal class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
}
