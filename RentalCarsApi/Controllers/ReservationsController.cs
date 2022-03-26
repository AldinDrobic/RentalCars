using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;

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
        /// Get a list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();

            return Ok(reservations);
        }

        /// <summary>
        /// Get specific reserveation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservations = await _context.Reservations.FindAsync(id);

            if (reservations == null)
                return NotFound();

            return Ok(reservations);
        }

        /// <summary>
        /// Create new reservation
        /// </summary>
        /// <param name="reservation">Reservation class that is used for creating new reservation object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
        {
            if (reservation == null)
                return BadRequest();

            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new {id = reservation.Id}, reservation);
        }



    }
}
