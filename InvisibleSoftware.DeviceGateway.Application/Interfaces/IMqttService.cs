using InvisibleSoftware.Devicegateway.Domain;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public interface IMqttService
    {
        Task<bool> ConnectAsync(CancellationToken cancellationToken);
        Task<bool> SendAsync(MqttPayloadOrder mqttPayloadOrder, string eventName, CancellationToken cancellationToken);
        Task<TResult> SendRequestAsync<TResult>(string request, string response, TimeSpan timeout, string payload, CancellationToken cancellationToken);
    }
}
