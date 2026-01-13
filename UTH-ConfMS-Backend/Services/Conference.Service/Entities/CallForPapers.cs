using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conference.Service.Entities;

[Table("call_for_papers")]
public class CallForPapers
{
    [Key]
    [Column("cfp_id")]
    public Guid CfpId { get; set; }

    [Column("conference_id")]
    public Guid ConferenceId { get; set; }

    [Column("title")]
    [MaxLength(500)]
    public string Title { get; set; } = string.Empty;

    [Column("content")]
    public string Content { get; set; } = string.Empty;

    [Column("submission_guidelines")]
    public string? SubmissionGuidelines { get; set; }

    [Column("formatting_requirements")]
    public string? FormattingRequirements { get; set; }

    [Column("min_pages")]
    public int? MinPages { get; set; }

    [Column("max_pages")]
    public int? MaxPages { get; set; }

    [Column("accepted_file_formats")]
    [MaxLength(255)]
    public string AcceptedFileFormats { get; set; } = "PDF";

    [Column("max_file_size_mb")]
    public int MaxFileSizeMb { get; set; } = 10;

    [Column("is_published")]
    public bool IsPublished { get; set; } = false;

    [Column("published_at")]
    public DateTime? PublishedAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("ConferenceId")]
    public virtual Conference Conference { get; set; } = null!;
}
