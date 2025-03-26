using Microsoft.AspNetCore.Mvc;
using BookLibraryApi.Models;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> _books = new()
        {
            new Book { Id = 1, Title = "Atomic Habits", Author = "James Clear" },
            new Book { Id = 2, Title = "Deep Work", Author = "Cal Newport" }
        };

       [HttpGet("get-all-books")]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(_books);
        }
        
        [HttpGet("book/{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound() : Ok(book);
        }
        
        [HttpPost("add-book")]
        public ActionResult AddBook([FromBody] Book book)
        {
            book.Id = _books.Max(b => b.Id) + 1;
            _books.Add(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

    }
}
