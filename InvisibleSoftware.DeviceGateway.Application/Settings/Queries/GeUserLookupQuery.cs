using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Common.Shared.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvisibleSoftware.DeviceGateway.Application.Settings.Queries
{
    public class GeUserLookupQuery : IRequest<LookupResponse<NameCodeRelatedDto>>
    {
    }

    public class GeUserLookupQueryHandler : IRequestHandler<GeUserLookupQuery, LookupResponse<NameCodeRelatedDto>>
    {
        private readonly ILogger<GeUserLookupQueryHandler> _logger;
        private readonly IRepository _repository;

        public GeUserLookupQueryHandler(ILogger<GeUserLookupQueryHandler> logger, IRepository repository)
        {
            _repository = repository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<LookupResponse<NameCodeRelatedDto>> Handle(GeUserLookupQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync<User>(cancellationToken);
            var dto = users.Where(u => u.isEnabled && !u.IsDeleted)
                .Select(u => new NameCodeRelatedDto
                {
                    Id = Guid.Parse(u.Id),
                    Name = u.UserName,
                    Code = u.Code
                }).ToList();
            var response = new LookupResponse<NameCodeRelatedDto>
            {
                Columns = new List<LookupColumn>
             {
                 new LookupColumn { Key = "Id", Label = nameof(User.Id), Display = false },
                 new LookupColumn { Key = "Name", Label = nameof(User.UserName), Display = true },
                 new LookupColumn { Key = "Code", Label = nameof(User.Code), Display = true }
             },
                Data = dto
            };
            return response;
        }
    }
}