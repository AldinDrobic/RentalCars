using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Services.CarServices;

namespace RentalCarsApi.Services
{
    public class CarService : ICarService
    {
        private readonly RentalCarsDbContext _context;

        public CarService(RentalCarsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return bool based on if car exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        /// <summary>
        /// Create new car 
        /// </summary>
        /// <param name="car">Car class that is used for creating new car object</param>
        /// <returns></returns>
        public async Task<Car> CreateCar(Car car)
        {
            if (car == null)
                throw new Exception("You need to specify the car details!");
            
            await AddCarToDatabase(car);
            await SaveDatabase();

            return car;
        }

        /// <summary>
        /// Deletes a car from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
                throw new Exception("Car not found");

            _context.Cars.Remove(car);
            await SaveDatabase();
        }

        /// <summary>
        /// Get specific car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Car> GetCar(int id)
        {
            var car = await _context.Cars
               .Include(c => c.Category)
               .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
                throw new Exception("Car not found");

            return car;
        }

        /// <summary>
        /// Get a list of all cars
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Car>> GetCars()
        {
            return await _context.Cars
                .Include(c => c.Category)
                .ToListAsync();
        }

        /// <summary>
        /// Updates car avaiability
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public async Task SetCarAvaiability(Car car)
        {
            if(car.IsRented)
                car.IsRented = false;
            else
                car.IsRented = true;

            _context.Entry(car).State = EntityState.Modified;
            await SaveDatabase();
            
        }

        /// <summary>
        /// Checks whether a car is rented or not 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsCarRented(int id)
        {
            Car car = await GetCar(id);
            return car.IsRented;
        }

        /// <summary>
        /// Adds car to database
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddCarToDatabase(Car car)
        {
            try
            {
                await _context.Cars.AddAsync(car);
                await SaveDatabase();
            }
            catch (Exception)
            {
                throw new Exception("Could not add data to database!");
            }
        }

        /// <summary>
        /// Deletes car from database
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public async Task RemoveCarFromDatabase(Car car)
        {
            _context.Cars.Remove(car);
            await SaveDatabase();
        }

        /// <summary>
        /// Saves to database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SaveDatabase()
        {
            try
            {               
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Could not save data to database!");
            }
        }

        /// <summary>
        /// Updates values for a specific car
        /// </summary>
        /// <param name="id"></param>
        /// <param name="car"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateCar(int id, Car car)
        {
            if (id != car.Id)
                throw new Exception("Identifier from endpoint doesn't match body id!");
            if (!CarExists(id))
                throw new Exception("Car doesn't exist in the database!");

            _context.Entry(car).State = EntityState.Modified;
            await SaveDatabase();
        }
    }
}
