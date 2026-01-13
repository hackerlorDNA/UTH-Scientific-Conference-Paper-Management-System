using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conference.Service.Entities;

[Table("conference_topics")]
public class ConferenceTopic
{
    [Key]
    [Column("topic_id")]
    public Guid TopicId { get; set; }

    [Column("conference_id")]
    public Guid ConferenceId { get; set; }

    [Column("name")]
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("ConferenceId")]
    public virtual Conference Conference { get; set; } = null!;
}
