using RentalCarsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.Mock
{
    public class ReservationMockData
    {
        public static List<Reservation> GetReservations()
        {
            return new List<Reservation>
            {
                new Reservation(){ Id = 1, ReservationNumber = 1000, CustomerBirth = "1984-07-04-1111", TimeDateRental = "220401", TimeDateReturn = "220401", CarId = 1},
                new Reservation(){ Id = 2, ReservationNumber = 1001, CustomerBirth = "1974-08-04-1112", TimeDateRental = "220402", TimeDateReturn = "220402", CarId = 4},
                new Reservation(){ Id = 3, ReservationNumber = 1002, CustomerBirth = "1964-09-04-1113", TimeDateRental = "220403", TimeDateReturn = "220403", CarId = 6}
            };
        }

        public static Reservation GetReservation()
        {
            return new Reservation()
            {
                Id = 1,
                ReservationNumber = 1000,
                CustomerBirth = "1984-07-04-1111",
                TimeDateRental = "220401",
                TimeDateReturn = "220401",
                CarId = 1
            };
        }

        public static Reservation PostReservation()
        {
            return new Reservation()
            {
                Id = 1,
                ReservationNumber = 1000,
                CustomerBirth = "1984-07-04-1111",
                TimeDateRental = "220401",
                TimeDateReturn = "220401",
                CarId = 1
            };
        }
    }
}
