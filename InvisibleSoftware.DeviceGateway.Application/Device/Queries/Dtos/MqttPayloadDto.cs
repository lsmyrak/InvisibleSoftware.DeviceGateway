namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos
{
    public class MqttPayloadDto:BaseDto
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
        public string CommandName { get; set; }
        public string DisplayCommandName { get; set; }
    }
}