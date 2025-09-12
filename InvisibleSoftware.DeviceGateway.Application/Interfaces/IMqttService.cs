using InvisibleSoftware.Devicegateway.Domain;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public interface IMqttService
    {
        public Task<bool> ConnectAsync(CancellationToken cancellationToken);

        public Task<bool> SendAsync(MqttPayloadOrder mqttPayloadOrder, string eventName, CancellationToken cancellationToken);

        public Task<bool> SendAsync(Guid payloadId, string eventName, CancellationToken cancellationToken);

        public Task<TResult> SendRequestAsync<TResult>(string request, string response, TimeSpan timeout, string payload, CancellationToken cancellationToken);
    }
}