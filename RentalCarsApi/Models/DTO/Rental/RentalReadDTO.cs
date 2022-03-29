namespace RentalCarsApi.Models.DTO.Rental
{
    public class RentalReadDTO
    {
        public int Id { get; set; }
        public int BookingNumber { get; set; }
        public double RentalPrice { get; set; }
        public int RentalDays { get; set; }
        public string CustomerBirth { get; set; }
        public string TimeDateRental { get; set; }
        public string TimeDateReturn { get; set; }
        public int StartCarMilage { get; set; }
        public int EndCarMilage { get; set; }
        public int CarId { get; set; }
    }
}
