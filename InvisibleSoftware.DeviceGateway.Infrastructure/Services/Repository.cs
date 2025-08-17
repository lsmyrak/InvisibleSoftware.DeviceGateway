using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext _context;
        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T entity,CancellationToken cancellationToken) where T : class
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

        public Task<T> GetByIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : class
        {
            throw new NotImplementedException();
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
        public string GenerateCode<T>()  where T : BaseAggregate
        {
            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            int countToday =  _context.Set<T>()
                .Where(h => h.CreatedAt >= today && h.CreatedAt < tomorrow)
                .Count();

            int nextNumber = countToday + 1;
            string numberPart = nextNumber.ToString("D4");

            return $"{datePart}/{numberPart}";
        }
    }
}
