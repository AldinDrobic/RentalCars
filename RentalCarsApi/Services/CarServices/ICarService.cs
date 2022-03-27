using RentalCarsApi.Models;

namespace RentalCarsApi.Services.CarServices
{
    public interface ICarService
    {
        public Task<IEnumerable<Car>> GetCars();
        public Task<Car> GetCar(int id);
        public Task<Car> CreateCar(Car car);
        public Task UpdateCar(int id, Car car);
        public Task DeleteCar(int id);
        public Task AddCarToDatabase(Car car);
        public Task SaveDatabase();
        public Task SetCarAvaiability(Car car);
        public Task<bool> IsCarRented(int id);
        public bool CarExists(int id);
        
    }
}
