using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IAuthService
    {
    
    Task<AuthResult> LoginAsync(LoginDto loginDto);
    Task<AuthResult> RegisterAsync(RegisterDto registerDto);
    Task LogoutAsync(Guid userId);

    Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    Task<bool> RequestPasswordResetAsync(string email);
    Task<bool> ResetPasswordAsync(string token, string newPassword);

    Task<bool> SendEmailVerificationAsync(Guid userId);
    Task<bool> VerifyEmailAsync(string token);
    string GenerateJwtToken(User user);
    }
}
