using Application.ServiceResponse;
using Application.ViewModels.LikeZodiacDTO;

namespace Application.IService
{
    public interface ILikeZodiacService
    {
        Task<ServiceResponse<List<LikeZodiacDTO>>> GetAllLikeZodiac();
        Task<ServiceResponse<LikeZodiacDTO?>> GetLikeZodiacById(int id);
        Task<ServiceResponse<LikeZodiacDTO>> AddLikeZodiacAsync(CreateLikeZodiacDTO createDto);
        Task<ServiceResponse<string>> UpdateLikeZodiacAsync(int id, CreateLikeZodiacDTO updateDto);
        Task<ServiceResponse<string>> DeleteLikeZodiacAsync(int id);
    }
}
