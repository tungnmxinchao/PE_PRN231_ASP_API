namespace Q1.DTOs
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public string? Description { get; set; }
        public string Language { get; set; } = null!;
        public int? ProducerId { get; set; }
        public int? DirectorId { get; set; }
        public string ProducerName { get; set; }
        public string DirectorName { get; set; }

        public List<string> Geners { get; set; } = new List<string>();
        public List<string> Starts { get; set; } = new List<string>();
    }
}
