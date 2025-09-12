using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IDatabaseSeederService
    {
        public Task<Unit> SeedInitialDataAsync(CancellationToken cancellationToken = default);
    }
}