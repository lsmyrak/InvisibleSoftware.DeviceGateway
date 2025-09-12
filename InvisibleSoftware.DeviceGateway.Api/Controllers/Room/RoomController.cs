using InvisibleSoftware.DeviceGateway.Application.Room.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvisibleSoftware.DeviceGateway.Api.Controllers.Room
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IMediator _mediator;

        public RoomController(ILogger<RoomController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("rooms-with-devices")]
        [Authorize]
        public async Task<IActionResult> GetRoomsWithDevices()
        {
            var roomsWithDevices = await _mediator.Send(new GetAccessibleDevicesQuery());
            return Ok(roomsWithDevices);
        }

        [HttpGet("room/{Id}/devices")]
        [Authorize]
        public async Task<IActionResult> GeDevicesByRoom(Guid Id)
        {
            var roomsWithDevices = await _mediator.Send(new GetAccessibleDevicesQuery());
            return Ok(roomsWithDevices);
        }
    }
}