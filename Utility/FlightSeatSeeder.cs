using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Utility
{
    public class FlightSeatSeeder
    {
        private readonly ApplicationDbContext _context;

        public FlightSeatSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedSeatsForFlights()
        {
            var flights = _context.Flights
                .Include(f => f.Aircraft)
                .Include(f => f.Seats)
                .ToList();

            foreach (var flight in flights)
            {
                if (flight.Aircraft == null) continue;

                if (flight.Seats.Any())
                    continue;

                var newSeats = new List<Seat>();

                for (int i = 1; i <= flight.Aircraft.Capacity; i++)
                {
                    Coach coach;

                    if (i <= flight.Aircraft.Capacity * 0.7)
                        coach = Coach.Economy;
                    else if (i <= flight.Aircraft.Capacity * 0.9)
                        coach = Coach.Business;
                    else
                        coach = Coach.FirstClass;

                    newSeats.Add(new Seat
                    {
                        Number = $"S{i}",
                        Coach = coach,
                        IsBooked = false,
                        IsCheckedIn = false,
                        FlightId = flight.Id
                    });
                }

                _context.Seats.AddRange(newSeats);
            }

            _context.SaveChanges();
        }
    }
}
