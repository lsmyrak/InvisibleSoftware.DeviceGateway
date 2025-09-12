using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Auth.Commands
{
    public class RegisterCommand : IRequest<AuthResult>
    {
        public RegisterDto RegisterDto { get; set; }

        public RegisterCommand(RegisterDto registerDto)
        {
            RegisterDto = registerDto;
        }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
    {
        private IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request.RegisterDto, cancellationToken);
        }
    }
}