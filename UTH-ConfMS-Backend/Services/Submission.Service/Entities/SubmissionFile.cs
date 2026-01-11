using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Submission.Service.Entities
{
    [Table("submission_file")]
    public class SubmissionFile
    {
        [Key]
        [Column("file_id")]
        public int Id { get; set; }

        [Column("paper_id")]
        public int PaperId { get; set; }

        [Column("file_type")]
        public string? FileType { get; set; }

        [Column("file_path")]
        public string? FilePath { get; set; }

        [Column("uploaded_at")]
        public DateTime? UploadedAt { get; set; }

        // Navigation
        [ForeignKey("PaperId")]
        public Paper? Paper { get; set; }
    }
}
