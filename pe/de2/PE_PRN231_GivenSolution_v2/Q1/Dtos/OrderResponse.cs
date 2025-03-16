using Q1.Models;

namespace Q1.Dtos
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        public string CustomerId { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }


    }

    public class CustomerResponse
    {
        public string CustomerId { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
    }

    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;

        public virtual DepartmentResponse? Department { get; set; }

    }

    public class DepartmentResponse
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
