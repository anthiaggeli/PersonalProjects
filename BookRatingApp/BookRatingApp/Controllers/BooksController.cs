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
    }
}
