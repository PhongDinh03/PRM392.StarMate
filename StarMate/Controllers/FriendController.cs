using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.ServiceResponse;
using Application.ViewModels.FriendDTO;
using Application.IService;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StarMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FriendResDTO>>>> GetAllFriends()
        {
            var response = await _friendService.GetAllFriends();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<FriendResDTO>>> GetFriendById(int id)
        {
            var response = await _friendService.GetFriendById(id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FriendResDTO>>> CreateFriend([FromBody] FriendReqDTO createForm)
        {
            var response = await _friendService.CreateFriend(createForm);
            return CreatedAtAction(nameof(GetFriendById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<FriendResDTO>>> UpdateFriend(int id, [FromBody] FriendReqDTO updateForm)
        {
            var response = await _friendService.UpdateFriend(updateForm, id);
            if (response.Data == null)
                return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("{userId}/{friendId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteFriend(int userId, int friendId)
        {
            var response = await _friendService.DeleteFriend(userId, friendId);

            if (!response.Data)
                return NotFound(response);  

            return Ok(response);  
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ServiceResponse<List<FriendResDTO>>>> GetFriendByUserId(int userId)
        {
            var response = await _friendService.GetFriendByUserId(userId);
            if (response.Data == null || response.Data.Count == 0)
                return NotFound(response);
            return Ok(response);
        }
    }
}
