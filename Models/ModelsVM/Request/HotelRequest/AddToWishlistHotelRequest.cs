using System.ComponentModel.DataAnnotations;

namespace Models.ModelsVM.Request.HotelRequest
{
    public class AddToWishlistHotelRequest
    {
        [Required(ErrorMessage = "HotelId is required.")]
        public int HotelId { get; set; }

    }
}
