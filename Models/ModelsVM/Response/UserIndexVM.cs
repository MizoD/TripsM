namespace Models.ModelsVM.Response
{
    public class UserIndexVM
    {
        public IEnumerable<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public int TotalUsers { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public string? Search { get; set; }
    }
}
