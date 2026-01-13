namespace Conference.Service.DTOs.Responses;

public class ConferenceDetailDto : ConferenceDto
{
    public List<TrackDto> Tracks { get; set; } = new();
    public List<DeadlineDto> Deadlines { get; set; } = new();
    public List<string> Topics { get; set; } = new();
}
