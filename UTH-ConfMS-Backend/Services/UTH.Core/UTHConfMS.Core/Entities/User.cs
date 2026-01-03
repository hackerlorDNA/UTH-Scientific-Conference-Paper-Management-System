using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UTHConfMS.Core.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }

        [Column("full_name")]
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Column("email")]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Column("password_hash")]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("role")]
        [Required]
        public string Role { get; set; } = string.Empty;

        [Column("affiliation")]
        public string? Affiliation { get; set; }

        [Column("expertise_keywords")]
        public string? ExpertiseKeywords { get; set; }

        // Navigation properties
        public ICollection<PaperAuthor> AuthoredPapers { get; set; } = new List<PaperAuthor>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public ICollection<Decision> DecisionsMade { get; set; } = new List<Decision>();
    }
}
