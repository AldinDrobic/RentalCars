using RentalCarsApi.Models;

namespace RentalCarsApi
{
    public class Rental
    {
        public int Id { get; set; }
        public int BookingNumber { get; set; }
        public double RentalPrice { get; set; }
        public int RentalDays { get; set; }
        public int CustomerBirth { get; set; }
        public DateTime TimeDateRental { get; set; }
        public DateTime TimeDateReturn { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
