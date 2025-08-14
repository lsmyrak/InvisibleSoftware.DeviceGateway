using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;
namespace InvisibleSoftware.DeviceGateway.Application.Room.Queries.Dtos
{
    public class MqttPayloadOrderDto:BaseDto
    {
        public virtual MqttPayloadDto MqttPayload { get; set; }
        public int DisplayOrder { get; set; }
    }
}