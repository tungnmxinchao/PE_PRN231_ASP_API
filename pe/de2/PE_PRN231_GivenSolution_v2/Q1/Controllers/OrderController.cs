using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.Dtos;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PE_PRN231_Sum23B5Context _context;
        private readonly IMapper _mapper;

        public OrderController(PE_PRN231_Sum23B5Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {

            var order = _context.Orders               
                .Include(c => c.Customer)
                .Include(e => e.Employee).ThenInclude(d => d.Department)
                .ToList();

            var orderResponse = _mapper.Map<List<OrderResponse>>(order);

            return Ok(orderResponse);
        }

        [HttpGet("GetOrderByDate/{From}/{To}")]
        public async Task<IActionResult> GetOrderByDate(DateTime From,
            DateTime To)
        {
            var order = _context.Orders
                .Include(c => c.Customer)
                .Include(e => e.Employee).ThenInclude(d => d.Department)
                .Where(x => x.OrderDate >= From && x.OrderDate <= To)
                .ToList();

            var orderResponse = _mapper.Map<List<OrderResponse>>(order);

            return Ok(orderResponse);
        }
    }
}
