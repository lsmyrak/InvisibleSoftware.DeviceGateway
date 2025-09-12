using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Device.Commands.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Device.Commands
{
    public class AddDeviceTypeCommand : IRequest<Guid>
    {
        public DeviceTypeDto DeviceType { get; set; }
    }

    public class AddDeviceTypeCommandHandler : IRequestHandler<AddDeviceTypeCommand, Guid>
    {
        private readonly IRepository _repository;

        public AddDeviceTypeCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddDeviceTypeCommand request, CancellationToken cancellationToken)
        {
            var deviceType = new DeviceType
            {
                Name = request.DeviceType.Name,
                Code = _repository.GenerateCode<DeviceType>(),
                Description = request.DeviceType.Description,
            };

            await _repository.AddAsync(deviceType, cancellationToken);
            return deviceType.Id;
        }
    }
}