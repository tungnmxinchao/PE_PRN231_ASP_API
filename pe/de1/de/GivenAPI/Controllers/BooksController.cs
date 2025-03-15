using Microsoft.AspNetCore.Mvc;
using GivenAPI.Models;

namespace GivenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(DataInitializer.Books
                .Select(b => new { 
                    b.BookID, 
                    b.Title, 
                    b.Publisher, 
                    b.PublicationYear, 
                    Authors = b.Authors.Select(a => new { a.AuthorID, a.Name, a.Bio})
                })
                );
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            book.BookID = DataInitializer.Books.Count + 1;
            DataInitializer.Books.Add(book);
            return Ok(new { message = "Book created successfully", bookId = book.BookID });
        }

        [HttpDelete("{bookId}")]
        public IActionResult DeleteBook(int bookId)
        {
            var book = DataInitializer.Books.FirstOrDefault(b => b.BookID == bookId);
            if (book == null) return NotFound(new { message = "Book not found" });

            DataInitializer.Books.Remove(book);
            return Ok(new { message = "Book deleted successfully" });
        }
    }
}
