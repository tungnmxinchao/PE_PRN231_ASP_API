using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.Dtos;
using Q1.Models;

namespace Q2.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private readonly string baseUrlOrder = "";
        private readonly string baseUrlEmployee = "";

        public OrderController()
        {
            _httpClient = new HttpClient();

            var contenType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contenType);

            baseUrlOrder = "http://localhost:5000/api/Order/";
            baseUrlEmployee = "http://localhost:5000/api/Employee/";

        }

        public async Task<IActionResult> Index()
        {         

            return View();
        }

        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            HttpResponseMessage response = await _httpClient.PostAsync($"{baseUrlEmployee}Delete/{employeeId}", null);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Employee and associated orders deleted successfully.";
            }
            else
            {
                TempData["Message"] = "Error deleting employee.";
            }

            return RedirectToAction("OrderList");
        }

        public async Task<IActionResult> SearchOrders(DateTime fromDate, DateTime toDate)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrlOrder}GetOrderByDate/{fromDate:dd MMM yyyy}/{toDate:dd MMM yyyy}");

            string data = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(data);

            JsonElement root = doc.RootElement;

            var items = root.EnumerateArray()
                .Select(x => new OrderResponse
                {
                    OrderId = x.GetProperty("orderId").GetInt32(),

                    OrderDate = x.GetProperty("orderDate").ValueKind == JsonValueKind.Null ? (DateTime?)null
                   : DateTime.Parse(x.GetProperty("orderDate").GetString()),

                    RequiredDate = x.GetProperty("requiredDate").ValueKind == JsonValueKind.Null ? (DateTime?)null
                     : DateTime.Parse(x.GetProperty("requiredDate").GetString()),

                    ShippedDate = x.GetProperty("shippedDate").ValueKind == JsonValueKind.Null ? (DateTime?)null
                    : DateTime.Parse(x.GetProperty("shippedDate").GetString()),

                    Freight = x.GetProperty("freight").ValueKind == JsonValueKind.Null ? (decimal?)null
                    : x.GetProperty("freight").GetDecimal(),

                    ShipName = x.GetProperty("shipName").GetString(),
                    ShipAddress = x.GetProperty("shipAddress").GetString(),
                    ShipCity = x.GetProperty("shipCity").GetString(),

                    ShipRegion = x.GetProperty("shipRegion").ValueKind == JsonValueKind.Null ? ""
                    : x.GetProperty("shipRegion").GetString(),

                    ShipPostalCode = x.GetProperty("shipPostalCode").GetString(),
                    ShipCountry = x.GetProperty("shipCountry").GetString(),
                    CustomerId = x.GetProperty("customerId").GetString(),
                    CompanyName = x.GetProperty("companyName").GetString(),

                    EmployeeId = x.GetProperty("employeeId").ValueKind == JsonValueKind.Null ? 0
                   : x.GetProperty("employeeId").GetInt32(),

                    FullName = x.GetProperty("fullName").ValueKind == JsonValueKind.Null ? ""
                 : x.GetProperty("fullName").GetString(),

                    DepartmentId = x.GetProperty("departmentId").ValueKind == JsonValueKind.Null ? 0
                     : x.GetProperty("departmentId").GetInt32(),

                    DepartmentName = x.GetProperty("departmentName").ValueKind == JsonValueKind.Null ? ""
                       : x.GetProperty("departmentName").GetString(),
                }).ToList();

            TempData["OrderList"] = JsonSerializer.Serialize(items);

            return RedirectToAction("OrderList");
        }

        public async Task<IActionResult> OrderList()
        {
            List<OrderResponse> items = new List<OrderResponse>();

            if (TempData["OrderList"] != null)
            {
                items = JsonSerializer.Deserialize<List<OrderResponse>>(TempData["OrderList"].ToString());
            }
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(baseUrlOrder + "GetAllOrder");

                string data = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(data);

                JsonElement root = doc.RootElement;

                items = root.EnumerateArray()
                    .Select(x => new OrderResponse
                    {
                        OrderId = x.GetProperty("orderId").GetInt32(),

                        OrderDate = x.GetProperty("orderDate").ValueKind == JsonValueKind.Null ? (DateTime?)null
                       : DateTime.Parse(x.GetProperty("orderDate").GetString()),

                        RequiredDate = x.GetProperty("requiredDate").ValueKind == JsonValueKind.Null ? (DateTime?)null
                         : DateTime.Parse(x.GetProperty("requiredDate").GetString()),

                        ShippedDate = x.GetProperty("shippedDate").ValueKind == JsonValueKind.Null ? (DateTime?)null
                        : DateTime.Parse(x.GetProperty("shippedDate").GetString()),

                        Freight = x.GetProperty("freight").ValueKind == JsonValueKind.Null ? (decimal?)null
                        : x.GetProperty("freight").GetDecimal(),

                        ShipName = x.GetProperty("shipName").GetString(),
                        ShipAddress = x.GetProperty("shipAddress").GetString(),
                        ShipCity = x.GetProperty("shipCity").GetString(),

                        ShipRegion = x.GetProperty("shipRegion").ValueKind == JsonValueKind.Null ? ""
                        : x.GetProperty("shipRegion").GetString(),

                        ShipPostalCode = x.GetProperty("shipPostalCode").GetString(),
                        ShipCountry = x.GetProperty("shipCountry").GetString(),
                        CustomerId = x.GetProperty("customerId").GetString(),
                        CompanyName = x.GetProperty("companyName").GetString(),

                        EmployeeId = x.GetProperty("employeeId").ValueKind == JsonValueKind.Null ? 0
                       : x.GetProperty("employeeId").GetInt32(),

                        FullName = x.GetProperty("fullName").ValueKind == JsonValueKind.Null ? ""
                     : x.GetProperty("fullName").GetString(),

                        DepartmentId = x.GetProperty("departmentId").ValueKind == JsonValueKind.Null ? 0
                         : x.GetProperty("departmentId").GetInt32(),

                        DepartmentName = x.GetProperty("departmentName").ValueKind == JsonValueKind.Null ? ""
                           : x.GetProperty("departmentName").GetString(),
                    }).ToList();
            }

            return View(items);
        }
    }
}
