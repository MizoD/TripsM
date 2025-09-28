using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsVM.Response
{
    public class HotelIndexVM
    {
        public IEnumerable<Hotel> Hotels { get; set; } = new List<Hotel>();

        public int AvailableHotels { get; set; }
        public int FullHotels { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public string? Search { get; set; }
        public string Filter { get; set; } = "all";
    }
}
