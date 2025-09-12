using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;

namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos
{
    public class DeviceDto : BaseDto
    {
        public string IpAddress { get; set; } = string.Empty;
        public virtual List<MqttPayloadOrderDto> MqttPayloadOrders { get; set; } = new List<MqttPayloadOrderDto>();
        public virtual List<DeviceGroupDto> DeviceGroups { get; set; }
        public virtual DeviceTypeDto DeviceType { get; set; }
        public virtual RoomDto Room { get; set; } = new RoomDto();
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public DateTime? LastSeen { get; set; }
    }
}