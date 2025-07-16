using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvisibleSoftware.DeviceGateway.Application.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
       public LoginDto LoginDto { get; set; }
        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto ?? throw new ArgumentNullException(nameof(loginDto));
        }
    }

    public class  LoginResponse
    {
        public string Token { get; set; }
    }

    public class LoginResponseHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        public Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Here you would typically validate the user credentials and generate a token
            // For demonstration purposes, we will return a dummy token
            var token = "dummy_token"; // Replace with actual token generation logic
            return Task.FromResult(new LoginResponse { Token = token });
        }
    }
}
