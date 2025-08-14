using InvisibleSoftware.DeviceGateway.Application.Device.Commands;
using InvisibleSoftware.DeviceGateway.Application.Device.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvisibleSoftware.DeviceGateway.Api.Controllers.Device
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly IMediator _mediator;
        public DeviceController(ILogger<DeviceController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("device-with-rooms")]
        [Authorize]
        public async Task<IActionResult> GetDeviceWithRoom()
        {
           var deviceWithRoom  = await _mediator.Send(new GetAccessibleDevicesWithRoomsQuery());
            return Ok(deviceWithRoom);
        }

        [HttpPost("execute-command/{payloadId}")]
        [Authorize]
        public async Task<IActionResult> ExecuteCommand(Guid payloadId)
        {
            await _mediator.Send(new RunMqttCommand(payloadId));
            return Ok();
        }

    }
}
