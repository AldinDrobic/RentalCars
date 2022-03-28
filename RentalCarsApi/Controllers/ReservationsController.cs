using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Car;
using RentalCarsApi.Models.DTO.Reservation;
using RentalCarsApi.Controllers;

namespace RentalCarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController: ControllerBase
    {      
        private readonly RentalCarsDbContext _context;
        private readonly IMapper _mapper;

        public ReservationsController(RentalCarsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ReservationReadDTO>>> GetReservations()
        {
            var reservations = _mapper.Map<List<ReservationReadDTO>>(await _context.Reservations
                .Include(r => r.Car)
                .ToListAsync());

            return Ok(reservations);
        }


        /// <summary>
        /// Get specific reserveation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationReadDTO>> GetReservation(int id)
        {
            var reservation = _mapper.Map<ReservationReadDTO>(await _context.Reservations
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == id));

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        /// <summary>
        /// Get specific reserveation by carId
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpGet("car/{carId}")]
        public async Task<ActionResult<ReservationReadDTO>> GetReservationByCarId(int carId)
        {

            var reservation = _mapper.Map<ReservationReadDTO>(await _context.Reservations
                .Where(r => r.CarId == carId)
                .FirstOrDefaultAsync());

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
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
            if(!CarExists(dtoReservation.CarId))
                return NotFound();

            CarEditDTO dtoCar = _mapper.Map<CarEditDTO>(await _context.Cars.FindAsync(dtoReservation.CarId));
            //If car is already rented
            if(dtoCar.IsRented)
                return BadRequest();

            dtoCar.IsRented = true;
            Reservation domainReservation = _mapper.Map<Reservation>(dtoReservation);

            try
            {
                await _context.Reservations.AddAsync(domainReservation);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
            return CreatedAtAction("GetReservation", new {id = domainReservation.Id}, _mapper.Map<ReservationReadDTO>(domainReservation));
        }

        /// <summary>
        /// Delete specific reservation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservationById(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound();

            CarEditDTO dtoCar = _mapper.Map<CarEditDTO>(await _context.Cars.FindAsync(reservation.CarId));
            dtoCar.IsRented = false;
            _context.Reservations.Remove(reservation);

            try
            {
                await _context.SaveChangesAsync();             
            }
            catch (Exception)
            {
                return BadRequest();
            } 

            return NoContent();
        }

        /// <summary>
        /// Delete specific reservation by carId
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpDelete("car/{id}")]
        public async Task<IActionResult> DeleteReservationByCarId(int carId)
        {
            var reservation = await _context.Reservations
                .Where(r => r.CarId == carId)
                .FirstOrDefaultAsync();
            if (reservation == null)
                return NotFound();

            CarEditDTO dtoCar = _mapper.Map<CarEditDTO>(await _context.Cars.FindAsync(reservation.CarId));
            dtoCar.IsRented = false;
            _context.Reservations.Remove(reservation);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Return bool based on if car exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

    }
}
