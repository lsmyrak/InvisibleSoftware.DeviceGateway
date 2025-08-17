using InvisibleSoftware.DeviceGateway.Application.Settings.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvisibleSoftware.DeviceGateway.Api.Controllers.Setting
{
    [Route("[controller]")]
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
        [HttpPost]
        [Route("seed-data")]
        public async Task<IActionResult> SeedData(CancellationToken cancellationToken)
        {
            await _mediator.Send(new SeedDataCommand(), cancellationToken);
            return Ok("Seeding done.");
        }

        [HttpPost("add-device-type")]
        [Authorize(Roles = "Admin,DeviceManager")]
        public async Task<IActionResult> AddDeviceType([FromBody] AddDeviceTypeCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }
            await _mediator.Send(command);
            return Ok("Device type added successfully.");
        }
        [HttpPost("add-device-group")]
        [Authorize(Roles = "Admin,DeviceManager")]
        public async Task<IActionResult> AddDeviceGroup([FromBody] AddDeviceGroupCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }
            await _mediator.Send(command);
            return Ok("Device group added successfully.");
        }       
    }
}
