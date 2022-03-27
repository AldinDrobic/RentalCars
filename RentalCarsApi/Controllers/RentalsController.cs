using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Car;
using RentalCarsApi.Models.DTO.Rental;
using RentalCarsApi.Services;

namespace RentalCarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController: ControllerBase
    {
        private readonly RentalCarsDbContext _context;
        private readonly IMapper _mapper;
        private readonly CarsController _carsController;

        public RentalsController(RentalCarsDbContext context, IMapper mapper, CarsController carsController)
        {
            _context = context;
            _mapper = mapper;
            _carsController = carsController;
        }

   

        ///// <summary>
        ///// Get a list of all rentals
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<RentalReadDTO>>> GetRentals()
        //{
        //    var rentals = _mapper.Map<List<RentalReadDTO>>(await _context.Rentals
        //        .Include(r => r.Car)
        //        .ToListAsync());

        //    return Ok(rentals);
        //}

        /// <summary>
        /// Get specific reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rental == null)
                return NotFound();

            return Ok(rental);
        }

        /// <summary>
        /// Create new rental
        /// </summary>
        /// <param name="dtoRental">Rental class that is used for creating new rental object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<RentalReadDTO>> CreateRental(RentalCreateDTO dtoRental)
        {
            if (dtoRental == null)
                return BadRequest();
            if (!CarExists(dtoRental.CarId))
                return NotFound();

            CarEditDTO dtoCar = _mapper.Map<CarEditDTO>(await _context.Cars.FindAsync(dtoRental.CarId));
            //If car is already rented
            if (dtoCar.IsRented)
                return BadRequest();

            dtoCar.IsRented = true;
            Rental domainRental = _mapper.Map<Rental>(dtoRental);

            try
            {
                await _context.Rentals.AddAsync(domainRental);
                await _carsController.UpdateCar(domainRental.CarId, dtoCar);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetReservation", new { id = domainRental.Id }, _mapper.Map<RentalReadDTO>(domainRental));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalById(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
                return NotFound();
            CarEditDTO dtoCar = _mapper.Map<CarEditDTO>(await _context.Cars.FindAsync(rental.CarId));
            dtoCar.IsRented = false;
            _context.Rentals.Remove(rental);

            try
            {
                await _carsController.UpdateCar(rental.CarId, dtoCar);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("car/{id}")]
        public async Task<IActionResult> DeleteRentalByCarId(int carId)
        {
            var rental = await _context.Rentals
                .Where(r => r.CarId == carId)
                .FirstOrDefaultAsync();
            if (rental == null)
                return NotFound();

            CarEditDTO dtoCar = _mapper.Map<CarEditDTO>(await _context.Cars.FindAsync(rental.CarId));
            dtoCar.IsRented = false;
            _context.Rentals.Remove(rental);

            try
            {
                await _carsController.UpdateCar(rental.CarId, dtoCar);
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
