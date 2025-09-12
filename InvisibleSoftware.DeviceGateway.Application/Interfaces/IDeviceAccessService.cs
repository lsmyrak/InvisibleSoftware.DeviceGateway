namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IDeviceAccessService
    {
        public Task<List<Devicegateway.Domain.Device>> GetAccessibleDevicesWithRoomsAsync();

        public Task<List<Devicegateway.Domain.Device>> GetAccessibleDevicesByRoomAsync(Guid Id);
    }
}