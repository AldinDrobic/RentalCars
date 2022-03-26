using Microsoft.EntityFrameworkCore;
using RentalCarsApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace RentalCarsApi.Data
{
    public class RentalCarsDbContext: DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public RentalCarsDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
    }
}
