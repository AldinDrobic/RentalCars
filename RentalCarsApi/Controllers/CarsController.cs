using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;

namespace RentalCarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController: ControllerBase
    {
        private readonly RentalCarsDbContext _context;
        private readonly IMapper _mapper;

        public CarsController(RentalCarsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all cars
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();

            return Ok(cars);
        }

        /// <summary>
        /// Get specific car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
                return NotFound();

            return Ok(car);
        }

        /// <summary>
        /// Create new car 
        /// </summary>
        /// <param name="car">Car class that is used for creating new car object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateCar(Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new {id = car.Id}, car);
        }
         
    }
}
