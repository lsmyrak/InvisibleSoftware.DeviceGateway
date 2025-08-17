using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly ApplicationContext _context;
        public HistoryService(ApplicationContext context)
        {
            _context = context;
        }
        public  string GenerateHistoryCode()
        {
            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            int countToday =  _context.Users
                .Where(h => h.CreatedAt >= today && h.CreatedAt < tomorrow)
                .Count();

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
