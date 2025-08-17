using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Settings.Commands
{
    public class AddDeviceGroupCommand:IRequest<Guid>
    {
        public DeviceGroupDto DeviceGroup { get; set; }
    }
    public class AddDeviceGroupCommandHandler : IRequestHandler<AddDeviceGroupCommand, Guid>
    {
        private readonly IRepository _repository;
        public AddDeviceGroupCommandHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> Handle(AddDeviceGroupCommand request, CancellationToken cancellationToken)
        {           
            var deviceGroup = new DeviceGroup
            {
                Name = request.DeviceGroup.Name,
                Code = $"DeviceGroup/{_repository.GenerateCode<DeviceGroup>}",
                Description = request.DeviceGroup.Description,                
            };
            await _repository.AddAsync(deviceGroup, cancellationToken);
            return deviceGroup.Id;
        }
    }
}
