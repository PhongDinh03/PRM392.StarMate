
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.AuthenDTO
{
    public class VerifyOTPResetDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string CodeOTP { get; set; }
    }
}
