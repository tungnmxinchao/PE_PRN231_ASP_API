using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Q1.DTOs;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDBContext _context;
        private readonly IMapper _mapper;

        public BooksController(LibraryDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = _context.Books.Include(a => a.Authors).ToList();

            var booksResponse = _mapper.Map<List<BookResponse>>(books);
            return Ok(booksResponse);
        }
        [HttpGet("search/{title}/{author}")]
        public async Task<IActionResult> Search(string title, string author)
        {
            var booksQuery = _context.Books
                .Include(b => b.Authors)
                .Where(b => (title == "*" || b.Title.Contains(title)) &&
                    (author == "*" || b.Authors.Any(a => a.Name.Contains(author))));

            var booksResponse = _mapper.Map<List<BookResponse>>(await booksQuery.ToListAsync());
            return Ok(booksResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookCopies)
                .ThenInclude(b => b.BorrowTransactions)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            int deletedCopies = book.BookCopies.Count;
            int deletedBorrows = book.BookCopies.Sum(copy => copy.BorrowTransactions.Count);


            _context.Books.Remove(book);
            await _context.SaveChangesAsync();


            return Ok(new
            {
                message = "Book deleted successfully",
                deleteCopy = deletedCopies,
                deletedTransactions = deletedBorrows
            });

        }
    }
}
