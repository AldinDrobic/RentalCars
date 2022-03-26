using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;

namespace RentalCarsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController: ControllerBase
    {
        private readonly RentalCarsDbContext _context;
        private readonly IMapper _mapper;

        public PricesController(RentalCarsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all prices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Price>>> GetPrices()
        {
            var prices = await _context.Prices.ToListAsync();

            return Ok(prices);
        }

        /// <summary>
        /// Get specific price by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Price>> GetPrice(int id)
        {
            var price = await _context.Prices.FindAsync(id);

            if (price == null)
                return BadRequest();

            return Ok(price);
        }

        /// <summary>
        /// Create new price
        /// </summary>
        /// <param name="price">Price class that is used for creating new price object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreatePrice(Price price)
        {
            if (price == null)
                return BadRequest();

            await _context.Prices.AddAsync(price);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new {id = price.Id}, price);
        }
    }
}
