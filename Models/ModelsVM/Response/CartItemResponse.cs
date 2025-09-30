namespace Models.ModelsVM.Response
{
    public class CartItemResponse
    {
        public string Type { get; set; } = "";
        public int ItemId { get; set; }
        public string Title { get; set; } = "";
        public int PassengersOrRooms { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedAt { get; set; }

        // Add these for flight seat details
        public string? SeatNumber { get; set; }
        public string? Coach { get; set; }
        public DateTime? TravelDate { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public DateTime? DepartureTime { get; set; }
    }
}
