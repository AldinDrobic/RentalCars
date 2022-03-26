namespace RentalCarsApi.Models
{
    public class Price
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BaseDayRental { get; set; }
        public double KilometerPrice { get; set; }
    }
}
