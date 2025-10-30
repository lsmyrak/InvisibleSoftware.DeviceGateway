using InvisibleSoftware.Devicegateway.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace InvisibleSoftware.DeviceGateway.Application.Auth.Commands
{
    public class UnregisterCommand : IRequest<bool>
    {
    }

    public class UnregisterCommandHandler : IRequestHandler<UnregisterCommand, bool>
    {
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        
        public UnregisterCommandHandler(HttpContextAccessor httpContextAccessor, UserManager<User> userManager ) 
        { 
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> Handle(UnregisterCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);
            return true;
        }
    }
}
