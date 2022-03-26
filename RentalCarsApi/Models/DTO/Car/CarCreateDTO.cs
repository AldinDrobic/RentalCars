namespace RentalCarsApi.Models.DTO.Car
{
    public class CarCreateDTO
    {
        public string Name { get; set; }
        public int Milage { get; set; }
        public bool IsRented { get; set; }
        public int CategoryId { get; set; }
    }
}
