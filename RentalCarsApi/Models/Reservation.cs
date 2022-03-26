using System.ComponentModel.DataAnnotations;

namespace RentalCarsApi.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReservationNumber { get; set; }
        [Required]
        public int CustomerBirth { get; set; }
        [Required]
        public DateTime TimeDateRental { get; set; }
        public DateTime TimeDateReturn{ get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
