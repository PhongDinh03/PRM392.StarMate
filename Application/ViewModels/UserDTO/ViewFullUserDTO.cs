using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.UserDTO
{
     public class ViewFullUserDTO
    {

        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? TelephoneNumber { get; set; }

        public int ZodiacId { get; set; }

        public string Gender { get; set; }
        public string NameZodiac { get; set; }
        public string? Decription { get; set; }

      
    }
}
