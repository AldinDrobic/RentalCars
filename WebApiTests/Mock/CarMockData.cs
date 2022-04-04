using RentalCarsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTests.Mock
{
    public class CarMockData
    {
        public static List<Car> GetCars()
        {
            return new List<Car>
            {
                new Car(){ Id = 1, Name = "Opel Astra", Milage = 156000, IsRented = true, CategoryId = 1 },
                new Car(){ Id = 2, Name = "Volvo V70", Milage = 230526, IsRented = true, CategoryId = 1 },
                new Car(){ Id = 3, Name = "Volvo V90", Milage = 122000, IsRented = false, CategoryId = 1 },
                new Car(){ Id = 4, Name = "Volvo XC90", Milage = 172340, IsRented = true, CategoryId = 2 },
                new Car(){ Id = 5, Name = "Volvo XC60", Milage = 154794, IsRented = true, CategoryId = 1 },
                new Car(){ Id = 6, Name = "Volvo XC40", Milage = 254786, IsRented = true, CategoryId = 3 },
                new Car(){ Id = 7, Name = "Chrysler Pacifica", Milage = 204413, IsRented = false, CategoryId = 3 },
                new Car(){ Id = 8, Name = "Ferrari Enzo", Milage = 11445, IsRented = true, CategoryId = 2 },
                new Car(){ Id = 9, Name = "Lamborghini Aventador", Milage = 10224, IsRented = false, CategoryId = 2 },
                new Car(){ Id = 10, Name = "Opel Meriva", Milage = 12000, IsRented = false, CategoryId = 3 }
            };
        }

        public static Car GetCar()
        {
            return new Car()
            {
                Id = 1,
                Name = "Opel Astra",
                Milage = 156000,
                IsRented = true,
                CategoryId = 1
            };
        }

        public static Car PostCar()
        {
            return new Car()
            {
                Id = 1,
                Name = "Opel Astra",
                Milage = 156000,
                IsRented = true,
                CategoryId = 1
            };
        }
    }
}
