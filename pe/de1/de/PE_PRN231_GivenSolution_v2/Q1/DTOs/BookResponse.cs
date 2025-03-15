using Q1.Models;

namespace Q1.DTOs
{
    public class BookResponse
    {
        public int BookId { get; set; }
        

        public virtual ICollection<AuthorResponse> Authors { get; set; }

        public string Title { get; set; } = null!;
        public string? Publisher { get; set; }
        public int? PublicationYear { get; set; }
    }

    public class AuthorResponse
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }

    }
}
