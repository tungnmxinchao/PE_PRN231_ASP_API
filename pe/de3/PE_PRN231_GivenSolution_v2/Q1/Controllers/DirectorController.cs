using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DTOs;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly PE_PRN_Fall22B1Context _context;
        private readonly IMapper _mapper;

        public DirectorController(PE_PRN_Fall22B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetDirectors/{nationality}/{gender}")]
        public async Task<IActionResult> GetDirectors(string nationality, string gender)
        {
            var directors = _context.Directors
                .Where(n => n.Nationality.ToLower().Equals(nationality.ToLower()) 
                && (gender.Equals("male") ? n.Male == true : n.Male == false));

            var directorResponse = _mapper.Map<List<DirectorsResponse>>(directors);
            return Ok(directorResponse);
        }

        [HttpGet("GetDirector/{id}")]
        public async Task<IActionResult> GetDirector(int id)
        {
            var director = _context.Directors
                .Include(m => m.Movies).ThenInclude(p => p.Producer)
                .FirstOrDefault(x => x.Id == id);

            var directorResposne = _mapper.Map<GetDirectorResponse>(director);
          
            return Ok(directorResposne);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DirectorCreate director)
        {
            try
            {
                var directorAdd = _mapper.Map<Director>(director);
                _context.Directors.Add(directorAdd);
                int rowAff = _context.SaveChanges();
                return Ok(rowAff);
            }
            catch
            {
                return Conflict("There is an error while adding");
            }

           
        }
    }
}
