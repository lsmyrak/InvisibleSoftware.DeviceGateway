using InvisibleSoftware.DeviceGateway.Application.Device.Commands.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Device.Commands
{
    public class AddDeviceCommand : IRequest<Guid>
    {
        public DeviceDto Device { get; set; }

        public AddDeviceCommand(DeviceDto device)
        {
            Device = device;
        }
    }

    public class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, Guid>
    {
        private readonly IRepository _repository;

        public AddDeviceCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
        {
            var deviceGroups = new List<Devicegateway.Domain.DeviceGroup>();
            var deviceType = await _repository.GetByIdAsync<Devicegateway.Domain.DeviceType>(request.Device.DeviceTypeId, cancellationToken);
            foreach (var deviceGroupId in request.Device.DeviceGroupIds)
            {
                var deviceGroup = await _repository.GetByIdAsync<Devicegateway.Domain.DeviceGroup>(deviceGroupId, cancellationToken);
                if (deviceGroup != null)
                {
                    deviceGroups.Add(deviceGroup);
                }
            }

            var room = await _repository.GetByIdAsync<Devicegateway.Domain.Room>(request.Device.RoomId, cancellationToken);

            var device = new Devicegateway.Domain.Device
            {
                Name = request.Device.Name,
                Description = request.Device.Description,
                Code = _repository.GenerateCode<Devicegateway.Domain.Device>(),
                IpAddress = request.Device.IpAddress,
                DeviceType = deviceType,
                Room = room,
                DeviceGroups = deviceGroups,
            };

            await _repository.AddAsync(device, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return device.Id;
        }
    }
}