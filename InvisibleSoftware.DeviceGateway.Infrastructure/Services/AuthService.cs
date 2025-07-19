using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Authentication:JwtKey"];
            var jwtIssuer = _configuration["Authentication:JwtIssuer"];

            var claims = new List<Claim>
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName)
    };




            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResult> LoginAsync(LoginDto loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return new AuthResult { Success = false, Errors = new[] { "Invalid credentials" } };

            var token = GenerateJwtToken(user);
            return new AuthResult { Success = true, Token = token };
        }

        public Task LogoutAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RequestPasswordResetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailVerificationAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyEmailAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
