using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvisibleSoftware.DeviceGateway.Application.Settings
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
