using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Auth.Queries
{
    public class GetRoleLookupQuery : IRequest<LookupResponse<NameRelatedDto>>
    {
    }
    public class GetRoleLookupQueryHandler : IRequestHandler<GetRoleLookupQuery, LookupResponse<NameRelatedDto>>
    {
        private readonly IRepository _repository;
        public GetRoleLookupQueryHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<LookupResponse<NameRelatedDto>> Handle(GetRoleLookupQuery request, CancellationToken cancellationToken)
        {
            var roles = await _repository.GetAllAsync<Role>(cancellationToken);


            var dto = roles.Where(r => r.isEnabled && !r.IsDeleted)
                .Select(r => new NameRelatedDto
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList();
            var response = new LookupResponse<NameRelatedDto>
            {
                Columns = new List<LookupColumn>
            {
                new LookupColumn { Key = "Id", Label = nameof(Role.Id), Display = false },
                new LookupColumn { Key = "Name", Label = nameof(Role.Name), Display = true }
            },
                Data = dto
            };
            return response;
        }

    }
}
