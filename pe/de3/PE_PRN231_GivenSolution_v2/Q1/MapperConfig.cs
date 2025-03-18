using AutoMapper;
using Q1.DTOs;
using Q1.Models;

namespace Q1
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Director, DirectorsResponse>()
                .ForMember(dest => dest.Male,
                opt => opt.MapFrom(src => src.Male == true ? "male" : "female"))
                .ForMember(dest => dest.DobString,
                opt => opt.MapFrom(src => src.Dob.ToString("d/M/yyyy")));

            CreateMap<Director, GetDirectorResponse>()
                .ForMember(dest => dest.Male,
                opt => opt.MapFrom(src => src.Male == true ? "male" : "female"))
                .ForMember(dest => dest.DobString,
                opt => opt.MapFrom(src => src.Dob.ToString("d/M/yyyy")));

            CreateMap<Movie, MovieResponse>()
                .ForMember(dest => dest.ProducerName,
                opt => opt.MapFrom(src => src.Producer.Name))
                .ForMember(dest => dest.DirectorName,
                opt => opt.MapFrom(src => src.Director.FullName));

            CreateMap<Director , DirectorCreate>().ReverseMap();



        }
    }
}
