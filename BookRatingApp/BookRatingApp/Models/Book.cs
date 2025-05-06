using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace BookRatingApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ο Τίτλος είναι υποχρεωτικός.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ο Συγγραφέας είναι υποχρεωτικός.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Το Είδος είναι υποχρεωτικό.")]
        public string Genre { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}

    

