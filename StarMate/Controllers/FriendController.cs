﻿using Microsoft.AspNetCore.Mvc;
using Application.ServiceResponse;
using Application.ViewModels.FriendDTO;
using Application.IService;

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

        /// <summary>
        /// Send friend request.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FriendResDTO>>> CreateFriend([FromBody] FriendReqDTO createForm)
        {
            var result = await _friendService.CreateFriend(createForm);

            // Check if the creation was successful
            if (!result.Success)
            {
                // Return a 400 Bad Request response with the error message if unsuccessful
                return BadRequest(result);
            }

            // Return a 200 OK response with the result if successful
            return Ok(result);
        }



        /// <summary>
        /// Update  friend .
        /// </summary>
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

        /// <summary>
        /// Get friend by user id.
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ServiceResponse<List<FriendResDTO>>>> GetFriendByUserId(int userId)
        {
            var response = await _friendService.GetFriendByUserId(userId);
            if (response.Data == null || response.Data.Count == 0)
                return NotFound(response);
            return Ok(response);
        }
        /// <summary>
        /// Get friend request by user id.
        /// </summary>
        [HttpGet("FriendRequest/{userId}")]
        public async Task<ActionResult<ServiceResponse<List<FriendResDTO>>>> GetFriendRequestByUserId(int userId)
        {
            var response = await _friendService.GetFriendRequestByUserId(userId);
            if (response.Data == null || response.Data.Count == 0)
                return NotFound(response);
            return Ok(response);
        }


        /// <summary>
        /// Get friend request by user id as friend id.
        /// </summary>
        [HttpGet("FriendRequestIncome/{userId}")]
        public async Task<ActionResult<ServiceResponse<List<FriendResDTO>>>> GetFriendRequestIncomeByUserId(int userId)
        {
            var response = await _friendService.GetFriendRequestIcomeByUserId(userId);
            if (response.Data == null || response.Data.Count == 0)
                return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Accept a friend request.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="friendId">The ID of the friend.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("accept")]
        public async Task<IActionResult> AcceptFriendRequest(int userId, int friendId)
        {
            var response = await _friendService.AcceptFriendRequest(userId, friendId);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Decline a friend request.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="friendId">The ID of the friend.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("decline")]
        public async Task<IActionResult> DeclineFriendRequest(int userId, int friendId)
        {
            var response = await _friendService.DeclineFriendRequest(userId, friendId);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
