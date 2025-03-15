using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GivenAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Q2.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient = null;

        private string baseUrl = "";

        public BooksController()
        {
            _httpClient = new HttpClient();

            var contenType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contenType);

            baseUrl = "http://localhost:5100/api/Books/";
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Title, 
            string Publisher, int PublicationYear, int AuthorId)
        {
            var loadAuthors = await LoadAllAuthors();

            if (string.IsNullOrWhiteSpace(Title))
            {
                ModelState.AddModelError("Title", "Title is required.");
            }
            if (string.IsNullOrWhiteSpace(Publisher))
            {
                ModelState.AddModelError("Publisher", "Publisher is required.");
            }
            if (PublicationYear <= 0)
            {
                ModelState.AddModelError("PublicationYear", "Publication Year must be greater than 0.");
            }
            if (AuthorId <= 0)
            {
                ModelState.AddModelError("AuthorId", "Please select a valid author.");
            }

            if (!ModelState.IsValid)
            {
                return View(loadAuthors);
            }

            var author = await FindAuthorById(AuthorId);
            List<Author> authors = new List<Author>();
            authors.Add(author);


            Book book = new Book()
            {
                BookID = PublicationYear,
                Title = Title,
                Publisher = Publisher,
                PublicationYear = PublicationYear,
                Authors = authors
            };

            bool result = await CreateBook(book);

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error adding book.");
                return View(loadAuthors);
            }
        }

        private async Task<bool> CreateBook(Book book)
        {
            string url = "http://localhost:5100/api/Books";

            // Chuyển đối tượng book thành JSON
            var json = JsonSerializer.Serialize(book, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Gửi request POST
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }

        private async Task<Author> FindAuthorById(int authorId)
        {
            baseUrl = "http://localhost:5100/api/Authors";
            HttpResponseMessage response = _httpClient.GetAsync(baseUrl).Result;

            string data = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(data);

            JsonElement root = doc.RootElement;

            var items = root.EnumerateArray()
                .Select(x => new GivenAPI.Models.Author
                {
                    AuthorID = x.GetProperty("authorID").GetInt32(),
                    Name = x.GetProperty("name").GetString(),
                    Bio = x.GetProperty("bio").GetString(),
                    Books = x.GetProperty("books").EnumerateArray()
                        .Select(a => new GivenAPI.Models.Book
                        {
                            BookID = a.GetProperty("bookID").GetInt32(),
                            Title = a.GetProperty("title").GetString(),
                            Publisher = a.GetProperty("publisher").GetString(),
                            PublicationYear = a.GetProperty("publicationYear").GetInt32(),
                        }).ToList()

                }).FirstOrDefault(x => x.AuthorID == authorId);

            return items;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            

            var authors = await LoadAllAuthors();

            return View(authors);
        }

        private async Task<List<Author>> LoadAllAuthors()
        {
            baseUrl = "http://localhost:5100/api/Authors";
            HttpResponseMessage response = _httpClient.GetAsync(baseUrl).Result;

            string data = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(data);

            JsonElement root = doc.RootElement;

            var items = root.EnumerateArray()
                .Select(x => new GivenAPI.Models.Author
                {
                    AuthorID = x.GetProperty("authorID").GetInt32(),
                    Name = x.GetProperty("name").GetString(),
                    Bio = x.GetProperty("bio").GetString()

                }).ToList();

            return items;
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = _httpClient.GetAsync(baseUrl).Result;

            string data = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(data);

            JsonElement root = doc.RootElement;

            var items = root.EnumerateArray()
                .Select(x => new GivenAPI.Models.Book
                {
                    BookID = x.GetProperty("bookID").GetInt32(),
                    Title = x.GetProperty("title").GetString(),
                    Publisher = x.GetProperty("publisher").GetString(),
                    PublicationYear = x.GetProperty("publicationYear").GetInt32(),
                    Authors = x.GetProperty("authors").EnumerateArray()
                        .Select(a => new GivenAPI.Models.Author
                        {
                            AuthorID = a.GetProperty("authorID").GetInt32(),
                            Name = a.GetProperty("name").GetString(),
                            Bio = a.GetProperty("bio").GetString()
                        }).ToList()

                }).ToList();

            return View(items);
        }
    }
}
