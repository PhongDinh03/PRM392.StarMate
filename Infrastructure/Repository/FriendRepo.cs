
using Application.IRepository;

using Infrastructure.Models;
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


        public async Task<Friend?> GetFById(int userId)
        {
            return await _context.Friends
                .FirstOrDefaultAsync(m => m.Id == userId);
        }
        public async Task<List<Friend>> GetFByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            var friends = await _context.Friends
                .Where(b => b.UserId == userId)
                .ToListAsync();

            if (friends == null || !friends.Any())
            {
               
                Console.WriteLine($"No friend found for user ID: {userId}");
            }

            return friends;
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
                friendship.status = status; // Ensure property name matches your model
                _context.Friends.Update(friendship); // Mark the entity as modified
                await _context.SaveChangesAsync(); // Save changes to the database
                return true; // Indicate that the update was successful
            }

            return false; // Indicate that the friendship was not found
        }


    }

}

