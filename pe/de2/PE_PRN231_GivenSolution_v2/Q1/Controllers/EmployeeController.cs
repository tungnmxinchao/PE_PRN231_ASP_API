using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PE_PRN231_Sum23B5Context _context;

        public EmployeeController(PE_PRN231_Sum23B5Context context)
        {
            _context = context;
        }

        [HttpPost("Delete/{EmployeeId}")]
        public async Task<IActionResult> Delete(int EmployeeId)
        {
            var employee = await _context.Employees
                .Include(x => x.Orders)
                .ThenInclude(o => o.OrderDetails)
                .FirstOrDefaultAsync(x => x.EmployeeId == EmployeeId);

            if (employee == null)
            {
                return NotFound();
            }

  
            var orders = employee.Orders.ToList();

   
            int totalOrders = orders.Count;
            int totalOrderDetails = orders.Sum(o => o.OrderDetails.Count);

   
            foreach (var order in orders)
            {
                _context.OrderDetails.RemoveRange(order.OrderDetails);
            }

  
            _context.Orders.RemoveRange(orders);

            _context.Employees.Remove(employee);

            int rowAff = await _context.SaveChangesAsync();

            return Ok(new
            {
                EmployeeDeletedCount = rowAff,
                OrderDeleted = totalOrders,
                TotalOrderDetails = totalOrderDetails
            });
        }
    }
}
