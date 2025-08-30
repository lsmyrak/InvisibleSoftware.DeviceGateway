using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using InvisibleSoftware.DeviceGateway.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace InvisibleSoftware.DeviceGateway.Application.Room.Commands
{
    public class ExecuteMqttCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ExecuteMqttCommand(Guid id)
        {
            Id = id;
        }
    }
    public class ExecuteMqttCommandHandler : IRequestHandler<ExecuteMqttCommand, bool>
    {
        private readonly IMqttService _mqttService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ExecuteMqttCommandHandler> _logger;
        private readonly IRepository _repository;
        public ExecuteMqttCommandHandler(IMqttService mqttService, ILogger<ExecuteMqttCommandHandler> logger, IHttpContextAccessor httpContextAccessor, IRepository repository)
        {
            _mqttService = mqttService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }
        public async Task<bool> Handle(ExecuteMqttCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogError("Request is null in {HandlerName}", nameof(ExecuteMqttCommandHandler));
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isPermission = await CheckPermission(userId, request.Id, cancellationToken);
            if(!isPermission)
            {
                _logger.LogWarning("User {UserId} does not have permission to execute MQTT command with ID: {Id}", userId, request.Id);
                return false;
            }
            _logger.LogInformation("Executing MQTT command with ID: {Id}", request.Id);

            return await _mqttService.SendAsync(request.Id, nameof(ExecuteMqttCommand), cancellationToken);
        }
        async Task<bool> CheckPermission(string userId, Guid payloadId, CancellationToken cancellationToken)
        {
            return await _repository.AnyAsync<MqttPayloadOrder>(
                m => m.MqttPayload.Id == payloadId
                     && m.Device.Room.Users.Any(u => u.Id == userId),
                cancellationToken);
        }


    }
}
