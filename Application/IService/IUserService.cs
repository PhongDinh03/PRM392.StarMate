using Application.ServiceResponse;
using Application.ViewModels.UserDTO;

namespace Application.IService
{
    public interface IUserService
    {
        Task<ServiceResponse<ViewUserDTO>> GetUserById(int id);
        Task<ServiceResponse<ViewUserDTO>> UpdateUser(int id, UpdateUserDTO user);
        Task<ServiceResponse<string>> DeleteUser(int id);

        Task<ServiceResponse<List<ViewFullUserDTO>>> GetRandomUsersByZodiacAndGenderAsync(int[] zodiacIds, string gender, int userId);
    }
}
