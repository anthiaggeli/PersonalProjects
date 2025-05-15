using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookRatingApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Display(Name = "Αξιολογητής")]
        [Required]
        public string Reviewer { get; set; }
        [Display(Name = "Σχόλια")]
        [StringLength(500)]
        public string Comment { get; set; }
        [Display(Name = "Βαθμολογία")]
        [Range(1, 5)]
        public int Rating { get; set; } // 1 to 5
        
        [Required]
        public int BookId { get; set; }
        [Display(Name = "Βιβλίο")]
        public Book? Book { get; set; } // Σχέση με το Book (αλλά δεν απαιτείται να το στείλεις από τη φόρμα)
    
    }
}