using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Rental;

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
        public async Task<ActionResult<IEnumerable<RentalReadDTO>>> GetRentals()
        {
            var rentals = _mapper.Map<List<RentalReadDTO>>(await _context.Rentals
                .Include(r => r.Car)
                .ToListAsync());

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
        /// <param name="dtoRental">Rental class that is used for creating new rental object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<RentalReadDTO>> CreateRental(RentalCreateDTO dtoRental)
        {
            if (dtoRental == null)
                return BadRequest();

            Rental domainRental = _mapper.Map<Rental>(dtoRental);

            await _context.Rentals.AddAsync(domainRental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental", new {id = domainRental.Id}, _mapper.Map<RentalReadDTO>(domainRental));
        }

    }
}
