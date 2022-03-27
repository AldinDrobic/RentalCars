using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Reservation;

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
            var reservations = _mapper.Map<List<ReservationReadDTO>>(await _context.Reservations
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == id));

            if (reservations == null)
                return NotFound();

            return Ok(reservations);
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
            Reservation domainReservation = _mapper.Map<Reservation>(dtoReservation);

            await _context.Reservations.AddAsync(domainReservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new {id = domainReservation.Id}, _mapper.Map<Reservation>(domainReservation));
        }



    }
}
