using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;

namespace InvisibleSoftware.DeviceGateway.Application.Room.Queries.Dtos
{
    public class DeviceDto: BaseDto
    {
        public string IpAddress { get; set; } = string.Empty;
        public virtual List<MqttPayloadOrderDto> MqttPayloadOrders { get; set; } = new List<MqttPayloadOrderDto>();
        public virtual List<DeviceGroupDto> DeviceGroups { get; set; }
        public virtual DeviceTypeDto DeviceType { get; set; }
        public virtual RoomDto Room { get; set; }
        public DateTime? LastSeen { get; set; }
    }
}

