namespace Conference.Service.DTOs.Common;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Affiliation { get; set; }
    public DateTime CreatedAt { get; set; }
}
