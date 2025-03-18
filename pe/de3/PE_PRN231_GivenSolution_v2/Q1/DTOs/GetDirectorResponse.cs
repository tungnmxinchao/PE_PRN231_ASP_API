using Q1.Models;

namespace Q1.DTOs
{
    public class GetDirectorResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Male { get; set; }
        public DateTime Dob { get; set; }
        public string DobString { get; set; }
        public string Nationality { get; set; } = null!;
        public string Description { get; set; } = null!;     

        public virtual ICollection<MovieResponse> Movies { get; set; }
    }

}
