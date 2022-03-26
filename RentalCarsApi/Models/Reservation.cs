namespace RentalCarsApi.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ReservationNumber { get; set; }
        public int CustomerBirth { get; set; }
        public DateTime TimeDateRental { get; set; }
        public DateTime TimeDateReturn{ get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
