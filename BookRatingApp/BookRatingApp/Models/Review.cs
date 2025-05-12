using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        [Required]
        public int BookId { get; internal set; }
        public Book Book { get; set; }  // Σχέση με το Book (αλλά δεν απαιτείται να το στείλεις από τη φόρμα)

    }
}