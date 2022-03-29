using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Models;

namespace RentalCarsApi
{
    [Index(nameof(BookingNumber), IsUnique = true)]
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookingNumber { get; set; }
        public double RentalPrice { get; set; }
        public int TotalRentalDays { get; set; }
        [Required]
        [RegularExpression(@"\d{4}-\d{2}-\d{2}-\d{4}",
            ErrorMessage = "Incorrect pattern, try using YYYY-MM-DD-XXXX")]
        public string CustomerBirth { get; set; }
        [Required]
        [RegularExpression(@"\d{2}((0[1-9]|1[012])(0[1-9]|1\d|2[0-8])|(0[13456789]|1[012])(29|30)|(0[13578]|1[02])31)|([02468][048]|[13579][26])0229",
            ErrorMessage = "Wrong format for date, use YYMMDD instead ")]
        public string TimeDateRental { get; set; }
        [RegularExpression(@"\d{2}((0[1-9]|1[012])(0[1-9]|1\d|2[0-8])|(0[13456789]|1[012])(29|30)|(0[13578]|1[02])31)|([02468][048]|[13579][26])0229",
            ErrorMessage = "Wrong format for date, use YYMMDD instead ")]
        public string TimeDateReturn { get; set; }
        public int StartCarMilage { get; set; }
        public int EndCarMilage { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
