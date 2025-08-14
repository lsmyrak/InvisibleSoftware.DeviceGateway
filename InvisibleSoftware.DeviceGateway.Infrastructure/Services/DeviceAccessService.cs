using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public class DeviceAccessService : IDeviceAccessService
    {
        private readonly ApplicationContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeviceAccessService(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Device>> GetAccessibleDevicesByRoomAsync(Guid Id)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var devicesWithRooms = await _context.Set<Device>()
              .Include(d => d.Room).ThenInclude(n => n.Users)
              .Include(m => m.DeviceType)
              .Include(d => d.Room).ThenInclude(r => r.Place)
              .Include(m => m.DeviceGroups)
              .Include(m => m.MqttPayloadOrders).ThenInclude(m => m.MqttPayload)
              .Where(d => d.Room.Users.Any(u => u.Id == userId) && d.Room.Id==Id).AsNoTracking()
              .ToListAsync();
            if (devicesWithRooms == null || devicesWithRooms.Count == 0)
            {
                return new List<Device>();
            }
            return devicesWithRooms;
        }

        public async Task<List<Device>> GetAccessibleDevicesWithRoomsAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var devicesWithRooms = await _context.Set<Device>()
                .Include(d => d.Room).ThenInclude(n=>n.Users)
                .Include(m => m.DeviceType)
                .Include(d => d.Room).ThenInclude(r => r.Place)
                .Include(m =>m.DeviceGroups)
                .Include(m => m.MqttPayloadOrders).ThenInclude(m => m.MqttPayload).AsNoTracking()
                .Where(d => d.Room.Users.Any(u => u.Id == userId))
                .ToListAsync(); 
            if(devicesWithRooms == null || devicesWithRooms.Count == 0)
            {
                return new List<Device>();
            }
            return devicesWithRooms;
            
        }        
    }
}
