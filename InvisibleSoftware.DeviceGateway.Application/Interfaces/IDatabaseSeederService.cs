using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IDatabaseSeederService
    {
        Task <Unit> SeedInitialDataAsync(CancellationToken cancellationToken = default);
    }
}
