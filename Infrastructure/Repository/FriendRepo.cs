using Application.Enums;
using Application.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


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
                .Where(f => f.UserId == userId && f.Status == 1 && f.FriendNavigation.Status == 1)
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
                .Where(f => f.UserId == userId && f.Status == 5 && f.FriendNavigation.Status == 1)
                .ToListAsync();

            // Check if no friends were found
            if (friends == null || !friends.Any())
            {
                Console.WriteLine($"No friends found for user ID: {userId}");
            }

            return friends; // Return the list of friends
        }


        public async Task<List<Friend>> GetFriendRequestIncome(int userId)
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
                .Where(f => f.UserId == userId && f.Status == 6 && f.FriendNavigation.Status == 1)
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
            if (userId <= 0 || friendId <= 0)
                throw new ArgumentException("User ID and Friend ID must be positive integers.");

            if (userId == friendId)
                throw new ArgumentException("User cannot be friends with themselves.");

            try
            {
                return await _context.Friends
                    .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId && f.Status != 1);
            }
            catch (Exception ex)
            {
                // Log the exception details (optional, if logging is configured)
                Console.WriteLine($"Error fetching friendship data: {ex.Message}");

                // Rethrow as a custom exception if preferred
                throw new ApplicationException("An error occurred while retrieving friendship data.", ex);
            }
        }


        public async Task<bool> UpdateFriendshipStatus(int userId, int friendId, Status status)
        {
            var friendship = await _context.Friends
        .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

            if (friendship != null)
            {
                friendship.Status = (byte)status; // Convert enum to byte
                _context.Friends.Update(friendship);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

}

