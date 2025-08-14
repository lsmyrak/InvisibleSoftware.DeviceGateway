using InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Room.Queries
{
    public class GetAccessibleDevicesByRoomQuery :IRequest<List<AccessibleDeviceWithRoomDto>>
    {
        public Guid RoomId { get; set; }
        public GetAccessibleDevicesByRoomQuery(Guid roomId)
        {
            RoomId = roomId;
        }
    }
    public class GetAccessibleDevicesByRoomQueryHandler : IRequestHandler<GetAccessibleDevicesByRoomQuery,List<AccessibleDeviceWithRoomDto>>
    {
        private readonly IDeviceAccessService _deviceAccessService;
        public GetAccessibleDevicesByRoomQueryHandler(IDeviceAccessService deviceAccessService)
        {
            _deviceAccessService = deviceAccessService;
        }
        public async Task<List<AccessibleDeviceWithRoomDto>> Handle(GetAccessibleDevicesByRoomQuery request, CancellationToken cancellationToken)
        {
           var devices =  await _deviceAccessService.GetAccessibleDevicesByRoomAsync(request.RoomId);
           return devices.Select(AccessibleDeviceWithRoomDto.Convert).ToList();
        }
    }

}
