using System.ComponentModel.DataAnnotations;

namespace RentalCarsApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public double? Price { get; set; }
    }
}

