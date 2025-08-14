using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;

namespace InvisibleSoftware.DeviceGateway.Application.Room.Queries.Dtos
{
    public class MqttPayloadDto:BaseDto
    {
        public string DisplayCommandName { get; set; } = string.Empty;
        public string CommandName { get; set; } = string.Empty;
    }
}