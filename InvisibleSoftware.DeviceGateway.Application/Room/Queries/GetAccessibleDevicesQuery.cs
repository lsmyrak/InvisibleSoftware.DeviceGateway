using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using InvisibleSoftware.DeviceGateway.Application.Room.Queries.Dtos;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Room.Queries
{
    public class GetAccessibleDevicesQuery : IRequest<List<AccessibleDeviceWithRoomDto>>
    {
    }

    public class GetAccessibleDevicesQueryHandler : IRequestHandler<GetAccessibleDevicesQuery, List<AccessibleDeviceWithRoomDto>>
    {
        private readonly IDeviceAccessService _deviceAccessService;

        public GetAccessibleDevicesQueryHandler(IDeviceAccessService deviceAccessService)
        {
            _deviceAccessService = deviceAccessService;
        }

        public async Task<List<AccessibleDeviceWithRoomDto>> Handle(GetAccessibleDevicesQuery request, CancellationToken cancellationToken)
        {
            var devices = await _deviceAccessService.GetAccessibleDevicesWithRoomsAsync();
            return devices.Select(m => AccessibleDeviceWithRoomDto.Convert(m)).ToList();
        }
    }
}