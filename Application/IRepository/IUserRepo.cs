﻿using Infrastructure.Models;

namespace Application.IRepository
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User> GetUserByEmailAddressAndPasswordHash(string email, string passwordHash);
        Task<User> GetUserByEmail(string email);
        Task<bool> CheckEmailAddressExisted(string emailaddress);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByConfirmationToken(string token);
    }
}
