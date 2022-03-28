namespace RentalCarsApi.Services.RentalServices
{
    public interface IRentalService
    {
        public Task<IEnumerable<Rental>> GetRentals();
        public Task<Rental> GetRentalById(int id);
        public Task<Rental> GetRentalByCarId(int id);
        public Task<Rental> CreateRental(Rental rental);
        public Task UpdateRental(int id, Rental rental);
        public Task DeleteRentalById(int id);
        public Task DeleteRentalByCarId(int carId);
        public Task AddRentalToDatabase(Rental rental);
        public Task SaveDatabase();
        public bool RentalExists(int id);
    }
}
