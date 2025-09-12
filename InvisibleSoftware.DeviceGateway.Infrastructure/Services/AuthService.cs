using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationContext _context;

        public AuthService(IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager, ApplicationContext context)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken)
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
                new Claim(ClaimTypes.Name, user.UserName),
            };
            foreach (var role in user.Role)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Code));
            }

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

        public async Task<AuthResult> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var emailUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (emailUser == null || !await _userManager.CheckPasswordAsync(emailUser, loginDto.Password))
                return new AuthResult { Success = false, Errors = new[] { "Invalid credentials" } };

            var user = await _context.Set<User>()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == emailUser.Id, cancellationToken);

            var token = GenerateJwtToken(emailUser);
            return new AuthResult { Success = true, Token = token };
        }

        public Task LogoutAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResult> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = new[] { "User with this email already exists" }
                };
            }
            user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Code = GenerateUserCode(),
                Name = registerDto.UserName,
                Description = $"Created User :  {registerDto.UserName}",
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToArray()
                };
            }
            var token = GenerateJwtToken(user);
            return new AuthResult
            {
                Success = true,
                Token = token
            };
        }

        public Task<bool> RequestPasswordResetAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(string token, string newPassword, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailVerificationAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyEmailAsync(string token, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public string GenerateUserCode()
        {
            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            int countToday = _context.Users
                .Where(h => h.CreatedAt >= today && h.CreatedAt < tomorrow)
                .Count();

            int nextNumber = countToday + 1;
            string numberPart = nextNumber.ToString("D4");

            return $"User/{datePart}/{numberPart}";
        }
    }
}