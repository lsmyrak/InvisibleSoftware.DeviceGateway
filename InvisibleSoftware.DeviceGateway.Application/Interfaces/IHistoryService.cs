using InvisibleSoftware.Devicegateway.Domain;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IHistoryService
    {
        public Task SaveEvent(CommandHistory commandHistory, CancellationToken cancellationToken);
        public Task<string> GenerateHistoryCodeAsync(CancellationToken cancellationToken);
    }
}
