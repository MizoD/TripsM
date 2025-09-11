using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class HotelImage
    {
        public int Id { get; set; }
        [Required]
        public string Imagelink { get; set; } = null!;
        public int HotelId { get; set; }
        [JsonIgnore]
        public Hotel? Hotel { get; set; }
    }
}
