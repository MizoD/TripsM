namespace Models.ModelsVM.Response
{
    public class DashboardVM
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
        public string? Address { get; set; }

        // Stats
        public int BookingsCount { get; set; }
        public int AllwishlistCount { get; set; }
        public int FlightsBookedCount { get; set; }
        public int UserReviewsCount { get; set; }

        // Data
        public IEnumerable<Booking>? Bookings { get; set; }
    }
}
