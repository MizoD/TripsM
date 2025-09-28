namespace Models.ModelsVM.Response
{
    public class ReviewIndexVM
    {
        public IEnumerable<Review> Reviews { get; set; } = new List<Review>();

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public string? Search { get; set; }
    }
}
