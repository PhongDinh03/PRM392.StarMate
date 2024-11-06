using Application.Enums;
using Application.IRepository;
using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.FriendDTO;
using AutoMapper;
using Domain.Models;

namespace Application.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepo _Repo;
        private readonly IUserRepo _UserRepo;
        private readonly IMapper _mapper;

        public FriendService(IMapper mapper, IFriendRepo Repo, IUserRepo userRepo)
        {
            _UserRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _Repo = Repo ?? throw new ArgumentNullException(nameof(Repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ServiceResponse<List<FriendResDTO>>> GetAllFriends()
        {
            var result = new ServiceResponse<List<FriendResDTO>>();
            try
            {
                // Retrieve the list of friends
                var friends = await _Repo.GetListF();

                // Convert to DTO
                var friendList = friends.Select(c => new FriendResDTO
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    FriendId = c.FriendId,

                    ZodiacName = c.FriendNavigation.Zodiac.NameZodiac
                }).ToList();

                // Set the response data
                result.Data = friendList;
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? e.InnerException.Message + "\n" + e.StackTrace
                    : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }


        public async Task<ServiceResponse<FriendResDTO>> GetFriendById(int id)
        {
            var result = new ServiceResponse<FriendResDTO>();
            try
            {
                var friend = await _Repo.GetFById(id);
                if (friend == null)
                {
                    result.Success = false;
                    result.Message = "Friend not found";
                }
                else
                {
                    var resFriend = new FriendResDTO
                    {
                        Id = friend.Id,
                        UserId = friend.UserId,
                        FriendId = friend.FriendId,
                        ZodiacName = friend.FriendNavigation.Zodiac.NameZodiac
                    };

                    result.Data = resFriend;
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? e.InnerException.Message + "\n" + e.StackTrace
                    : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }


        public async Task<ServiceResponse<FriendResDTO>> CreateFriend(FriendReqDTO createForm)
        {
            var result = new ServiceResponse<FriendResDTO>();

            // Validate the input
            if (createForm == null || createForm.UserId <= 0 || createForm.FriendId <= 0 || createForm.UserId == createForm.FriendId)
            {
                result.Success = false;
                result.Message = "Invalid friend request data! User cannot be friends with themselves.";
                return result;
            }

            // Check if both UserId and FriendId exist in the User table
            var userExists = await _UserRepo.GetByIdAsync(createForm.UserId);
            var friendExists = await _UserRepo.GetByIdAsync(createForm.FriendId);

            if (userExists == null)
            {
                result.Success = false;
                result.Message = $"User with ID {createForm.UserId} not found in the database.";
                return result;
            }

            if (friendExists == null)
            {
                result.Success = false;
                result.Message = $"User with ID {createForm.FriendId} not found in the database.";
                return result;
            }

            // Check if a friendship already exists
            var existingFriendship = await _Repo.GetFriendshipByUserAndFriendId(createForm.UserId, createForm.FriendId);
            if (existingFriendship != null)
            {
                result.Success = false;
                result.Message = "Friendship already exists!";
                return result;
            }

            // Create the new friendship
            var newFriend = _mapper.Map<Friend>(createForm);
            newFriend.Status = 0; // Set the status to false (0)

            // Add the new friend relationship
            await _Repo.AddAsync(newFriend);

            // Optionally create a reciprocal friend relationship if it doesn’t exist
            var reciprocalFriendship = await _Repo.GetFriendshipByUserAndFriendId(createForm.FriendId, createForm.UserId);
            if (reciprocalFriendship == null)
            {
                var reciprocalFriend = new Friend
                {
                    UserId = createForm.FriendId,
                    FriendId = createForm.UserId,
                    Status = 0 // Set status as needed
                };

                await _Repo.AddAsync(reciprocalFriend);
            }

            // Map the newly created friend to FriendResDTO for the response
            result.Data = _mapper.Map<FriendResDTO>(newFriend);
            result.Success = true;
            result.Message = "Friend added successfully!";

            return result;
        }





        public async Task<ServiceResponse<FriendResDTO>> UpdateFriend(FriendReqDTO updateForm, int id)
        {
            var result = new ServiceResponse<FriendResDTO>();
            try
            {
                ArgumentNullException.ThrowIfNull(updateForm);

                var friendUpdate = await _Repo.GetFById(id) ??
                                     throw new ArgumentException("Given friend Id doesn't exist!");
                friendUpdate.UserId = updateForm.UserId;
                friendUpdate.FriendId = updateForm.FriendId;


                await _Repo.Update(friendUpdate);

                result.Success = true;
                result.Message = "Update friend successfully";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? e.InnerException.Message + "\n" + e.StackTrace
                    : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteFriend(int userId, int friendId)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                // Get the friendship entity based on userId and friendId
                var friendship = await _Repo.GetFriendshipByUserAndFriendId(userId, friendId);

                if (friendship == null)
                {
                    result.Success = false;
                    result.Message = "Friendship not found";
                }
                else
                {
                    // Remove the friendship entity from the repository
                    await _Repo.Remove(friendship);
                    result.Success = true;
                    result.Message = "Deleted successfully";
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error occurred: {e.Message}";
            }

            return result;
        }



        public async Task<ServiceResponse<List<FriendResDTO>>> GetFriendByUserId(int userId)
        {
            var result = new ServiceResponse<List<FriendResDTO>>();

            try
            {
                // Validate user ID
                if (userId <= 0)
                {
                    throw new ArgumentException("Invalid user ID.", nameof(userId));
                }

                // Retrieve friends for the specified userId
                var friends = await _Repo.GetFByUserId(userId);
                Console.WriteLine($"Friends retrieved for UserId {userId}: {friends?.Count ?? 0}");

                // Check if friends were found
                if (friends == null || !friends.Any())
                {
                    result.Success = false;
                    result.Message = $"No friends found for user ID: {userId}";
                    return result;
                }

                // Map the friend list to FriendResDTO
                var friendList = friends.Select(c => new FriendResDTO
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    FriendId = c.FriendId,
                    FriendGender = c.FriendNavigation?.Gender ?? "Unknown",
                    FriendName = c.FriendNavigation?.FullName ?? "Unknown", // Check for null and provide a default
                    ZodiacName = c.FriendNavigation?.Zodiac?.NameZodiac ?? "Unknown" // Check for null and provide a default
                }).ToList();

                // Set the response data
                result.Data = friendList;
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"An error occurred while retrieving friends for user ID {userId}: {e.Message}\n{e.StackTrace}";
            }

            return result;
        }

        public async Task<ServiceResponse<List<FriendResDTO>>> GetFriendRequestByUserId(int userId)
        {
            var result = new ServiceResponse<List<FriendResDTO>>();

            try
            {
                // Validate user ID
                if (userId <= 0)
                {
                    throw new ArgumentException("Invalid user ID.", nameof(userId));
                }

                // Retrieve friends for the specified userId
                var friends = await _Repo.GetFriendRequest(userId);
                Console.WriteLine($"Friends retrieved for UserId {userId}: {friends?.Count ?? 0}");

                // Check if friends were found
                if (friends == null || !friends.Any())
                {
                    result.Success = false;
                    result.Message = $"No friends found for user ID: {userId}";
                    return result;
                }

                // Map the friend list to FriendResDTO
                var friendList = friends.Select(c => new FriendResDTO
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    FriendId = c.FriendId,
                    FriendGender = c.FriendNavigation?.Gender ?? "Unknown",
                    FriendName = c.FriendNavigation?.FullName ?? "Unknown", // Check for null and provide a default
                    ZodiacName = c.FriendNavigation?.Zodiac?.NameZodiac ?? "Unknown", // Check for null and provide a default
                }).ToList();

                // Set the response data
                result.Data = friendList;
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"An error occurred while retrieving friends for user ID {userId}: {e.Message}\n{e.StackTrace}";
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> AcceptFriendRequest(int userId, int friendId)
        {
            var result = new ServiceResponse<bool>();
            try
            {
                // Use the enum value for "accepted" status
                bool isAcceptedOriginal = await _Repo.UpdateFriendshipStatus(userId, friendId, Status.Accepted);
                bool isAcceptedReversed = await _Repo.UpdateFriendshipStatus(friendId, userId, Status.Accepted);

                if (isAcceptedOriginal && isAcceptedReversed)
                {
                    result.Success = true;
                    result.Message = "Friend request accepted successfully in both directions.";
                    result.Data = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Friendship not found or accept update failed in one or both directions!";
                    result.Data = false;
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? $"{e.InnerException.Message}\n{e.StackTrace}"
                    : $"{e.Message}\n{e.StackTrace}";
            }

            return result;
        }


        public async Task<ServiceResponse<bool>> DeclineFriendRequest(int userId, int friendId)
        {
            var result = new ServiceResponse<bool>();
            try
            {
                // Use the enum value for "declined" status
                bool isDeclinedOriginal = await _Repo.UpdateFriendshipStatus(userId, friendId, Status.Declined);
                bool isDeclinedReversed = await _Repo.UpdateFriendshipStatus(friendId, userId, Status.Declined);

                if (isDeclinedOriginal && isDeclinedReversed)
                {
                    result.Success = true;
                    result.Message = "Friend request declined successfully in both directions.";
                    result.Data = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Friendship not found or decline update failed in one or both directions!";
                    result.Data = false;
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException != null
                    ? $"{e.InnerException.Message}\n{e.StackTrace}"
                    : $"{e.Message}\n{e.StackTrace}";
            }

            return result;
        }

    }
}