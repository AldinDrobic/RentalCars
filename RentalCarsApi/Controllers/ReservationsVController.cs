using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Car;
using RentalCarsApi.Models.DTO.Reservation;
using RentalCarsApi.Services.CarServices;
using RentalCarsApi.Services.ReservationServices;

namespace RentalCarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsVController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;

        public ReservationsVController(IMapper mapper, IReservationService reservationService, ICarService carService)
        {
            _mapper = mapper;
            _reservationService = reservationService;
            _carService = carService;
        }

        /// <summary>
        /// Get a list of all reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationReadDTO>>> GetReservations()
        {
            return _mapper.Map<List<ReservationReadDTO>>(await _reservationService.GetReservations());
        }

        /// <summary>
        /// Get specific reserveation by carId
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpGet("car/{carId}")]
        public async Task<ActionResult<ReservationReadDTO>> GetReservationByCarId(int carId)
        {
            return (_mapper.Map<ReservationReadDTO>(await _reservationService.GetReservationByCarId(carId)));
        }

        /// <summary>
        /// Get specific reserveation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationReadDTO>> GetReservationById(int id)
        {
            return (_mapper.Map<ReservationReadDTO>(await _reservationService.GetReservationById(id)));
        }

        /// <summary>
        /// Create new reservation
        /// </summary>
        /// <param name="dtoReservation">reservation>Reservation class that is used for creating new reservation object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ReservationReadDTO>> CreateReservation(ReservationCreateDTO dtoReservation)
        {
            if (dtoReservation == null)
                return BadRequest();
            if(!_carService.CarExists(dtoReservation.CarId))
                return NotFound();

            Car domainCar = _mapper.Map<Car>(await _carService.GetCar(dtoReservation.CarId));
            if(domainCar.IsRented)
                return BadRequest();

            await _carService.SetCarAvaiability(domainCar);
            Reservation domainReservation = _mapper.Map<Reservation>(dtoReservation);

            await _reservationService.AddReservationToDatabase(domainReservation);
            await _carService.UpdateCar(dtoReservation.CarId, domainCar);

            return CreatedAtAction("GetReservationById", new { id = domainReservation.Id }, _mapper.Map<ReservationReadDTO>(domainReservation));
        }
    }
}
