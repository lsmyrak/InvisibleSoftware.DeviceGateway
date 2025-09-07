using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Device.Commands;
using InvisibleSoftware.DeviceGateway.Application.Device.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvisibleSoftware.DeviceGateway.Api.Controllers.Device
{
    [ApiController]
    [Route("api/[controller]")]
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
            var deviceWithRoom = await _mediator.Send(new GetAccessibleDevicesWithRoomsQuery());
            return Ok(deviceWithRoom);
        }

        [HttpPost("execute-command/{payloadId}")]
        [Authorize]
        public async Task<IActionResult> ExecuteCommand(Guid payloadId)
        {
            await _mediator.Send(new RunMqttCommand(payloadId));
            return Ok();
        }

        [HttpGet("lookup-device-type")]
        public async Task<LookupResponse<NameRelatedDto>> GetLoopupDeviceTypes()
        {
            return await _mediator.Send(new GetDeviceTypeLookupQuery());
        }

        [HttpGet("lookup-device-group")]
        public async Task<LookupResponse<NameRelatedDto>> GetLoopupDeviceGroup()
        {
            return await _mediator.Send(new GetDeviceGroupLookupQuery());
        }
        [HttpGet("loopup-room")]
        public async Task<LookupResponse<NameRelatedDto>> GetLoopuRooms()
        {
            return await _mediator.Send(new GetRoomLookupQuery());
        }

        [HttpPost("device/add")]
        [Authorize(Roles = "Admin,DeviceManager")]
        public async Task<IActionResult> AddDevice([FromBody] AddDeviceCommand command)
        {           
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
