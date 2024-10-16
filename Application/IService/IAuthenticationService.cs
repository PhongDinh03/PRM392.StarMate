using Application.ServiceResponse;
using Application.ViewModels.AuthenDTO;

namespace Application.IService
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse<RegisterDTO>> RegisterAsync(RegisterDTO userObject);

        public Task<TokenResponse<string>> LoginAsync(LoginDTO userObject);
        public Task<TokenResponse<string>> ForgotPass(string email);
        public Task<string> GenerateRandomPasswordResetTokenByEmailAsync(string email);
        public Task<TokenResponse<string>> VerifyForgotPassCode(VerifyOTPResetDTO dto);
        public Task<ServiceResponse<ResetPassDTO>> ResetPass(ResetPassDTO dto);
    }
}
