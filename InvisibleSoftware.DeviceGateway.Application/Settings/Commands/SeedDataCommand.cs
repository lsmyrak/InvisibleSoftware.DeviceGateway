using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvisibleSoftware.DeviceGateway.Application.Settings.Commands
{
    public class SeedDataCommand:IRequest<Unit>
    {

    }
    public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand, Unit>
    {
        private readonly ILogger<SeedDataCommandHandler> _logger;
        private readonly IDatabaseSeederService _databaseSeederService;
        public SeedDataCommandHandler(ILogger<SeedDataCommandHandler> logger, IDatabaseSeederService databaseSeederService)
        {
            _databaseSeederService = databaseSeederService;
            _logger = logger;
        }
        public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            return await _databaseSeederService.SeedInitialDataAsync(cancellationToken);
        }
    }
}
