using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Review.Service.Entities
{
    [Table("assignment")]
    public class Assignment
    {
        [Key]
        [Column("assignment_id")]
        public int Id { get; set; }

        [Column("paper_id")]
        public int PaperId { get; set; }

        [Column("reviewer_id")]
        public int ReviewerId { get; set; }

        [Column("assigned_date")]
        public DateTime? AssignedDate { get; set; }

        [Column("due_date")]
        public DateTime? DueDate { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        // Navigation
        // [ForeignKey("PaperId")]
        // public Paper? Paper { get; set; } // TODO: Add reference to Submission.Service

        // [ForeignKey("ReviewerId")]
        // public User? Reviewer { get; set; } // TODO: Add reference to Identity.Service

        public Review? Review { get; set; }
    }
}