using Microsoft.AspNetCore.Mvc;

namespace Q2.Controllers
{
    public class OrderListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
