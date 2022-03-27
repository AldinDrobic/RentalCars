using AutoMapper;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Category;

namespace RentalCarsApi.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryCreateDTO>()
                .ReverseMap();
            CreateMap<Category, CategoryReadDTO>()
                .ReverseMap();
        }
    }
}
