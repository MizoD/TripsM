using System.ComponentModel.DataAnnotations;

namespace Models.ModelsVM.Request
{
    public class ForgetPasswordRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
