using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Service.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public Guid Id { get; set; }

        [Column("email")]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Column("username")]
        public string? Username { get; set; }

        [Column("password_hash")]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("full_name")]
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Column("affiliation")]
        public string? Affiliation { get; set; }

        [Column("department")]
        public string? Department { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties (Identity service chỉ cần Users nên có thể bổ sung sau nếu cần)
    }
}
