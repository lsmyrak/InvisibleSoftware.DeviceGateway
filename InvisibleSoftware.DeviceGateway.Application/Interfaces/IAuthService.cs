using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResult> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);

        public Task<AuthResult> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken);

        public Task LogoutAsync(Guid userId);

        public Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken);

        public Task<bool> RequestPasswordResetAsync(string email, CancellationToken cancellationToken);

        public Task<bool> ResetPasswordAsync(string token, string newPassword, CancellationToken cancellationToken);

        public Task<bool> SendEmailVerificationAsync(Guid userId, CancellationToken cancellationToken);

        public Task<bool> VerifyEmailAsync(string token, CancellationToken cancellationToken);

        public string GenerateJwtToken(User user);

        public string GenerateUserCode();
    }
}