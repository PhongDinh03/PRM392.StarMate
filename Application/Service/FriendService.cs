
using Application.IRepository;
using Application.IService;
using Application.ServiceResponse;
using Application.ViewModels.FriendDTO;
using AutoMapper;
using Infrastructure.Models;
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
        private readonly IMapper _mapper;

        public FriendService(IMapper mapper, IFriendRepo Repo)
        {
            _mapper = mapper;
            _Repo = Repo;
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
            try
            {
                // Assuming createForm.Id should be zero for new bookings
                if (createForm.Id != 0)
                {
                    var friendExist = await _Repo.GetFById(createForm.Id);
                    if (friendExist != null)
                    {
                        result.Success = false;
                        result.Message = "Friend with the same ID already exists!";
                        return result;
                    }
                }

              

                var newFriend = _mapper.Map<Friend>(createForm); // Map directly
                await _Repo.AddAsync(newFriend); // Assuming the database generates the ID

                // Map to BookingResDTO
                result.Data = _mapper.Map<FriendResDTO>(newFriend);
                result.Success = true;
                result.Message = "Friend add successfully!";
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
                // Retrieve friends for the specified userId
                var friends = await _Repo.GetFByUserId(userId);
                Console.WriteLine($"Friends retrieved for UserId {userId}: {friends?.Count ?? 0}");

                // Map the friend list to FriendResDTO
                var friendList = friends.Select(c => new FriendResDTO
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    FriendId = c.FriendId,
                }).ToList();

                // Set the response data
                result.Data = friendList;
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"An error occurred while retrieving friends: {e.Message}\n{e.StackTrace}";
            }

            return result;
        }

     
    }
}
