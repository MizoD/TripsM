namespace Models.ModelsVM.Response
{
    public class AirportIndexVM
    {
        public IEnumerable<Airport> Airports { get; set; } = new List<Airport>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public int ActiveAirports { get; set; }
        public int InactiveAirports { get; set; }
        public string? Search { get; set; }
    }
}
