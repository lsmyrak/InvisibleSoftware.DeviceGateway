namespace InvisibleSoftware.DeviceGateway.Application.Device.Commands.Dtos
{
    public class DeviceDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public Guid DeviceTypeId { get; set; }
        public List<Guid> DeviceGroupIds { get; set; }
        public Guid RoomId { get; set; }

    }
}
