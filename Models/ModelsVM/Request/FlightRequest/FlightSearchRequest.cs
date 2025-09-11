using System.ComponentModel.DataAnnotations;

namespace Models.ModelsVM.Request.FlightRequest
{
    public class FlightSearchRequest
    {
        [Required]
        public string Country { get; set; } = null!;
        public DateTime? TravelDate { get; set; }
        public int NumberOfPassengers { get; set; }

    }
}
