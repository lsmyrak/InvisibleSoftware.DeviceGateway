using InvisibleSoftware.DeviceGateway.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvisibleSoftware.DeviceGateway.Application.Device.Commands
{
    public class RunMqttCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public RunMqttCommand(Guid id)
        {
            Id = id;
        }
    }
    public class RunMqttCommandHandler : IRequestHandler<RunMqttCommand, bool>
    {
        private readonly IMqttService _mqttService;
        private readonly ILogger<RunMqttCommandHandler> _logger;        
        public RunMqttCommandHandler(IMqttService mqttService,ILogger<RunMqttCommandHandler> logger)
        {
            _mqttService = mqttService;
            _logger = logger;
        }
        public async Task<bool> Handle(RunMqttCommand request, CancellationToken cancellationToken)
        {
          return  await _mqttService.SendAsync(request.Id,nameof(RunMqttCommand),cancellationToken);            
        }
    }
}
