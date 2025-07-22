using InvisibleSoftware.Devicegateway.Domain;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IHistoryService
    {
        public Task SaveEvent(CommandHistory commandHistory, CancellationToken cancellationToken);
        string GenerateHistoryCode();
    }
}
