using RentalCarsApi.Models;

namespace RentalCarsApi.Services.ReservationServices
{
    public interface IReservationService
    {
        public Task<IEnumerable<Reservation>> GetReservations();
        public Task<Reservation> GetReservationById(int id);
        public Task<Reservation> GetReservationByCarId(int id);
        public Task<Reservation> CreateReservation(Reservation reservation);
        public Task UpdateReservation(int id, Reservation reservation);
        public Task DeleteReservationById(int id);
        public Task DeleteReservationByCarId(int carId);
        public Task AddReservationToDatabase(Reservation reservation);
        public Task SaveDatabase();
        public bool ReservationExists(int id);
    }
}
