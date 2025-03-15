using Q1.Models;

namespace Q1.DTO
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? Year { get; set; }
        public string? Description { get; set; }
        public int DirectorId { get; set; }
        public string DirectorName { get; set; }

        public virtual ICollection<GenerResponse> Genres { get; set; }


    }

    public class GenerResponse
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }

   
}
