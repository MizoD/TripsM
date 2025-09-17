namespace Models.ModelsVM.Response
{
    public class CountryIndexVM
    {
        public IEnumerable<Country> Countries { get; set; } = new List<Country>();

        // Pagination
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        // Search filter
        public string? Search { get; set; }
    }
}
