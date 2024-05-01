using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdvancedBookSearch.Models.DataLayer;

namespace AdvancedBookSearch.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookstoreContext _context;

        public BooksController(BookstoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookstoreContext = _context.Books.Include(b => b.Genre).Include(a => a.Authors);
            ViewBag.Count = bookstoreContext.Count();
            return View(await bookstoreContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> FilterBooks()
        {
            string keyword = Request.Form["keyword"].ToString();
            var genre = Request.Form["genre"].ToString();
            var price = Request.Form["price"].ToString();
            string[] parts = price.Split(":");

            var bookstoreContext = _context.Books.Include(b => b.Genre).Include(a => a.Authors).Where(m => m.Title.Contains(keyword));

            if (genre != "all" && price != "all")
            {
                if (parts[1] == "above")
                {
                    bookstoreContext = _context.Books.Include(b => b.Genre).Include(a => a.Authors)
                    .Where(m => m.Title.Contains(keyword) && m.Price >= 60 &&
                     m.Genre.GenreId == genre);
                }
                else
                {
                    int first_amount = int.Parse(parts[0]);
                    int second_amount = int.Parse(parts[1]);

                    bookstoreContext = _context.Books.Include(b => b.Genre).Include(a => a.Authors)
                    .Where(m => m.Title.Contains(keyword) && m.Price >= first_amount && m.Price < second_amount &&
                     m.Genre.GenreId == genre);
                }
            }
            else if (genre == "all" && price != "all")
            {
                if (parts[1] == "above")
                {
                    bookstoreContext = _context.Books.Include(b => b.Genre).Include(a => a.Authors)
                    .Where(m => m.Title.Contains(keyword) && m.Price >= 60);
                }
                else
                {
                    int first_amount = int.Parse(parts[0]);
                    int second_amount = int.Parse(parts[1]);

                    bookstoreContext = _context.Books.Include(b => b.Genre).Include(a => a.Authors)
                    .Where(m => m.Title.Contains(keyword) && m.Price >= first_amount && m.Price < second_amount);
                }
            }
            else if (genre != "all" && price == "all")
            {
                bookstoreContext = _context.Books.Include(b => b.Genre).Include(a => a.Authors)
                                    .Where(m => m.Title.Contains(keyword) && m.Genre.GenreId == genre);
            }

            ViewBag.Count = bookstoreContext.Count();
            return View("Index", await bookstoreContext.ToListAsync());
        }
    }
}
