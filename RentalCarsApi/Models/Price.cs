using System.ComponentModel.DataAnnotations;

namespace RentalCarsApi.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public double BaseDayRental { get; set; }
        [Required]
        public double KilometerPrice { get; set; }
    }
}
