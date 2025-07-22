namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos
{
    public class AccessibleDeviceWithRoomDto
    {
        public DeviceDto Device { get; set; } = new DeviceDto();
        public RoomDto Room { get; set; } = new RoomDto();


    }
}
