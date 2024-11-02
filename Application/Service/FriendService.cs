
using Application.IRepository;
using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.FriendDTO;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    UserId=friend.UserId,
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
            newFriend.Status = false; // Set the status to false (0)

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
                    Status = false // Set status as needed
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



        public async Task<ServiceResponse<bool>> UpdateFriendshipStatus(int userId, int friendId)
        {
            var result = new ServiceResponse<bool>();
            try
            {
                // Ensure status is set to true (active)
                bool newStatus = true; // Assuming true indicates an active friendship

                // Call the repository method to update the friendship status
                var isUpdated = await _Repo.UpdateFriendshipStatus(userId, friendId, newStatus);

                // Optionally, check the response from the repository
                if (isUpdated)
                {
                    // Optionally update the status for the reverse friendship (friendId to userId)
                    var isUpdatedReverse = await _Repo.UpdateFriendshipStatus(friendId, userId, newStatus);

                    // Check if the reverse update was successful
                    if (!isUpdatedReverse)
                    {
                        // Handle case where the reverse friendship was not found or not updated
                        result.Success = true; // Still consider the operation successful since the main update worked
                        result.Message = "Friendship status updated for one direction, but not for the reverse!";
                        result.Data = true; // Return true as the primary update was successful
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = "Friendship status updated successfully in both directions!";
                        result.Data = true; // Return true if the update was successful for both
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "Friendship not found or status update failed!";
                    result.Data = false; // Return false if the update was not successful
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
