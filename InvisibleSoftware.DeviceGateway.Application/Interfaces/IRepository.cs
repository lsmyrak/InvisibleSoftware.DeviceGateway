using InvisibleSoftware.Devicegateway.Domain;

namespace InvisibleSoftware.DeviceGateway.Application.Interfaces
{
    public interface IRepository
    {        
        public Task<List<T>> GetAllAsync<T>(CancellationToken cancellationToken) where T : class;
        public Task<T> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : class;
        public Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class;
        public Task UpdateAsync<T>(T entity, CancellationToken cancellationToken) where T : class;
        public Task DeleteAsync<T>(Guid id,CancellationToken cancellationToken) where T : class;
        public Task SaveChangesAsync(CancellationToken cancellationToken);

        public string GenerateCode<T>() where T : BaseAggregate;

    }
}
