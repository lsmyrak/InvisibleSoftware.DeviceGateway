using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries
{
    public class GetDeviceTypeLookupQuery : IRequest<LookupResponse<NameRelatedDto>>
    {
    }

    public class GetDeviceTypeLookupQueryHandler : IRequestHandler<GetDeviceTypeLookupQuery, LookupResponse<NameRelatedDto>>
    {
        private readonly IRepository _repository;
        public GetDeviceTypeLookupQueryHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<LookupResponse<NameRelatedDto>> Handle(GetDeviceTypeLookupQuery request, CancellationToken cancellationToken)
        {
            var deviceTypes = await _repository.GetAllAsync<DeviceType>(cancellationToken);
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
                new LookupColumn { Key = "Id", Label = nameof(DeviceType.Id), Display = false },
                new LookupColumn { Key = "Name", Label = nameof(DeviceType.Name), Display = true }
            },
                Data = dto
            };

            return response;
        }
        
    }
}
