namespace RentalCarsApi.Models.DTO.Price
{
    public class PriceCreateDTO
    {
        public string Name { get; set; }
        public double BaseDayRental { get; set; }
        public double KilometerPrice { get; set; }
    }
}
