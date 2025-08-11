using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using fatmeh_lab2_backend.Presentation.Models;

namespace fatmeh_lab2_backend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private static List<Author> _authors = new List<Author>
        {
            new Author { AuthorId = 1, Name = "Carine", BirthDate = new DateTime(1965, 7, 31), Country = "Lebanon" },
            new Author { AuthorId = 2, Name = "Nadine", BirthDate = new DateTime(1903, 6, 25), Country = "Lebanon" },
            new Author { AuthorId = 3, Name = "Zendaya", BirthDate = new DateTime(1926, 4, 28), Country = "USA" },
          
        };

        private static List<Book> _books = new List<Book>
        {
            new Book
            {
                BookId = 1, Title = "Albi da2", AuthorId = 1, ISBN = "9780747532743",
                PublishedYear = 1997, ReleaseDate = new DateTime(1997, 6, 26)
            },
            new Book
            {
                BookId = 2, Title = "Kfarnahoum", AuthorId = 2, ISBN = "9780451524935", PublishedYear = 1949,
                ReleaseDate = new DateTime(1949, 6, 8)
            },
            new Book
            {
                BookId = 3, Title = "Spiderman", AuthorId = 3, ISBN = "9780061120084", PublishedYear = 1960,
                ReleaseDate = new DateTime(1960, 7, 11)
            },
          
        };


        [HttpGet("books-by-year")]
        public IActionResult GetBooksByYear(int year, string sortOrder = "asc")
        {
            var query = _books.Where(b => b.PublishedYear == year);

            query = sortOrder.ToLower() == "desc"
                ? query.OrderByDescending(b => b.ReleaseDate)
                : query.OrderBy(b => b.ReleaseDate);

            return Ok(query.ToList());
        }


        [HttpGet("authors-by-birth-year")]
        public IActionResult GetAuthorsByBirthYear()
        {
            var result = _authors
                .GroupBy(a => a.BirthDate.Year)
                .Select(g => new
                {
                    BirthYear = g.Key,
                    Authors = g.ToList()
                });

            return Ok(result);
        }


        [HttpGet("authors-by-year-country")]
        public IActionResult GetAuthorsByYearAndCountry()
        {
            var result = _authors
                .GroupBy(a => new { a.BirthDate.Year, a.Country })
                .Select(g => new
                {
                    BirthYear = g.Key.Year,
                    Country = g.Key.Country,
                    Authors = g.ToList()
                });

            return Ok(result);
        }


        [HttpGet("total-books")]
        public IActionResult GetTotalBooks()
        {
            return Ok(new { TotalBooks = _books.Count });
        }


        [HttpGet("books-paginated")]
        public IActionResult GetBooksPaginated(int pageNumber = 1, int pageSize = 3)
        {
            var query = _books
                .OrderBy(b => b.BookId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _books.Count,
                Items = query.ToList()
            });
        }
    }
}