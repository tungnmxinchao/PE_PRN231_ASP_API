namespace GivenAPI.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}
