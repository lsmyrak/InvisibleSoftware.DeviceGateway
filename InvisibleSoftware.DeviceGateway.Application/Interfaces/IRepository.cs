using InvisibleSoftware.Devicegateway.Domain;
using System.Linq.Expressions;

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
        Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken cancellationToken = default) 
            where T : class;
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class;

    }
}
