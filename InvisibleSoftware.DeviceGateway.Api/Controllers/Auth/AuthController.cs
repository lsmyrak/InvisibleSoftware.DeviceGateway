using InvisibleSoftware.DeviceGateway.Application.Auth.Commands;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvisibleSoftware.DeviceGateway.Api.Controllers.Auth
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
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize]
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
        [HttpPost("add-role")]
        [Authorize]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
        {
            var roleId = await _mediator.Send(command);
            var role = await _mediator.Send(new GetRoleByIdQuery(roleId));
            return CreatedAtAction(nameof(GetRoleById), new { id = roleId }, role);
        }

        [HttpPost("user-role-management")]
        [Authorize]
        public async Task<IActionResult> AddRoleToUser([FromBody] UserRoleManagementCommand command)
        {
            await _mediator.Send(command);
            return Ok("Role added successfully.");
        }
        [HttpGet("lookup-role")]
        [Authorize]
        public async Task<IActionResult> LoopupRole()
        {
            return Ok(await _mediator.Send(new GetRoleLookupQuery()));
        }
        [HttpGet("role/{id}")]
        [Authorize]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery(id));
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);

        }
    }
}
