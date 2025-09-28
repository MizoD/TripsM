using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public enum AirCraftStatus { Ready, Busy, Maintainance}
    public enum AirCraftType { Economy, Business, Private}
    public class AirCraft
    {
        public int Id { get; set; }
        [Required]
        public string Model { get; set; } = null!;
        [Required]
        public int Capacity { get; set; }
        public AirCraftStatus Status { get; set; }
        public string AirlineName { get; set; } = string.Empty;
        public AirCraftType Type { get; set; }

        [Required]
        public decimal InitialPrice { get; set; }
        public bool IsActive { get; set; } = true;

        public int AirportId { get; set; }
        [JsonIgnore]
        [ValidateNever] 
        public Airport Airport { get; set; } = null!;
    }
}
