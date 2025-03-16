namespace GivenAPI.Models
{
    public class DataInitializer
    {
        public static List<Book> Books { get;  set; }
        public static List<Author> Authors { get;  set; }

        static DataInitializer()
        {
            // Initialize the authors list
            Authors = new List<Author>
            {
                new Author { AuthorID = 1, Name = "Robert C. Martin", Bio = "Software engineer and author" },
                new Author { AuthorID = 2, Name = "Andy Hunt", Bio = "Software consultant and author" },
                new Author { AuthorID = 3, Name = "Dave Thomas", Bio = "Software engineer and consultant" }
            };

            // Initialize the books list with reference to authors
            Books = new List<Book>
            {
                new Book
                {
                    BookID = 1,
                    Title = "Clean Code",
                    Publisher = "Prentice Hall",
                    PublicationYear = 2008,
                    Authors = new List<Author> { Authors[0] } // Robert C. Martin
                },
                new Book
                {
                    BookID = 2,
                    Title = "The Pragmatic Programmer",
                    Publisher = "Addison-Wesley",
                    PublicationYear = 1999,
                    Authors = new List<Author> { Authors[1], Authors[2] } // Andy Hunt, Dave Thomas
                }
            };

            // Set the reference from authors to books
            Authors[0].Books = new List<Book> { Books[0] }; // Clean Code by Robert C. Martin
            Authors[1].Books = new List<Book> { Books[0], Books[1] }; // The Pragmatic Programmer by Andy Hunt
            Authors[2].Books = new List<Book> { Books[1] }; // The Pragmatic Programmer by Dave Thomas
        }
    }
}
