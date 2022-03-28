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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(SeedHelper.GetCategories());
            modelBuilder.Entity<Price>().HasData(SeedHelper.GetPrices());
            modelBuilder.Entity<Car>().HasData(SeedHelper.GetCars());
            modelBuilder.Entity<Reservation>().HasData(SeedHelper.GetReservations());
            modelBuilder.Entity<Rental>().HasData(SeedHelper.GetRentals());
        }
    }
}
