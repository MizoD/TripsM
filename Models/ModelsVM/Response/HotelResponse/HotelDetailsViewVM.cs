namespace Models.ModelsVM.Response.HotelResponse
{
    public class HotelDetailsViewVM
    {
        public Hotel Hotel { get; set; } = new();
        public List<Hotel> RelatedHotels { get; set; } = new();
    }
}
