using RentalCarsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.Mock
{
    public class PriceMockData
    {
        public static List<Price> GetPrices()
        {
            return new List<Price>
            {
                new Price
                {
                    Id = 1,
                    Name = "Standard",
                    BaseDayRental = 10,
                    KilometerPrice = 1
                }
            };
        }

        public static Price GetPrice()
        {
            return new Price()
            {
                Id = 1,
                Name = "Standard",
                BaseDayRental = 10,
                KilometerPrice = 1
            };
        }
    }
}
