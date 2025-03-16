using AutoMapper;
using Q1.Dtos;
using Q1.Models;

namespace Q1
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Order, OrderResponse>()
                 .ForMember(dest => dest.CompanyName, 
                    opt => opt.MapFrom(src => src.Customer.CompanyName))
                 .ForMember(dest => dest.FullName, 
                    opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
                 .ForMember(dest => dest.DepartmentId,
                    opt => opt.MapFrom(src => src.Employee.DepartmentId))
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Employee.Department.DepartmentName));

        }
    }
}
