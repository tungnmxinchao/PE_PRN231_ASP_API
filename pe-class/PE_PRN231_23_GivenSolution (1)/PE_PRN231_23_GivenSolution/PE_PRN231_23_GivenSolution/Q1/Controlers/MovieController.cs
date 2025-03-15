using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Q1.DTO;
using Q1.Models;

namespace Q1.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly PE_PRN_Fall2023B1Context _context;

        public MovieController(PE_PRN_Fall2023B1Context context)
        {
            _context = context;
        }

        [EnableQuery]
        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> Get()
        {
            var movies = _context.Movies.Include(d => d.Director)
                .Include(g => g.Genres)
                .Select( x => new MovieResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Year = x.Year,
                    Description = x.Description,
                    DirectorId = x.DirectorId,
                    DirectorName = x.Director.Name,

                    Genres = x.Genres.Select(g => new GenerResponse
                    {
                        Title = g.Title,
                        Description = g.Description
                    }).ToList()

                });
            

            return Ok(movies);
        }

        [HttpGet("GetMoviesByGenre/{genre}")]
        public async Task<IActionResult> GetByGenre(String genre)
        {

            var movies = _context.Movies
                .Include(g => g.Genres)
                .Where(m => m.Genres.Any(g => g.Title.Equals(genre)))
                .Select(x => new GetMovieByGenreResponsecs
                {
                    Id = x.Id,
                    Title = x.Title,
                    Year = x.Year,
                    Description = x.Description,

                    Genres = x.Genres.Select(g => new GenerResponse
                    {
                        Title = g.Title,
                        Description = g.Description
                    }).ToList()

                }).ToList();

            return Ok(movies);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddRequest add)
        {

            try
            {

                if (add.StartDate >= add.EndDate)
                {
                    throw new Exception("Error! Start Date have to > EndDate");
                }

                var schedules = _context.Schedules
                    .Where(x => x.MovieId != add.MovieId && 
                    x.TimeSlotId == add.TimeSlotId &&
                    x.RoomId == add.RoomId)
                    .ToList();
                if(schedules.Count > 0)
                {
                    throw new Exception("Exist slot");
                }

            }catch(Exception ex)
            {
                ApiResponse response = new ApiResponse()
                {
                    Code = (int)HttpStatusCode.Conflict,
                    Message = ex.GetBaseException().Message
                };

                return Conflict(response);
            }

            Schedule schedule = new Schedule
            {
                MovieId = add.MovieId,
                RoomId = add.RoomId,
                TimeSlotId = add.TimeSlotId,
                StartDate = add.StartDate,
                EndDate = add.EndDate,
            };

            _context.Schedules.Add(schedule);
            _context.SaveChanges();

            return Ok(schedule);
        }
    }
}
