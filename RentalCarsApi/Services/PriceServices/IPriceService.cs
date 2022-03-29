using RentalCarsApi.Models;

namespace RentalCarsApi.Services.PriceServices
{
    public interface IPriceService
    {
        public Task<IEnumerable<Price>> GetPrices();
        public Task<Price> GetPrice(int id);
        public Task<Price> CreatePrice(Price price);
        public Task UpdatePrice(int id, Price price);
        public Task DeletePrice(int id);
        public Task AddPriceToDatabase(Price price);
        public Task SaveDatabase();
        public bool PriceExists(int id);
        public Task CalculatePrice(Rental rental);
        
    }
}
