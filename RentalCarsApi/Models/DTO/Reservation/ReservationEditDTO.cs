namespace RentalCarsApi.Models.DTO.Reservation
{
    public class ReservationEditDTO
    {
        public int Id { get; set; }
        public int ReservationNumber { get; set; }
        public string CustomerBirth { get; set; }
        public string TimeDateRental { get; set; }
        public string TimeDateReturn { get; set; }
        public int CarId { get; set; }
    }
}
