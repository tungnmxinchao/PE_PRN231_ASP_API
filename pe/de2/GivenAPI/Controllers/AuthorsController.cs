using Microsoft.AspNetCore.Mvc;
using GivenAPI.Models;

namespace GivenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok(DataInitializer.Authors
                .Select(a => new {
                    a.AuthorID,
                    a.Name,
                    a.Bio,
                    Books = a.Books.Select(b => new { b.BookID, b.Title, b.Publisher, b.PublicationYear })
                })
                );
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] Author author)
        {
            author.AuthorID = DataInitializer.Authors.Count + 1;
            DataInitializer.Authors.Add(author);
            return Ok(new { message = "Author created successfully", authorId = author.AuthorID });
        }

        [HttpDelete("{authorId}")]
        public IActionResult DeleteAuthor(int authorId)
        {
            var author = DataInitializer.Authors.FirstOrDefault(a => a.AuthorID == authorId);
            if (author == null) return NotFound(new { message = "Author not found" });

            DataInitializer.Authors.Remove(author);
            return Ok(new { message = "Author deleted successfully" });
        }
    }
}
