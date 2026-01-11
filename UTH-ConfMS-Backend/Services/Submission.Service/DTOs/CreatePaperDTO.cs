using System.ComponentModel.DataAnnotations;

namespace Submission.Service.DTOs
{
    public class CreatePaperDTO
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Abstract is required")]
        public string Abstract { get; set; } = string.Empty;

        [Required(ErrorMessage = "TrackId is required")]
        public int TrackId { get; set; }
    }
}