namespace Models.ModelsVM.Response
{
    public class FlightIndexVM
    {
        public IEnumerable<Flight> Flights { get; set; } = new List<Flight>();
        public int UpcomingFlights { get; set; }
        public int PastFlights { get; set; }

        // Pagination
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        // Search + Filter
        public string? Search { get; set; }
        public string Filter { get; set; } = "all";
    }
}
