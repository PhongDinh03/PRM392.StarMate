using Application.IRepository;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repository
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly ZodiacTinderContext _dbContext;
        public UserRepo(ZodiacTinderContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<bool> CheckEmailAddressExisted(string emailaddress) =>
            await _dbContext.Users.AnyAsync(u => u.Email == emailaddress);

        public async Task<User> GetUserByConfirmationToken(string token)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.ConfirmationToken == token);
            return user ?? throw new Exception("User not found");
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users
                  .FirstOrDefaultAsync(record => record.Email == email);
            return user is null ? throw new Exception("Email is not correct") : user;
        }

        public async Task<User> GetUserByEmailAddressAndPasswordHash(string email, string passwordHash)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(record => record.Email == email && record.Password == passwordHash);
            return user is null ? throw new Exception("Email & password is not correct") : user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users
                 .FirstOrDefaultAsync(record => record.Email == email);
            return user is null ? throw new Exception("Email is not correct") : user;
        }
    }
}
