using InvisibleSoftware.DeviceGateway.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace InvisibleSoftware.DeviceGateway.Application.Room.Commands
{
    public class ExecuteMqttCommand:IRequest<bool>
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
        public ExecuteMqttCommandHandler(IMqttService mqttService,ILogger<ExecuteMqttCommandHandler> logger,IHttpContextAccessor httpContextAccessor)
        {
            _mqttService = mqttService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(ExecuteMqttCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogError("Request is null in {HandlerName}", nameof(ExecuteMqttCommandHandler));
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(CheckPermission(userId,request.Id))
            {
                _logger.LogWarning("User {UserId} does not have permission to execute MQTT command with ID: {Id}", userId, request.Id);
                return false;
            }            
            _logger.LogInformation("Executing MQTT command with ID: {Id}", request.Id);
            
            return await _mqttService.SendAsync(request.Id,nameof(ExecuteMqttCommand),cancellationToken);            
        }
        bool CheckPermission(string userId, Guid commandId)
        {

            return true;
        }
    }
}
