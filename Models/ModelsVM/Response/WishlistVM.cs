namespace Models.ModelsVM.Response
{
    public class WishlistVM
    {
        public List<TripWishlist> MyTripsWishlist { get; set; } = new();
        public List<FlightWishlist> MyFlightsWishlist { get; set; } = new();
        public List<HotelWishlist> MyHotelsWishlist { get; set; } = new();
    }
}
