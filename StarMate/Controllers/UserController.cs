using Application.IService;
using Application.ViewModels.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace StarMate.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An ActionResult containing the user data or a NotFound result.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ViewUserDTO))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (response.Data == null)
            {
                return NotFound(new { message = response.Message });
            }
            return Ok(response.Data);
        }

        /// <summary>
        /// Updates a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The user data to update.</param>
        /// <returns>An ActionResult containing the result of the update operation.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(UpdateUserDTO))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO user)
        {
            var response = await _userService.UpdateUser(id, user);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An ActionResult containing the result of the delete operation.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ViewUserDTO))]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomUsersByZodiacAndGender([FromQuery] int[] zodiacIds, [FromQuery] string gender, int userId)
        {
            var response = await _userService.GetRandomUsersByZodiacAndGenderAsync(zodiacIds, gender, userId);
            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new { message = response.Message });
            }
            return Ok(response.Data);
        }
    }
}
