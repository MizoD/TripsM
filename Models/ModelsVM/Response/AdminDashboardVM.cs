namespace Models.ModelsVM.Response
{
    public class AdminDashboardVM
    {
        public int BookingsCount { get; set; }
        public int TripsCount { get; set; }
        public int FlightsCount { get; set; }
        public int HotelsCount { get; set; }
        public decimal TotalBookingsMade { get; set; }

        public int AircraftCount { get; set; }
        public int AirportsCount { get; set; }
        public int CountriesCount { get; set; }

        public int TotalReviews { get; set; }
        public int GoodReviewsCount { get; set; }
        public int BadReviewsCount { get; set; }

        public int TotalUsers { get; set; }
    }
}
