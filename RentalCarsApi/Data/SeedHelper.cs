using RentalCarsApi.Models;

namespace RentalCarsApi.Data
{
    public static class SeedHelper
    {
        public static ICollection<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category(){ Id = 1, Name = "Compact"},
                new Category(){ Id = 2, Name = "Premium"},
                new Category(){ Id = 3, Name = "Minivan"}
            };
        }

        public static ICollection<Price> GetPrices()
        {
            return new List<Price>()
            {
                new Price(){ Id = 1, Name = "Standard", BaseDayRental = 10, KilometerPrice = 1}
            };
        }

        public static ICollection<Car> GetCars()
        {
            return new List<Car>()
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

        public static ICollection<Reservation> GetReservations()
        {
            return new List<Reservation>()
            {
                new Reservation(){ Id = 1, ReservationNumber = 1000, CustomerBirth = "1984-07-04-1111", TimeDateRental = "220401", TimeDateReturn = "220401", CarId = 1},
                new Reservation(){ Id = 2, ReservationNumber = 2000, CustomerBirth = "1974-08-04-1112", TimeDateRental = "220402", TimeDateReturn = "220402", CarId = 4},
                new Reservation(){ Id = 3, ReservationNumber = 3000, CustomerBirth = "1964-09-04-1113", TimeDateRental = "220403", TimeDateReturn = "220403", CarId = 6}

            };
        }

        public static ICollection<Rental> GetRentals()
        {
            return new List<Rental>()
            {
                new Rental(){ Id = 1, BookingNumber = 1000, RentalPrice = 1000, RentalDays = 1, CustomerBirth = "1984-07-04-1111", TimeDateRental = "220401", TimeDateReturn = "220401", CarId = 2},
                new Rental(){ Id = 2, BookingNumber = 2000, RentalPrice = 2000, RentalDays = 1, CustomerBirth = "1974-07-04-1112", TimeDateRental = "220402", TimeDateReturn = "220402", CarId = 5},
                new Rental(){ Id = 3, BookingNumber = 3000, RentalPrice = 3000, RentalDays = 1, CustomerBirth = "1964-07-04-1113", TimeDateRental = "220403", TimeDateReturn = "220403", CarId = 8}
            };
        }

    }
}
