using Conference.Service.DTOs.Common;

namespace Conference.Service.Integrations;

public interface IIdentityIntegration
{
    Task<List<UserDto>> GetUsersByIdsAsync(List<Guid> userIds);
}

// Minimal DTO for User needed in Conference
// We can reuse DTOs.Common.UserDto if it exists or create one here.
// Let's check Conference.Service.DTOs.Common namespace.
