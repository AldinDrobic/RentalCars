using AutoMapper;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Reservation;

namespace RentalCarsApi.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationCreateDTO>()
                .ForMember(rdto => rdto.CarId, opt => opt
                .MapFrom(r => r.CarId))
                .ReverseMap();

            CreateMap<Reservation, ReservationReadDTO>()
                .ForMember(rdto => rdto.CarId, opt => opt
                .MapFrom(r => r.CarId))
                .ReverseMap();

            CreateMap<Reservation, ReservationEditDTO>()
               .ForMember(rdto => rdto.CarId, opt => opt
               .MapFrom(r => r.CarId))
               .ReverseMap();
        }
    }
}
