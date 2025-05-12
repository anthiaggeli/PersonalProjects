using BookRatingApp.Data;
using BookRatingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookRatingApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger ,AppDbContext context)
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
        public IActionResult AddReview(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.BookTitle = book.Title;
            return View(new Review());
        }
        [HttpPost]
        public IActionResult AddReview(int id, Review review)
        {
            review.Book = _context.Books.FirstOrDefault(b => b.Id == id);
            Console.WriteLine($"review.Book is {review.Book.Title}");
            if (ModelState.IsValid)
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    book.Reviews.Add(review);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                return NotFound();
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(review);
            }
            
        }
    }
}
