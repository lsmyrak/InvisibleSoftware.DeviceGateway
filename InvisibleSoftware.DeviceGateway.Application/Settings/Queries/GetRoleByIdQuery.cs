using InvisibleSoftware.Devicegateway.Domain;

using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using InvisibleSoftware.DeviceGateway.Application.Settings.Commands.Dtos;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Settings.Queries
{
    public class GetRoleByIdQuery : IRequest<RoleDto>
    {
        public Guid Id { get; set; }

        public GetRoleByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRepository _repository;

        public GetRoleByIdQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetByIdAsync<Role>(request.Id, cancellationToken);
            if (role == null || role.IsDeleted || !role.isEnabled)
            {
                return new RoleDto();
            }
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };
        }
    }
}