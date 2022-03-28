namespace RentalCarsApi.Models.DTO.Price
{
    public class PriceEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BaseDayRental { get; set; }
        public double KilometerPrice { get; set; }
    }
}
