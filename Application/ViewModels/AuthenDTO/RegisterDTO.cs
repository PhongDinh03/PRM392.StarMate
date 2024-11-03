
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.AuthenDTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "{0} to {2} from {1} character.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string TelephoneNumber { get; set; }

        public string gender { get; set; }

        public int? ZodiacId { get; set; }
    }
}
