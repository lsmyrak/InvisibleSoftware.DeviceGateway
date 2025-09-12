using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries
{
    public class GetDeviceGroupLookupQuery : IRequest<LookupResponse<NameRelatedDto>>
    {
    }

    public class GetDeviceGroupLookupQueryHandler : IRequestHandler<GetDeviceGroupLookupQuery, LookupResponse<NameRelatedDto>>
    {
        private readonly IRepository _repository;

        public GetDeviceGroupLookupQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<LookupResponse<NameRelatedDto>> Handle(GetDeviceGroupLookupQuery request, CancellationToken cancellationToken)
        {
            var deviceTypes = await _repository.GetAllAsync<DeviceGroup>(cancellationToken);

            var dto = deviceTypes.Where(dt => dt.isEnabled && !dt.IsDeleted)
                .Select(dt => new NameRelatedDto
                {
                    Id = dt.Id,
                    Name = dt.Name
                }).ToList();

            var response = new LookupResponse<NameRelatedDto>
            {
                Columns = new List<LookupColumn>
            {
                new LookupColumn { Key = "Id", Label = nameof(DeviceGroup.Id), Display = false },
                new LookupColumn { Key = "Name", Label = nameof(DeviceGroup.Name), Display = true }
            },
                Data = dto
            };

            return response;
        }
    }
}