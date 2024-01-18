using Flights.Domain.Entities;
using Flights.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Flights.Server.Data
{
    public class Entities : DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flight> Flights => Set<Flight>();

        public Entities(DbContextOptions<Entities> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);
            modelBuilder.Entity<Flight>().Property(p => p.RemainingNumberOfSeats).IsConcurrencyToken();
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);
            modelBuilder.Entity<Flight>().OwnsMany(f => f.Bookings);

            SeedFlights(modelBuilder);
        }

        public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Entities>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Flights")));
        }

        private void SeedFlights(ModelBuilder modelBuilder)
        {
            var flight1Id = new Guid("40794675-e773-4601-929a-722108e63b81");
            var flight2Id = new Guid("50794675-e773-4601-929a-722108e63b82");

            // Seeding the Flight entity
            modelBuilder.Entity<Flight>().HasData(
                new
                {
                    Id = flight1Id,
                    Airline = "American Airlines",
                    Price = "300",
                    RemainingNumberOfSeats = 150
                },
                new
                {
                    Id = flight2Id,
                    Airline = "Deutsche BA",
                    Price = "400",
                    RemainingNumberOfSeats = 200
                }
            );

            // Seeding Departure and Arrival for each Flight
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure).HasData(
                new
                {
                    FlightId = flight1Id,
                    Place = "New York",
                    Time = new DateTime(2022, 7, 15, 10, 30, 0)
                },
                new
                {
                    FlightId = flight2Id,
                    Place = "Berlin",
                    Time = new DateTime(2022, 7, 16, 12, 0, 0)
                }
            );

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival).HasData(
                new
                {
                    FlightId = flight1Id,
                    Place = "London",
                    Time = new DateTime(2022, 7, 15, 20, 45, 0)
                },
                new
                {
                    FlightId = flight2Id,
                    Place = "Paris",
                    Time = new DateTime(2022, 7, 16, 22, 30, 0)
                }
            );
        }
    }
}