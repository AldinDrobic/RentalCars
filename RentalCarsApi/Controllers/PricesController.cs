using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Price;
using RentalCarsApi.Services.PriceServices;

namespace RentalCarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPriceService _priceService;

        public PricesController(IMapper mapper, IPriceService priceService)
        {
            _mapper = mapper;
            _priceService = priceService;
        }
        public PricesController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        /// <summary>
        /// Get a list of all prices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceReadDTO>>> GetPrices()
        {
            return Ok(_mapper.Map<List<PriceReadDTO>>(await _priceService.GetPrices()));
        }

        /// <summary>
        /// Get specific price by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceReadDTO>> GetPrice(int id)
        {
            return Ok(_mapper.Map<PriceReadDTO>(await _priceService.GetPrice(id)));
        }

        /// <summary>
        /// Create new price 
        /// </summary>
        /// <param name="dtoPrice">Price class that is used for creating new price object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PriceReadDTO>> CreateCategory(PriceCreateDTO dtoPrice)
        {

            Price domainPrice = _mapper.Map<Price>(dtoPrice);

            await _priceService.CreatePrice(domainPrice);

            return CreatedAtAction("GetPrice",
                new { id = domainPrice.Id },
                _mapper.Map<PriceReadDTO>(domainPrice));
        }

        /// <summary>
        /// Deletes a price from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(int id)
        {
            if (!_priceService.PriceExists(id))
                return NotFound();

            await _priceService.DeletePrice(id);

            return NoContent();
        }

        /// <summary>
        /// Update a specific price by id
        /// </summary>
        /// <param name="id">Price objects identifier</param>
        /// <param name="dtoPrice">Price dto object that arrives from body</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, PriceEditDTO dtoPrice)
        {
            Price domainPrice = _mapper.Map<Price>(dtoPrice);
            await _priceService.UpdatePrice(id, domainPrice);

            return CreatedAtAction("GetPrice",
                new { id = domainPrice.Id },
                _mapper.Map<PriceReadDTO>(domainPrice));
        }
    }
}
