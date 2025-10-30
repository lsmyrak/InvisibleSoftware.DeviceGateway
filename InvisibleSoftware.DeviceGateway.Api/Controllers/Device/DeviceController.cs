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

        #region Posts devices

        [HttpPost("add-device")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDevice([FromBody] AddDeviceCommand command)

        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("add-device-type")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDeviceGroup([FromBody] AddDeviceGroupCommand command)
        {
            if (command == null)
            {
                return BadRequest("Command cannot be null.");
            }
            await _mediator.Send(command);
            return Ok("Device group added successfully.");
        }

        #endregion Posts devices        

        #region Execute Command

        [HttpPost("execute-command/{payloadId}")]
        [Authorize]
        public async Task<IActionResult> ExecuteCommand(Guid payloadId)
        {
            await _mediator.Send(new ExecuteMqttCommand(payloadId));
            return Ok();
        }

        #endregion Execute Command

        #region lookups

        [HttpGet("lookup-device-type")]
        [Authorize]
        public async Task<LookupResponse<NameRelatedDto>> GetLoopupDeviceTypes()
        {
            return await _mediator.Send(new GetDeviceTypeLookupQuery());
        }

        [HttpGet("lookup-device-group")]
        [Authorize]
        public async Task<LookupResponse<NameRelatedDto>> GetLoopupDeviceGroup()
        {
            return await _mediator.Send(new GetDeviceGroupLookupQuery());
        }

        [HttpGet("loopup-room")]
        [Authorize]
        public async Task<LookupResponse<NameRelatedDto>> GetLoopuRooms()
        {
            return await _mediator.Send(new GetRoomLookupQuery());
        }

        [HttpGet("lookup-payload")]
        [Authorize]
        public async Task<IActionResult> GetLookupPayload(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPayloadLookupQuery());
            return Ok(result);
        }

        #endregion lookups
    }
}