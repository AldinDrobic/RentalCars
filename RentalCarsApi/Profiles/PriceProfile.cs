using AutoMapper;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Price;

namespace RentalCarsApi.Profiles
{
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<Price, PriceCreateDTO>()
                .ReverseMap();

            CreateMap<Price, PriceReadDTO>()
                .ReverseMap();
        }
    }
}
