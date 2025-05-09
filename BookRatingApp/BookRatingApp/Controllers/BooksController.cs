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
        
        private static List<Book> books = new List<Book>();
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
                _context.Add(book);
                _context.SaveChanges();
                books.Add(book);
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
            var book = books.FirstOrDefault(b => b.Id == id);
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
            if (ModelState.IsValid)
            {
                var book = books.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    book.Reviews.Add(review);
                    return RedirectToAction("Details", new { id });
                }
                return NotFound();
            }
            return View(review);
        }
        public IActionResult Details(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

    }
}
