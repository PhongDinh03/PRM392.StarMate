using Application.ServiceResponse;
using Application.ViewModels.FriendDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IFriendService
    {
        public Task<ServiceResponse<List<FriendResDTO>>> GetAllFriends();

        public Task<ServiceResponse<FriendResDTO>> GetFriendById(int id);

        
        public Task<ServiceResponse<FriendResDTO>> CreateFriend(FriendReqDTO createForm);
        public Task<ServiceResponse<FriendResDTO>> UpdateFriend(FriendReqDTO updateForm, int id);
        public Task<ServiceResponse<bool>> DeleteFriend(int userId, int friendId);
        public Task<ServiceResponse<List<FriendResDTO>>> GetFriendByUserId(int id);

        public Task<ServiceResponse<List<FriendResDTO>>> GetFriendRequestByUserId(int id);
        Task<ServiceResponse<bool>> UpdateFriendshipStatus(int userId, int friendId);
    }
}
