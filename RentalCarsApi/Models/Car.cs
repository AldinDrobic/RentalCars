namespace RentalCarsApi.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Milage { get; set; }
        public bool IsRented { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
