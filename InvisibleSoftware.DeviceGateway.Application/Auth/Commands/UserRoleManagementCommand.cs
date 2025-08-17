using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;

namespace InvisibleSoftware.DeviceGateway.Application.Auth.Commands
{
    public class UserRoleManagementCommand:IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
    public class UserRoleManagementCommandHandler : IRequestHandler<UserRoleManagementCommand, Guid>
    {
        private readonly IRepository _repository;
        public UserRoleManagementCommandHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> Handle(UserRoleManagementCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync<User>(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new ArgumentException("User not found", nameof(request.UserId));
            }
            var roles = new List<Role>();
            foreach (var roleId in request.RoleIds)
            {
                var role = await _repository.GetByIdAsync<Role>(roleId, cancellationToken);               
                roles.Add(role);
            }                        
            user.Role.Clear();
            user.Role.AddRange(roles);
            await _repository.UpdateAsync<User>(user, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return Guid.Parse(user.Id);
        }
    }
}
