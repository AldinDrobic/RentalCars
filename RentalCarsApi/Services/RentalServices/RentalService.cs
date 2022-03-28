using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;

namespace RentalCarsApi.Services.RentalServices
{
    public class RentalService : IRentalService
    {
        private readonly RentalCarsDbContext _context;

        public RentalService(RentalCarsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a rental to the database
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddRentalToDatabase(Rental rental)
        {
            try
            {
                await _context.Rentals.AddAsync(rental);
                await SaveDatabase();
            }
            catch (Exception)
            {
                throw new Exception("Could not add data to database!");
            }
        }

        /// <summary>
        /// Creates a new rental and adds it to the database
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Rental> CreateRental(Rental rental)
        {
            if (rental == null)
                throw new Exception("You need to specify the rental details!");

            await AddRentalToDatabase(rental);
            await SaveDatabase();

            return rental;
        }

        /// <summary>
        /// Delete rental by car id
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteRentalByCarId(int carId)
        {
            var rental = await _context.Rentals
               .Where(r => r.CarId == carId)
               .FirstOrDefaultAsync();
            if (rental == null)
                throw new Exception("Can't find your rental!");

            _context.Rentals.Remove(rental);
            await SaveDatabase();
        }

        /// <summary>
        /// Delete rental by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteRentalById(int id)
        {
            var rental = await GetRentalById(id);
            if (rental == null)
                throw new Exception("Can't find your reservation!");

            _context.Rentals.Remove(rental);
            await SaveDatabase();
        }

        /// <summary>
        /// Get rental by car id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Rental> GetRentalByCarId(int id)
        {
            var rental = await _context.Rentals
                .Where(r => r.CarId == id)
                .Include(r => r.Car)
                .FirstOrDefaultAsync();

            if (rental == null)
                throw new Exception("Rental not found");

            return rental;
        }

        /// <summary>
        /// Get rental by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Rental> GetRentalById(int id)
        {
            var rental = await _context.Rentals
              .Include(r => r.Car)
              .FirstOrDefaultAsync(c => c.Id == id);

            if (rental == null)
                throw new Exception("Rental not found");

            return rental;
        }

        /// <summary>
        /// Get a list of all rentals
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Rental>> GetRentals()
        {
            return await _context.Rentals
                .Include(r => r.Car)
                .ToListAsync();
        }

        /// <summary>
        /// Checks if rental exists in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
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
        /// Updates a rental
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rental"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateRental(int id, Rental rental)
        {
            if (id != rental.Id)
                throw new Exception("Identifier from endpoint doesn't match body id!");
            if (!RentalExists(id))
                throw new Exception("Rental doesn't exist in the database!");

            _context.Entry(rental).State = EntityState.Modified;
            await SaveDatabase();
        }
    }
}
