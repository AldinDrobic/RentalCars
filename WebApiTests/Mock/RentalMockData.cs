using RentalCarsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.Mock
{
    public class RentalMockData
    {
        public static List<Rental> GetRentalS()
        {
            return new List<Rental>()
            {
                new Rental(){ Id = 1, BookingNumber = 1000, TotalRentalDays = 1, CustomerBirth = "1984-07-04-1111", TimeDateRental = "220401", TimeDateReturn = "220401", StartCarMilage= 230526, CarId = 2},
                new Rental(){ Id = 2, BookingNumber = 1001, TotalRentalDays = 1, CustomerBirth = "1974-07-04-1112", TimeDateRental = "220402", TimeDateReturn = "220402", StartCarMilage= 154794, CarId = 5},
                new Rental(){ Id = 3, BookingNumber = 1002, TotalRentalDays = 1, CustomerBirth = "1964-07-04-1113", TimeDateRental = "220403", TimeDateReturn = "220403", StartCarMilage= 11445, CarId = 8}
            };
        }

        public static Rental GetRental()
        {
            return new Rental()
            {
                Id = 1,
                BookingNumber = 1000,
                TotalRentalDays = 1,
                CustomerBirth = "1984-07-04-1111",
                TimeDateRental = "220401",
                TimeDateReturn = "220401",
                StartCarMilage = 230526,
                CarId = 2
            };
        }

        public static Rental PostRental()
        {
            return new Rental()
            {
                Id = 1,
                BookingNumber = 1000,
                TotalRentalDays = 1,
                CustomerBirth = "1984-07-04-1111",
                TimeDateRental = "220401",
                TimeDateReturn = "220401",
                StartCarMilage = 230526,
                CarId = 2
            };
        }
    }
}
