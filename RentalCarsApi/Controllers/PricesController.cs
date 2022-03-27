using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Price;

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
        public async Task<ActionResult<List<PriceReadDTO>>> GetPrices()
        {
            var prices = _mapper.Map<List<PriceReadDTO>>(await _context.Prices.ToListAsync());

            return Ok(prices);
        }

        /// <summary>
        /// Get specific price by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceReadDTO>> GetPrice(int id)
        {
            var price = await _context.Prices.FindAsync(id);

            if (price == null)
                return BadRequest();

            return Ok(_mapper.Map<PriceReadDTO>(price));
        }

        /// <summary>
        /// Create new price
        /// </summary>
        /// <param name="dtoPrice">Price class that is used for creating new price object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PriceReadDTO>> CreatePrice(PriceCreateDTO dtoPrice)
        {
            if (dtoPrice == null)
                return BadRequest();
            Price domainPrice = _mapper.Map<Price>(dtoPrice);

            await _context.Prices.AddAsync(domainPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new {id = domainPrice.Id}, _mapper.Map<PriceReadDTO>(domainPrice));
        }
    }

}
