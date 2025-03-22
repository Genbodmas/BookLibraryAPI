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
            new Book { Id = 2, Title = "Deep Work", Author = "Cal TestPort" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll() => Ok(_books);

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            book.Id = _books.Max(b => b.Id) + 1;
            _books.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }
    }
}
