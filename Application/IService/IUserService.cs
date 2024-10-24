﻿using Application.ServiceResponse;
using Application.ViewModels.UserDTO;

namespace Application.IService
{
    public interface IUserService
    {
        Task<ServiceResponse<ViewUserDTO>> GetUserById(int id);
        Task<ServiceResponse<string>> UpdateUser(int id, UpdateUserDTO user);
        Task<ServiceResponse<string>> DeleteUser(int id);
    }
}
