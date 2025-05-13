using BookRatingApp.Data;
using BookRatingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookRatingApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        // allow users to add a new book
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(book);
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
                Console.WriteLine($"Book title received: {_context.Books.FirstOrDefault(b => b.Id == review.BookId)}");
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
