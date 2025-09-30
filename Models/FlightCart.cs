using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [PrimaryKey(nameof(UserId), nameof(FlightId), nameof(SeatNumber))] 
    public class FlightCart : Cart
    {
        public int FlightId { get; set; }
        public Flight Flight { get; set; } = null!;

        public DateTime TravelDate { get; set; }
        public Coach Coach { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
    }
}
