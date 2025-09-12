using InvisibleSoftware.DeviceGateway.Application.Settings.Commands;
using InvisibleSoftware.DeviceGateway.Application.Settings.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvisibleSoftware.DeviceGateway.Api.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly IMediator _mediator;

        public SettingsController(ILogger<SettingsController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator;
        }

        #region seed data for roles and admin user and my devices ...

        [HttpPost]
        [Route("seed-data")]
        public async Task<IActionResult> SeedData(CancellationToken cancellationToken)
        {
            await _mediator.Send(new SeedDataCommand(), cancellationToken);
            return Ok("Seeding done.");
        }

        #endregion seed data for roles and admin user and my devices ...

        #region roles and user management

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

        #endregion roles and user management

        #region lookup

        [HttpGet("lookup-role")]
        [Authorize]
        public async Task<IActionResult> LoopupRole()
        {
            return Ok(await _mediator.Send(new GetRoleLookupQuery()));
        }

        [HttpGet("lookup-user")]
        [Authorize]
        public async Task<IActionResult> LoopupUser()
        {
            return Ok(await _mediator.Send(new GeUserLookupQuery()));
        }

        #endregion lookup
    }
}