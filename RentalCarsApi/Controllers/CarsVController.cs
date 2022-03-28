using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Car;
using RentalCarsApi.Services.CarServices;
using RentalCarsApi.Services.ReservationServices;

namespace RentalCarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class CarsVController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarService _carService;

        public CarsVController(IMapper mapper,ICarService carService)
        {
            _mapper = mapper;
            _carService = carService;
        }

        /// <summary>
        /// Get a list of all cars
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarReadDTO>>> GetCars()
        {
            return Ok(_mapper.Map<List<CarReadDTO>>(await _carService.GetCars()));
        }

        /// <summary>
        /// Get specific car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CarReadDTO>> GetCar(int id)
        {
            return Ok(_mapper.Map<CarReadDTO>(await _carService.GetCar(id)));
        }

        /// <summary>
        /// Create new car 
        /// </summary>
        /// <param name="dtoCar">Car class that is used for creating new car object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CarReadDTO>> CreateCar(CarCreateDTO dtoCar)
        {

            Car domainCar = _mapper.Map<Car>(dtoCar);
            
            await _carService.CreateCar(domainCar);

            return CreatedAtAction("GetCar",
                new { id = domainCar.Id },
                _mapper.Map<CarReadDTO>(domainCar));
        }

        /// <summary>
        /// Deletes a car from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if(!_carService.CarExists(id))
                return NotFound();

            await _carService.DeleteCar(id);

            return NoContent();
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
            Car domainCar = _mapper.Map<Car>(dtoCar);
            await _carService.UpdateCar(id, domainCar);

            return CreatedAtAction("GetCar",
                new { id = domainCar.Id },
                _mapper.Map<CarReadDTO>(domainCar));
        }
    }
}
