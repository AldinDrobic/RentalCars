using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalCarsApi.Models.DTO.Car;
using RentalCarsApi.Services;

namespace RentalCarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsVController
    {
        private readonly IMapper _mapper;
        private readonly ICarService _carService;

        public RentalsVController(IMapper mapper, ICarService carService)
        {
            _mapper = mapper;
            _carService = carService;
        }

    }
}
