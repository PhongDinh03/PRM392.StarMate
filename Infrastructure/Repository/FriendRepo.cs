
using Application.IRepository;
using Domain.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repository
{
    public class FriendRepo : GenericRepo<Friend>, IFriendRepo
    {
        private readonly ZodiacTinderContext _context;
        public FriendRepo(ZodiacTinderContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Friend>> GetListF()
        {
            return await _context.Friends
                 .ToListAsync();
        }


        public async Task<Friend?> GetFById(int id)
        {
            return await _context.Friends
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Friend>> GetFByUserId(int userId)
        {
            // Validate user ID
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.", nameof(userId));
            }

            // Retrieve friends and include the FriendUser details
            var friends = await _context.Friends
                .Include(f => f.FriendNavigation)
                .ThenInclude(u => u.Zodiac)// Include friend user details
                .Where(f => f.UserId == userId &&  f.Status == true && f.FriendNavigation.Status == 1)
                .ToListAsync();

            // Check if no friends were found
            if (friends == null || !friends.Any())
            {
                Console.WriteLine($"No friends found for user ID: {userId}");
            }

            return friends; // Return the list of friends
        }


        public async Task<List<Friend>> GetFriendRequest(int userId)
        {
            // Validate user ID
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.", nameof(userId));
            }

            // Retrieve friends and include the FriendUser details
            var friends = await _context.Friends
                .Include(f => f.FriendNavigation)
                .ThenInclude(u => u.Zodiac)// Include friend user details
                .Where(f => f.UserId == userId && f.Status == false && f.FriendNavigation.Status == 1)
                .ToListAsync();

            // Check if no friends were found
            if (friends == null || !friends.Any())
            {
                Console.WriteLine($"No friends found for user ID: {userId}");
            }

            return friends; // Return the list of friends
        }

        public async Task<Friend?> GetFriendshipByUserAndFriendId(int userId, int friendId)
        {
            return await _context.Friends
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);
        }

        public async Task<bool> UpdateFriendshipStatus(int userId, int friendId, bool status)
        {
            // Fetch the friendship record from the database
            var friendship = await _context.Friends
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

            if (friendship != null)
            {
                // Update the status
                friendship.Status = status; // Ensure property name matches your model
                _context.Friends.Update(friendship); // Mark the entity as modified
                await _context.SaveChangesAsync(); // Save changes to the database
                return true; // Indicate that the update was successful
            }

            return false; // Indicate that the friendship was not found
        }


    }

}

