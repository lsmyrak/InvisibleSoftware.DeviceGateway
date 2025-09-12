using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries
{
    public class GetPayloadLookupQuery : IRequest<LookupResponse<NameRelatedDto>>
    {
    }

    public class GetPayloadLookupQueryHandler : IRequestHandler<GetPayloadLookupQuery, LookupResponse<NameRelatedDto>>
    {
        private readonly IRepository _repository;

        public GetPayloadLookupQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<LookupResponse<NameRelatedDto>> Handle(GetPayloadLookupQuery request, CancellationToken cancellationToken)
        {
            var payloads = await _repository.GetAllAsync<MqttPayload>(cancellationToken);
            var data = payloads.Where(p => p.isEnabled && !p.IsDeleted)
                .Select(p => new NameRelatedDto
                {
                    Id = p.Id,
                    Name = p.DisplayCommandName
                }).ToList();

            var response = new LookupResponse<NameRelatedDto>
            {
                Columns = new List<LookupColumn>
                {
                    new LookupColumn { Key = "Id", Label = nameof(MqttPayload.Id), Display = false },
                    new LookupColumn { Key = "Name", Label = nameof(MqttPayload.DisplayCommandName), Display = true }
                },
                Data = data
            };
            return response;
        }
    }
}