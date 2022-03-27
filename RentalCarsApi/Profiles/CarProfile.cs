using AutoMapper;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Car;

namespace RentalCarsApi.Profiles
{
    public class CarProfile: Profile
    {
        public CarProfile()
        {

            CreateMap<Car, CarReadDTO>()
                .ForMember(cdto => cdto.CategoryId, opt => opt
                .MapFrom(c => c.CategoryId))
                .ReverseMap();

            CreateMap<Car, CarCreateDTO>()
                .ForMember(cdto => cdto.CategoryId, opt => opt
                .MapFrom(c => c.CategoryId))
                .ReverseMap();

            CreateMap<Car, CarEditDTO>()
                .ForMember(cdto => cdto.CategoryId, opt => opt
                .MapFrom(c => c.CategoryId))
                .ReverseMap();

        }
    }
}
