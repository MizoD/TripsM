namespace Models.ModelsVM.Response
{
    public class TripIndexVM
    {
        public IEnumerable<Trip> Trips { get; set; } = new List<Trip>();

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string? Search { get; set; }
    }
}
