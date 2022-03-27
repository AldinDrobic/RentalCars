using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Data;
using RentalCarsApi.Models;
using RentalCarsApi.Models.DTO.Car;

namespace RentalCarsApi.Services.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly RentalCarsDbContext _context;

        public ReservationService(RentalCarsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a reservation to the database
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddReservationToDatabase(Reservation reservation)
        {
            try
            {
                await _context.Reservations.AddAsync(reservation);
                await SaveDatabase();
            }
            catch (Exception)
            {
                throw new Exception("Could not add data to database!");
            }
        }

        /// <summary>
        /// Creates a new reservation and adds it to the database
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new Exception("You need to specify the reservation details!");

            await AddReservationToDatabase(reservation);
            await SaveDatabase();

            return reservation;
        }

        /// <summary>
        /// Delete reservation by car id
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteReservationByCarId(int carId)
        {
            var reservation = await _context.Reservations
               .Where(r => r.CarId == carId)
               .FirstOrDefaultAsync();
            if (reservation == null)
                throw new Exception("Can't find your reservation!");

            _context.Reservations.Remove(reservation);
            await SaveDatabase();
        }

        /// <summary>
        /// Delete reservation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteReservationById(int id)
        {
            var reservation = await GetReservationById(id);
            if (reservation == null)
                throw new Exception("Can't find your reservation!");

            _context.Reservations.Remove(reservation);
            await SaveDatabase();
        }

        /// <summary>
        /// Get reservation by car id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Reservation> GetReservationByCarId(int id)
        {
            var reservation = await _context.Reservations
                .Where(r => r.CarId == id)
                .Include(r => r.Car)
                .FirstOrDefaultAsync();

            if (reservation == null)
                throw new Exception("Reservation not found");

            return reservation;
        }

        /// <summary>
        /// Get reservation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Reservation> GetReservationById(int id)
        {
            var reservation = await _context.Reservations
              .Include(r => r.Car)
              .FirstOrDefaultAsync(c => c.Id == id);

            if (reservation == null)
                throw new Exception("Reservation not found");

            return reservation;
        }

        /// <summary>
        /// Get a list of all reservations
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _context.Reservations
                .Include(r => r.Car)
                .ToListAsync();
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
        /// Checks if reservation exists in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
        
        /// <summary>
        /// Updates a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
                throw new Exception("Identifier from endpoint doesn't match body id!");
            if(!ReservationExists(id))
                throw new Exception("Car doesn't exist in the database!");

            _context.Reservations.Add(reservation);
            await SaveDatabase();
        }
    }
}
