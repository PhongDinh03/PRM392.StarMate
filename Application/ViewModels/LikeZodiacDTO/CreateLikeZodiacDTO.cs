using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.LikeZodiacDTO
{
    public class CreateLikeZodiacDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ZodiacLikeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ZodiacLikeId must be a positive integer")]
        public int ZodiacLikeId { get; set; }
    }
}
