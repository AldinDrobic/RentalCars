﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalCarsApi.Services.CarServices;
using RentalCarsApi.Services.ReservationServices;

namespace RentalCarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsVController
    {
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;

        public RentalsVController(IMapper mapper, IReservationService reservationService, ICarService carService)
        {
            _mapper = mapper;
            _reservationService = reservationService;
            _carService = carService;
        }

    }
}
