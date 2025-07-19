using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
       public LoginDto LoginDto { get; set; }
        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto;
        }
    }

    public class  LoginResponse
    {
        public string Token { get; set; }
    }

    public class LoginResponseHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IAuthService _authService;
        public LoginResponseHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request.LoginDto);
            if (result.Success)
            return new LoginResponse
            {
                Token = result.Token
            };
            else
            {
                throw new UnauthorizedAccessException(string.Join(", ", result.Errors));
            }
        }
    }
}
