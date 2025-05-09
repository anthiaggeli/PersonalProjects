using System.ComponentModel.DataAnnotations;

namespace BookRatingApp.Models
{
    public class Review
    {

        public int Id { get; set; }
        [Required]
        public string Reviewer { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // 1 to 5
    }
}
