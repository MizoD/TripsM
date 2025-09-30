using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsVM.Response
{
    public class HomeVM
    {
        public List<string> AirportsName { get; set; } = new();
        public List<string> HotelsName { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public List<Hotel> Hotels { get; set; } = new();
        public List<Trip> FeaturedTrips { get; set; } = new();
        public List<Flight> UpcomingFlights { get; set; } = new();
    }
}
