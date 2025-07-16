using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MQTTnet;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public class MqttService : IMqttService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<MqttService> _logger;
        private readonly IMqttClient _mqttClient;
        private readonly IHistoryService _historyService;

        //Todo Move to setting File or Database
        private string _brokerAddress = "192.168.253.50";
        private int _brokerPort = 1883;
        private string _username = "dev29a";
        private string _password = "Immolation@138";
        private string _clientId = "DeviceGatewayClient";
        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingRequests = new();

        public MqttService(ApplicationContext context, ILogger<MqttService> logger, IHistoryService historyService)
        {
            var factory = new MqttClientFactory();
            _context = context;
            _historyService = historyService;
            _logger = logger;
            _mqttClient = factory.CreateMqttClient();
            _mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
        }

        private async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs args)
        {
            var topic = args.ApplicationMessage.Topic;
            var payload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
            _logger.LogInformation("Received message on topic {Topic}: {Payload}", topic, payload);

            if (_pendingRequests.TryRemove(topic, out var tcs))
            {
                tcs.TrySetResult(payload);
            }
        }

        public async Task<bool> ConnectAsync(CancellationToken cancellationToken)
        {
            if (_mqttClient.IsConnected)
            {
                _logger.LogInformation("MQTT client is already connected.");
                return true;
            }
            var options = new MqttClientOptionsBuilder()
                .WithClientId(_clientId)
                .WithTcpServer(_brokerAddress, _brokerPort)
                .WithCredentials(_username, _password)
                .WithCleanSession()
                .Build();
            try
            {
                await _mqttClient.ConnectAsync(options, cancellationToken);
                _logger.LogInformation("Connected to MQTT broker at {Address}:{Port}", _brokerAddress, _brokerPort);
                var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(f =>
                    {
                        f.WithTopic("devices/response");
                        f.WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce);
                    })
                    .Build();
                await _mqttClient.SubscribeAsync(subscribeOptions, cancellationToken);
                _logger.LogInformation("Subscribed to topic 'devices/response'");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to MQTT broker at {Address}:{Port}", _brokerAddress, _brokerPort);
                return false;
            }
        }

        public async Task<bool> SendAsync(MqttPayloadOrder mqttPayloadOrder, string eventName, CancellationToken cancellationToken)
        {
            var mqttConfig = await _context.MqttConfigs.SingleOrDefaultAsync(m => m.isEnabled == true);
            if (mqttConfig != null)
            {
                _brokerAddress = mqttConfig.BrokerAddress;
                _brokerPort = mqttConfig.BrokerPort;
                _username = mqttConfig.UserName;
                _password = mqttConfig.Password;
            }
            if (!_mqttClient.IsConnected)
            {
                _logger.LogWarning("MQTT client is not connected. Cannot send message.");
                await ConnectAsync(cancellationToken);
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(mqttPayloadOrder.MqttPayload.Topic)
                .WithPayload(mqttPayloadOrder.MqttPayload.Payload)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce)
                .Build();
            try
            {
                var result = await _mqttClient.PublishAsync(message, cancellationToken);
                var code = await _historyService.GenerateHistoryCodeAsync(cancellationToken);

                await _historyService.SaveEvent(new CommandHistory
                {
                    EventName = eventName,
                    Code = code,
                    Device = mqttPayloadOrder.MqttPayload.Device,
                    MqttPayloadOrder = mqttPayloadOrder,
                    CommandTime = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    Description = $"Sent MQTT message to topic {mqttPayloadOrder.MqttPayload.Topic} with payload {mqttPayloadOrder.MqttPayload.Payload}",
                    Version = 1,
                    Name = $"{eventName}_{mqttPayloadOrder.MqttPayload.Device}"
                }, cancellationToken);
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send MQTT message to topic {Topic}", mqttPayloadOrder.MqttPayload.Topic);
                return false;
            }
        }

        public async Task<TResult> SendRequestAsync<TResult>(string request, string response, TimeSpan timeout, string payload, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<string>();
            _pendingRequests[response] = tcs;

           var mqttPlayloadOrder =  new MqttPayloadOrder
            {
                MqttPayload = new MqttPayload
                {
                    Topic = request,
                    Payload = payload,
                    Device = null
                }
            };
            await SendAsync(mqttPlayloadOrder, "MQTT Request Generic MQTT Service", cancellationToken);

            var completedTask = await Task.WhenAny(tcs.Task, Task.Delay(timeout));
            if (completedTask == tcs.Task)
            {
                var responseJson = await tcs.Task;
                return JsonSerializer.Deserialize<TResult>(responseJson);
            }
            else
            {
                throw new TimeoutException($"No response on topic {response} within {timeout.TotalSeconds}s");
            }
        }
    }
}
