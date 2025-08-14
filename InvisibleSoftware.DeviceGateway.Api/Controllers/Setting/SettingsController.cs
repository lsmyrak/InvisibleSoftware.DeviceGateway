using InvisibleSoftware.DeviceGateway.Application.Settings;
using MediatR;
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
    }
}
