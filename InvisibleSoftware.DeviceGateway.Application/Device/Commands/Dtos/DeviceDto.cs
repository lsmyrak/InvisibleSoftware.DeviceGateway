namespace InvisibleSoftware.DeviceGateway.Application.Device.Commands.Dtos
{
    public class DeviceDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public Guid DeviceTypeId { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;

        public Guid RoomId { get; set; }
        public List<Guid> DeviceGroupIds { get; set; } = new();

        public List<MqttPayloadOrderDto> MqttPayloadOrders { get; set; } = new();
    }

    public class MqttPayloadOrderDto
    {
        public Guid MqttPayloadId { get; set; }
        public int DisplayOrder { get; set; }
    }
}