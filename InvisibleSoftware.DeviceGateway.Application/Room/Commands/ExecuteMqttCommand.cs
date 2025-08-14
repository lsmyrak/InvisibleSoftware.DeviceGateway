using InvisibleSoftware.DeviceGateway.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly ILogger<ExecuteMqttCommandHandler> _logger;        
        public ExecuteMqttCommandHandler(IMqttService mqttService,ILogger<ExecuteMqttCommandHandler> logger)
        {
            _mqttService = mqttService;
            _logger = logger;
        }
        public async Task<bool> Handle(ExecuteMqttCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing MQTT command with ID: {Id}", request.Id);
            
            return await _mqttService.SendAsync(request.Id,nameof(ExecuteMqttCommand),cancellationToken);            
        }
    }
}
