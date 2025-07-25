﻿using InvisibleSoftware.DeviceGateway.Application.Device.Queries;
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

        [HttpGet("get-device-with-rooms")]
        [Authorize]
        public async Task<IActionResult> GetDeviceWithRoom()
        {
           var deviceWithRoom  = await _mediator.Send(new GetAccessibleDevicesWithRoomsQuery());
            return Ok(deviceWithRoom);
        }
    }
}
