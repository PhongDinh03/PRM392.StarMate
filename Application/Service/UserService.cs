
using Application.IRepository;
using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.UserDTO;
using AutoMapper;
using Infrastructure.Models;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<string>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                var user =  await _userRepo.GetByIdAsync(id);
                if (user != null)
                {
                    _ = _userRepo.Remove(user);
                    serviceResponse.Success = true;
                    serviceResponse.Message = "User deleted successfully";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Failed to delete user: {ex.Message}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ViewUserDTO>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<ViewUserDTO>();

            try
            {
                var user = await _userRepo.GetByIdAsync(id);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                }
                else
                {
                    var userDTO = _mapper.Map<ViewUserDTO>(user);
                    serviceResponse.Data = userDTO;
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> UpdateUser(int id, UpdateUserDTO user)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                var existingUser = await _userRepo.GetByIdAsync(id);
                if (existingUser == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                    return serviceResponse;
                }
                var userEntity = _mapper.Map<User>(user);
                await _userRepo.Update(userEntity);

                serviceResponse.Success = true;
                serviceResponse.Message = "User updated successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Failed to update user: {ex.Message}";
            }

            return serviceResponse;
        }
    }
}
