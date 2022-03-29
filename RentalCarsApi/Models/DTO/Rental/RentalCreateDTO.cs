namespace RentalCarsApi.Models.DTO.Rental
{
    public class RentalCreateDTO
    {
        public string CustomerBirth { get; set; }
        public string TimeDateRental { get; set; }
        public string TimeDateReturn { get; set; }
        public int CarId { get; set; }
    }
}
