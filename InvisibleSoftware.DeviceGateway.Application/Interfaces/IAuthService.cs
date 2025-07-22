using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IAuthService
    {
    
    Task<AuthResult> LoginAsync(LoginDto loginDto,CancellationToken cancellationToken);
    Task<AuthResult> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken);
    Task LogoutAsync(Guid userId);

    Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword,CancellationToken cancellationToken);
    Task<bool> RequestPasswordResetAsync(string email, CancellationToken cancellationToken);
    Task<bool> ResetPasswordAsync(string token, string newPassword,CancellationToken cancellationToken);

    Task<bool> SendEmailVerificationAsync(Guid userId, CancellationToken cancellationToken);
    Task<bool> VerifyEmailAsync(string token, CancellationToken cancellationToken);
    string GenerateJwtToken(User user);
     string GenerateUserCode();
    }
}
