using System.ComponentModel.DataAnnotations;

namespace Models.ModelsVM
{
    public class NewsletterVM
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
