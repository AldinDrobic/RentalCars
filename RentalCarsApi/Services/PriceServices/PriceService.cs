using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;

namespace RentalCarsApi.Services.PriceServices
{
    public class PriceService : IPriceService
    {
        private readonly RentalCarsDbContext _context;

        public PriceService(RentalCarsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add price to database
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddPriceToDatabase(Price price)
        {
            try
            {
                await _context.Prices.AddAsync(price);
                await SaveDatabase();
            }
            catch (Exception)
            {
                throw new Exception("Could not add data to database!");
            }
        }

        /// <summary>
        /// Create new price
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Price> CreatePrice(Price price)
        {
            if (price == null)
                throw new Exception("You need to specify the price details!");

            await AddPriceToDatabase(price);
            await SaveDatabase();

            return price;
        }

        /// <summary>
        /// Delete a price
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeletePrice(int id)
        {
            var price = await _context.Prices.FindAsync(id);

            if (price == null)
                throw new Exception("Price not found");

            _context.Prices.Remove(price);
            await SaveDatabase();
        }

        /// <summary>
        /// Get a specific price by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Price> GetPrice(int id)
        {
            var price = await _context.Prices.FindAsync(id);

            if (price == null)
                throw new Exception("Price not found");

            return price;
        }

        /// <summary>
        /// Get a list of all prices
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Price>> GetPrices()
        {
            return await _context.Prices
                .ToListAsync();
        }

        /// <summary>
        /// Return bool based on if price exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool PriceExists(int id)
        {
            return _context.Prices.Any(e => e.Id == id);
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
        /// Update a price
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdatePrice(int id, Price price)
        {
            if (id != price.Id)
                throw new Exception("Identifier from endpoint doesn't match body id!");

            _context.Entry(price).State = EntityState.Modified;
            await SaveDatabase();
        }
    }
}
