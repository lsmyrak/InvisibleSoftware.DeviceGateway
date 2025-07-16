using InvisibleSoftware.DeviceGateway.Application.Auth.Commands;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvisibleSoftware.DeviceGateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await  _mediator.Send(new LoginCommand(loginDto)); 
            return Ok(token);
        }

        [HttpPost("register")]
        public IActionResult Register()
        {
            return Ok();
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
        [HttpGet("unregister")]
        public IActionResult Unregister()
        {
            return Ok();
        }
    }
}
