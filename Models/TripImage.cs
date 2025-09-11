using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class TripImage
    {
        public int Id { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        public int TripId { get; set; }

        [JsonIgnore]
        public Trip? Trip { get; set; }

    }
}
