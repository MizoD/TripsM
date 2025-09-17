namespace Models.ModelsVM.Response
{
    public class BookingIndexVM
    {
        public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public string? Search { get; set; }
    }
}
