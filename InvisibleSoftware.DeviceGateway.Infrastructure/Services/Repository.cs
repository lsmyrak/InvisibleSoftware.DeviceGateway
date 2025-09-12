using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task DeleteAsync<T>(Guid id, CancellationToken cancellationToken) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync<T>(CancellationToken cancellationToken) where T : class
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : class
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync<T>(T entity, CancellationToken cancellationToken) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public string GenerateCode<T>() where T : BaseAggregate
        {
            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            int countToday = _context.Set<T>()
                .Where(h => h.CreatedAt >= today && h.CreatedAt < tomorrow)
                .Count();

            int nextNumber = countToday + 1;
            string numberPart = nextNumber.ToString("D4");

            return $"{nameof(T)}/{datePart}/{numberPart}";
        }

        public async Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken cancellationToken = default) where T : class
        {
            IQueryable<T> query = _context.Set<T>();
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class
        {
            return await _context.Set<T>().AnyAsync(predicate, cancellationToken);
        }

        public Task<T> GetUserById<T>(string id, CancellationToken cancellationToken) where T : class
        {
            return _context.Set<T>().FindAsync(id, cancellationToken).AsTask();
        }
    }
}