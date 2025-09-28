namespace Models.ModelsVM.Response
{
    public class AircraftIndexVM
    {
        public List<AirCraft> Aircrafts { get; set; } = new();

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public string? Search { get; set; }
    }
}
