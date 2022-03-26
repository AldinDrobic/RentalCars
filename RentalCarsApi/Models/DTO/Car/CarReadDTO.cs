namespace RentalCarsApi.Models.DTO.Car
{
    public class CarReadDTO
    {
        public string Name { get; set; }
        public int Milage { get; set; }
        public bool IsRented { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
