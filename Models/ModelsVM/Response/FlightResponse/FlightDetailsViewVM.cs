namespace Models.ModelsVM.Response.FlightResponse
{
    public class FlightDetailsViewVM
    {
        public Flight Flight { get; set; } = null!;
        public List<Flight> RelatedFlights { get; set; } = new();
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }
        public int AvailableSeats { get; set; }
        public List<Seat> Seats { get; set; } = new();
    }
}
