using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries
{
    public class GetRoomLookupQuery : IRequest<LookupResponse<NameRelatedDto>>
    {
    }

    public class GetRoomLookupQueryHandler : IRequestHandler<GetRoomLookupQuery, LookupResponse<NameRelatedDto>>
    {
        private readonly IRepository _repository;

        public GetRoomLookupQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<LookupResponse<NameRelatedDto>> Handle(GetRoomLookupQuery request, CancellationToken cancellationToken)
        {
            var deviceTypes = await _repository.GetAllAsync<Devicegateway.Domain.Room>(cancellationToken);

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
                new LookupColumn { Key = "Id", Label = nameof(Devicegateway.Domain.Room.Id), Display = false },
                new LookupColumn { Key = "Name", Label = nameof(Devicegateway.Domain.Room.Name), Display = true }
            },
                Data = dto
            };

            return response;
        }
    }
}