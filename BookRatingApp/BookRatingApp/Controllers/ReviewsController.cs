using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookRatingApp.Data;
using BookRatingApp.Models;

namespace BookRatingApp.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BooksController> _logger;
        public ReviewsController(ILogger<BooksController> logger,AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(int bookId)
        {
            var appDbContext = _context.Reviews.Include(review => review.Book).Where(review => review.BookId == bookId).ToListAsync();
            return View(await appDbContext);
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }
        public IActionResult AddReview(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                return NotFound();
            }
            // Δημιουργούμε ένα νέο Review και του περνάμε το BookId
            var review = new Review
            {
                BookId = bookId // Αυτό είναι το BookId που θέλουμε να στείλουμε
            };

            ViewBag.BookTitle = book.Title;
            return View(review);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(Review review)
        {
            //Console.WriteLine($"review.Book is {review.Book.Title}");
            Console.WriteLine($"BookId received: {review.BookId}");

            var book = _context.Books.FirstOrDefault(b => b.Id == review.BookId);
            if (book == null)
            {
                return NotFound(); // No matching book
            }

            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                Console.WriteLine($"Book title received: {_context.Books.FirstOrDefault(b => b.Id == review.BookId).Title}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Console.WriteLine("State invalid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                ViewBag.BookTitle = book.Title;
                return View(review);
            }
        }

    }
}
