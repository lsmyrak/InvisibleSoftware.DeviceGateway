using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly ApplicationContext _context;
        public HistoryService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateHistoryCodeAsync(CancellationToken cancellationToken)
        {
            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
     
            int countToday = await _context.CommandHistories
                .Where(h => h.CreatedAt.Date == DateTime.UtcNow.Date)
                .CountAsync();

            int nextNumber = countToday + 1;
            string numberPart = nextNumber.ToString("D4");

            return $"Hist/{datePart}/{numberPart}";
        }

        public async Task SaveEvent(CommandHistory commandHistory, CancellationToken cancellationToken)
        {
            await _context.CommandHistories.AddAsync(commandHistory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
