using System.ComponentModel.DataAnnotations;

namespace Models.ModelsVM.Request
{
    public class PayCartRequest
    {
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
