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
    public class ReservationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;

        public ReservationsController(IMapper mapper, IReservationService reservationService, ICarService carService)
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
        public async Task<IActionResult> GetReservations()
        {
            return Ok(_mapper.Map<List<ReservationReadDTO>>(await _reservationService.GetReservations()));
        }

        /// <summary>
        /// Get specific reserveation by carId
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpGet("car/{carId}")]
        public async Task<IActionResult> GetReservationByCarId(int carId)
        {
            return Ok((_mapper.Map<ReservationReadDTO>(await _reservationService.GetReservationByCarId(carId))));
        }

        /// <summary>
        /// Get specific reserveation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            return Ok((_mapper.Map<ReservationReadDTO>(await _reservationService.GetReservationById(id))));
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
            if (!_carService.CarExists(dtoReservation.CarId))
                return NotFound();

            Car domainCar = _mapper.Map<Car>(await _carService.GetCar(dtoReservation.CarId));
            if (domainCar.IsRented)
                return BadRequest();

            await _carService.SetCarAvailability(domainCar);
            Reservation domainReservation = _mapper.Map<Reservation>(dtoReservation);

            await _reservationService.AddReservationToDatabase(domainReservation);
            await _carService.UpdateCar(dtoReservation.CarId, domainCar);

            return CreatedAtAction("GetReservationById", new { id = domainReservation.Id }, _mapper.Map<ReservationReadDTO>(domainReservation));
        }

        /// <summary>
        /// Deletes a reservation from database by reservation id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("reservationId/{id}")]
        public async Task<IActionResult> DeleteReservationById(int id)
        {
            //Set car status to available
            Reservation reservation = await _reservationService.GetReservationById(id);
            Car car = await _carService.GetCar(reservation.CarId);
            await _carService.SetCarAvailability(car);

            //Delete rental
            await _reservationService.DeleteReservationById(id);
            return NoContent();
        }

        /// <summary>
        /// Deletes a reservation from database by car id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("carId/{id}")]
        public async Task<IActionResult> DeleteReservationByCarId(int id)
        {
            //Set car status to available
            Reservation reservation = await _reservationService.GetReservationByCarId(id);
            Car car = await _carService.GetCar(reservation.CarId);
            await _carService.SetCarAvailability(car);

            //Delete rental
            await _reservationService.DeleteReservationByCarId(id);
            return NoContent();
        }

        /// <summary>
        /// Update a specific reservation by id
        /// </summary>
        /// <param name="id">Reservation objects identifier</param>
        /// <param name="dtoReservation">Reservation dto object that arrives from body</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation(int id, ReservationEditDTO dtoReservation)
        {
            Reservation domainReservation = _mapper.Map<Reservation>(dtoReservation);
            await _reservationService.UpdateReservation(id, domainReservation);

            return CreatedAtAction("CreateReservation",
                new { id = domainReservation.Id },
                _mapper.Map<ReservationReadDTO>(domainReservation));
        }
    }
}
