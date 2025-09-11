namespace Models.ModelsVM.Response.TripResponse
{
    public class TripDetailsViewModel
    {
        public TripResponse Trip { get; set; } = null!;
        public ICollection<TripResponse> RelatedTrips { get; set; } = new List<TripResponse>();
        public ICollection<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();
        public ICollection<string> TripImages { get; set; } = new List<string>();
    }
}
