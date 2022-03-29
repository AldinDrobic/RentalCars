using AutoMapper;
using RentalCarsApi.Models.DTO.Rental;

namespace RentalCarsApi.Profiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<Rental, RentalReadDTO>()
                .ForMember(rdto => rdto.CarId, opt => opt
                .MapFrom(r => r.CarId))
                .ReverseMap();

            CreateMap<Rental, RentalCreateDTO>()
                .ForMember(rdto => rdto.CarId, opt => opt
                .MapFrom(c => c.CarId))
                .ReverseMap();

            CreateMap<Rental, RentalEditDTO>()
                .ForMember(rdto => rdto.CarId, opt => opt
                .MapFrom(c => c.CarId))
                .ReverseMap();

            CreateMap<Rental, RentalEndDTO>()
                .ForMember(rdto => rdto.CarId, opt => opt
                .MapFrom(c => c.CarId))
                .ReverseMap();
        }
    }
}
