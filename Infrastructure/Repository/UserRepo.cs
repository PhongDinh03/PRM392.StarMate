using Application.IRepository;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
        public async Task<List<User>> GetRandomUsersByZodiacAndGenderAsync(int[] zodiacIds, string gender)
        {
            // Validate input parameters
            if (zodiacIds == null || zodiacIds.Length == 0)
            {
                throw new ArgumentException("Zodiac IDs cannot be null or empty", nameof(zodiacIds));
            }

            if (string.IsNullOrWhiteSpace(gender))
            {
                throw new ArgumentException("Gender cannot be null or empty", nameof(gender));
            }

            // Normalize gender to lowercase for case-insensitive comparison
            string normalizedGender = gender.ToLower();

            // Retrieve users based on zodiac IDs and gender
            var users = await _dbContext.Users
                .Where(u => zodiacIds.Contains(u.ZodiacId.GetValueOrDefault()) &&
                            u.gender.ToLower() == normalizedGender)
                .Include(u => u.Zodiac) // Ensure Zodiac is included
                .ToListAsync();

            // Check if Zodiac information is retrieved
            foreach (var user in users)
            {
                if (user.Zodiac == null)
                {
                    Console.WriteLine($"User: {user.FullName}, ZodiacId: {user.ZodiacId} - Zodiac data not found.");
                }
                else
                {
                    Console.WriteLine($"User: {user.FullName}, ZodiacId: {user.ZodiacId}, NameZodiac: {user.Zodiac.NameZodiac}, Description: {user.Zodiac.DesZodiac}");
                }
            }

            // Check if we have at least 1 user available
            if (users.Count == 0)
            {
                throw new Exception("No users available for the specified criteria");
            }

            // Randomly select up to 8 users
            var selectedUsersCount = Math.Min(8, users.Count);
            var selectedUsers = users.OrderBy(_ => Guid.NewGuid()).Take(selectedUsersCount).ToList();

            return selectedUsers;
        }




    }
}


