using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.LikeZodiacDTO
{
    public class LikeZodiacDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        
        public string Email { get; set; } = null!;
        public string? Gender { get; set; }
        public string TelephoneNumber { get; set; } = null!;

        public int ZodiacLikeId { get; set; }
        public string NameZodiac { get; set; } = null!;
    }
}
