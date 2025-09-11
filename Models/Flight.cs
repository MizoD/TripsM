using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
        public enum FlightStatus
        {
            Scheduled,
            Boarding,
            Departed,
            InAir,
            Landed,
            Cancelled,
            Delayed
        }

        public class Flight
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Flight title is required")]
            [StringLength(100, ErrorMessage = "Flight title cannot exceed 100 characters")]
            public string Title { get; set; } = string.Empty;

            public FlightStatus Status { get; set; } = FlightStatus.Scheduled;

            [Required(ErrorMessage = "Departure time is required")]
            public DateTime DepartureTime { get; set; }

            [Required(ErrorMessage = "Arrival time is required")]
            public DateTime ArrivalTime { get; set; }

            public int Traffic { get; set; }

            [Required(ErrorMessage = "Departure airport is required")]
            public int DepartureAirportId { get; set; }

            [JsonIgnore]
            public Airport DepartureAirport { get; set; } = null!;

            [Required(ErrorMessage = "Arrival airport is required")]
            public int ArrivalAirportId { get; set; }

            [JsonIgnore]
            public Airport ArrivalAirport { get; set; } = null!;

            [Required(ErrorMessage = "Aircraft is required")]
            public int AirCraftId { get; set; }

            [JsonIgnore]
            public AirCraft Aircraft { get; set; } = null!;

            public int? TripId { get; set; }

            [JsonIgnore]
            public Trip? Trip { get; set; }

            public ICollection<Seat> Seats { get; set; } = new List<Seat>();
            public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

            // ---------------- Derived Properties ----------------

            [NotMapped]
            public TimeSpan Duration => ArrivalTime - DepartureTime;

            [NotMapped]
            public int TotalSeats => Seats.Count;

            [NotMapped]
            public int AvailableSeats => Seats.Count(s => !s.IsBooked);

            [NotMapped]
            public int BookedSeats => Seats.Count(s => s.IsBooked);

            // ---------------- Pricing ----------------

            [NotMapped]
            public decimal Price
            {
                get
                {
                    if (DepartureAirport == null || ArrivalAirport == null || Aircraft == null)
                        return 0;

                    double distanceKm = CalculateDistance(
                        DepartureAirport.Latitude, DepartureAirport.Longitude,
                        ArrivalAirport.Latitude, ArrivalAirport.Longitude);

                    return (decimal)(distanceKm * (double)Aircraft.InitialPrice);
                }
            }

            public decimal GetPriceForCoach(Coach coach) => coach switch
            {
                Coach.Economy => Price,
                Coach.Business => Price * 2.5m,
                Coach.FirstClass => Price * 4m,
                _ => Price
            };

            // ---------------- Seat Handling ----------------

            public IEnumerable<Seat> GetAvailableSeats(Coach? coach = null) =>
                coach == null
                    ? Seats.Where(s => !s.IsBooked)
                    : Seats.Where(s => !s.IsBooked && s.Coach == coach.Value);

            /// <summary>
            /// Generate seats for this flight based on the aircraft's capacity.
            /// </summary>
            public void GenerateSeats()
            {
                if (Aircraft == null) return;

                Seats.Clear();
                for (int i = 1; i <= Aircraft.Capacity; i++)
                {
                    Seats.Add(new Seat
                    {
                        Number = $"S{i:D3}",
                        Coach = GetCoachType(i, Aircraft.Capacity),
                        IsBooked = false,
                        IsCheckedIn = false,
                        FlightId = Id
                    });
                }
            }

            private static Coach GetCoachType(int index, int capacity)
            {
                if (index <= capacity * 0.7) return Coach.Economy;
                if (index <= capacity * 0.9) return Coach.Business;
                return Coach.FirstClass;
            }

            // ---------------- Helpers ----------------

            /// <summary>
            /// Haversine formula to calculate distance (in km).
            /// </summary>
            private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
            {
                const double R = 6371; // Radius of Earth in km
                double dLat = (lat2 - lat1) * Math.PI / 180;
                double dLon = (lon2 - lon1) * Math.PI / 180;

                lat1 *= Math.PI / 180;
                lat2 *= Math.PI / 180;

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                           Math.Cos(lat1) * Math.Cos(lat2) *
                           Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                return R * c;
            }
        }
}
