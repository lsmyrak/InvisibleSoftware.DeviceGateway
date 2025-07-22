using InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IDeviceAccessService
    {
        public Task<List<AccessibleDeviceWithRoomDto>> GetAccessibleDevicesWithRoomsAsync();
    }
}
