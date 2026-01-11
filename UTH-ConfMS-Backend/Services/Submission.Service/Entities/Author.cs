using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Submission.Service.Entities
{
    [Table("author")]
    public class Author
    {
        [Key]
        [Column("author_id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("affiliation")]
        public string? Affiliation { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        // Navigation
        public ICollection<PaperAuthor> Papers { get; set; } = new List<PaperAuthor>();
    }
}
