using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using InvisibleSoftware.DeviceGateway.Application.Settings.Queries.Dtos;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Settings.Commands
{
    public class AddRoleCommand : IRequest<Guid>
    {
        public RoleDto Role { get; set; }
    }

    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Guid>
    {
        private readonly IRepository _repository;

        public AddRoleCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Code = _repository.GenerateCode<Role>(),
                Name = request.Role.Name,
                Description = request.Role.Description
            };
            await _repository.AddAsync(role, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return role.Id;
        }
    }
}