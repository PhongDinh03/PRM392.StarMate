using Application.IService;
using Application.ViewModels.AuthenDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StarMate.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related actions.
    /// </summary>
    [Route("api/authentication")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenController(IAuthenticationService authen)
        {
            _authenticationService = authen;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerObject">The registration details.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerObject)
        {
            var result = await _authenticationService.RegisterAsync(registerObject);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Initiates the forgot password process.
        /// </summary>
        /// <param name="email">The email of the user who forgot their password.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _authenticationService.ForgotPass(email);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Verifies the OTP for password reset.
        /// </summary>
        /// <param name="request">The OTP verification request details.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOTPResetDTO request)
        {
            var response = await _authenticationService.VerifyForgotPassCode(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="dto">The password reset details.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassWord(ResetPassDTO dto)
        {
            var response = await _authenticationService.ResetPass(dto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginObject">The login details.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO loginObject)
        {
            var result = await _authenticationService.LoginAsync(loginObject);

            if (!result.Success)
            {
                return StatusCode(401, result);
            }

            return Ok(
                new
                {
                    success = result.Success,
                    message = result.Message,
                    token = result.DataToken,
                    role = result.Role,
                    hint = result.HintId,
                }
            );
        }
    }
}
