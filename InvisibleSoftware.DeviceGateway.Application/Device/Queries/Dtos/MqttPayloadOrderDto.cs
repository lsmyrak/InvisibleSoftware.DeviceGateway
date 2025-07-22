namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos
{
    public class MqttPayloadOrderDto:BaseDto
    {
        public virtual MqttPayloadDto MqttPayload { get; set; }
        public int DisplayOrder { get; set; }
    }
}