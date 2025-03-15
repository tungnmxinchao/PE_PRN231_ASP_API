namespace Q1.DTO
{
    public class GetMovieByGenreResponsecs
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? Year { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<GenerResponse> Genres { get; set; }
    }
}
