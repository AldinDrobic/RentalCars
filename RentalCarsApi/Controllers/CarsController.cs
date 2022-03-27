using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Car;

namespace RentalCarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController: ControllerBase
    {
        private readonly RentalCarsDbContext _context;
        private readonly IMapper _mapper;
        private readonly RentalsController _rentalsController;
        private readonly ReservationsController _reservationsController;

        public CarsController(RentalCarsDbContext context, IMapper mapper, RentalsController rentalsController, ReservationsController reservationsController)
        {
            _context = context;
            _mapper = mapper;
            _rentalsController = rentalsController;
            _reservationsController = reservationsController;
        }

        /// <summary>
        /// Get a list of all cars
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarReadDTO>>> GetCars()
        {
            var cars = _mapper.Map<List<CarReadDTO>>(await _context.Cars
                .Include(c => c.Category)
                .ToListAsync());

            return Ok(cars);
        }

        /// <summary>
        /// Get specific car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CarReadDTO>> GetCar(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
                return NotFound();

            return Ok(_mapper.Map<CarReadDTO>(car));
        }

        /// <summary>
        /// Create new car 
        /// </summary>
        /// <param name="dtoCar">Car class that is used for creating new car object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CarReadDTO>> CreateCar(CarCreateDTO dtoCar)
        {
            if (dtoCar == null)
            {
                return BadRequest();
            }

            Car domainCar = _mapper.Map<Car>(dtoCar);

            await _context.Cars.AddAsync(domainCar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", 
                new {id = domainCar.Id}, 
                _mapper.Map<CarReadDTO>(domainCar));
        }

        /// <summary>
        /// Update a specific car by id
        /// </summary>
        /// <param name="id">Car objects identifier</param>
        /// <param name="dtoCar">Car dto object that arrives from body</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCar(int id, CarEditDTO dtoCar)
        {
            if(id != dtoCar.Id)
                return BadRequest();

            Car domainCar = _mapper.Map<Car>(dtoCar);
            _context.Entry(domainCar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!CarExists(id))
                    return NotFound();
            }

            return CreatedAtAction("GetCar",
                new { id = domainCar.Id },
                _mapper.Map<CarReadDTO>(domainCar));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            //Delete car rental
            await _rentalsController.DeleteRentalByCarId(car.Id);

            //Delete car reservation
            await _reservationsController.DeleteReservationByCarId(car.Id);

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

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
