using System.ComponentModel.DataAnnotations;

namespace RentalCarsApi.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int Milage { get; set; }
        [Required]
        public bool IsRented { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
