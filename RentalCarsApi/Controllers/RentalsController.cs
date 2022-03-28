using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Rental;
using RentalCarsApi.Services.CarServices;
using RentalCarsApi.Services.RentalServices;

namespace RentalCarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRentalService _rentalService;
        private readonly ICarService _carService;

        public RentalsController(IMapper mapper, IRentalService rentalService, ICarService carService)
        {
            _mapper = mapper;
            _rentalService = rentalService;
            _carService = carService;
        }

        /// <summary>
        /// Get a list of all rentals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalReadDTO>>> GetRentals()
        {
            return Ok(_mapper.Map<List<RentalReadDTO>>(await _rentalService.GetRentals()));
        }

        /// <summary>
        /// Get specific rental by carId
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpGet("car/{carId}")]
        public async Task<ActionResult<RentalReadDTO>> GetRentalByCarId(int carId)
        {
            return Ok((_mapper.Map<RentalReadDTO>(await _rentalService.GetRentalByCarId(carId))));
        }

        /// <summary>
        /// Get specific rental by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalReadDTO>> GetRentalById(int id)
        {
            return Ok((_mapper.Map<RentalReadDTO>(await _rentalService.GetRentalById(id))));
        }

        /// <summary>
        /// Create new rental
        /// </summary>
        /// <param name="dtoRental">rental>Rental class that is used for creating new rental object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<RentalReadDTO>> CreateRental(RentalCreateDTO dtoRental)
        {
            if (dtoRental == null)
                return BadRequest();
            if (!_carService.CarExists(dtoRental.CarId))
                return NotFound();

            Car domainCar = _mapper.Map<Car>(await _carService.GetCar(dtoRental.CarId));
            if (domainCar.IsRented)
                return BadRequest();

            await _carService.SetCarAvaiability(domainCar);
            Rental domainRental = _mapper.Map<Rental>(dtoRental);

            await _rentalService.AddRentalToDatabase(domainRental);
            await _carService.UpdateCar(dtoRental.CarId, domainCar);

            return CreatedAtAction("GetRentalById", new { id = domainRental.Id }, _mapper.Map<RentalReadDTO>(domainRental));
        }

        /// <summary>
        /// Deletes a rental from database by rental id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("rentalId/{id}")]
        public async Task<IActionResult> DeleteRentalById(int id)
        {
            //Set car status to available
            Rental rental = await _rentalService.GetRentalById(id);
            Car car = await _carService.GetCar(rental.CarId);
            await _carService.SetCarAvaiability(car);

            //Delete rental
            await _rentalService.DeleteRentalById(id);
            return NoContent();
        }

        /// <summary>
        /// Deletes a rental from database by car id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("carId/{id}")]
        public async Task<IActionResult> DeleteRentalByCarId(int id)
        {
            //Set car status to available
            Rental rental = await _rentalService.GetRentalByCarId(id);
            Car car = await _carService.GetCar(rental.CarId);
            await _carService.SetCarAvaiability(car);

            //Delete rental
            await _rentalService.DeleteRentalByCarId(id);
            return NoContent();
        }

        /// <summary>
        /// Update a specific rental by id
        /// </summary>
        /// <param name="id">Rental objects identifier</param>
        /// <param name="dtoRental">Rental dto object that arrives from body</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRental(int id, RentalEditDTO dtoRental)
        {
            Rental domainRental = _mapper.Map<Rental>(dtoRental);
            await _rentalService.UpdateRental(id, domainRental);

            return CreatedAtAction("CreateRental",
                new { id = domainRental.Id },
                _mapper.Map<RentalReadDTO>(domainRental));
        }
    }
}
