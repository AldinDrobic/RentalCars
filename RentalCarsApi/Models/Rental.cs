using System.ComponentModel.DataAnnotations;
using RentalCarsApi.Models;

namespace RentalCarsApi
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookingNumber { get; set; }
        public double RentalPrice { get; set; }
        public int RentalDays { get; set; }
        [Required]
        public int CustomerBirth { get; set; }
        [Required]
        public DateTime TimeDateRental { get; set; }
        public DateTime TimeDateReturn { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
