using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;

namespace RentalCarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController: ControllerBase
    {
        private readonly RentalCarsDbContext _context;
        private readonly IMapper _mapper;

        public RentalsController(RentalCarsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all rentals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Rental>>> GetRentals()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Car)
                .ToListAsync();

            return Ok(rentals);
        }

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
        /// <param name="rental">Rental class that is used for creating new rental object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateRental(Rental rental)
        {
            if (rental == null)
                return BadRequest();

            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental", new {id = rental.Id}, rental);
        }

    }
}
