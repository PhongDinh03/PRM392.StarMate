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
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password hash cannot be null or empty", nameof(passwordHash));
            }

            // Ensure _dbContext and Users DbSet are not null
            if (_dbContext == null)
            {
                throw new Exception("DbContext is null");
            }

            if (_dbContext.Users == null)
            {
                throw new Exception("Users DbSet is null");
            }

            // Retrieve the user from the database
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(record => record.Email == email && record.Password == passwordHash);

            // Check if user is found
            if (user is null)
            {
                throw new Exception("Email & password is not correct");
            }

            return user;
        }
    



    public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users
                 .FirstOrDefaultAsync(record => record.Email == email);
            return user is null ? throw new Exception("Email is not correct") : user;
        }
    }
}
