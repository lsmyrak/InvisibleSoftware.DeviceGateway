using InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries
{
    public class GetAccessibleDevicesWithRoomsQuery : IRequest<List<AccessibleDeviceWithRoomDto>>
    {                 
    }
    public class GetAccessibleDevicesWithRoomsQueryHandler : IRequestHandler<GetAccessibleDevicesWithRoomsQuery, List<AccessibleDeviceWithRoomDto>>
    {
        private readonly IDeviceAccessService _deviceAccessService;
        public GetAccessibleDevicesWithRoomsQueryHandler(IDeviceAccessService deviceAccessService)
        {
            _deviceAccessService = deviceAccessService;
        }
        public async Task<List<AccessibleDeviceWithRoomDto>> Handle(GetAccessibleDevicesWithRoomsQuery request, CancellationToken cancellationToken)
        {
            var devices = await _deviceAccessService.GetAccessibleDevicesWithRoomsAsync();
            return  devices.Select(m=> AccessibleDeviceWithRoomDto.Convert(m)).ToList();
        }
    }
}
