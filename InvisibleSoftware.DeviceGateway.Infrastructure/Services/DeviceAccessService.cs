using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos;
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
        public async Task<List<AccessibleDeviceWithRoomDto>> GetAccessibleDevicesWithRoomsAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var devicesWithRooms = await _context.Set<Device>()
                .Include(d => d.Room).ThenInclude(n=>n.Users).Where(d => d.Room.Users.Any(u => u.Id == userId)).ToListAsync(); 
            if(devicesWithRooms == null || devicesWithRooms.Count == 0)
            {
                return new List<AccessibleDeviceWithRoomDto>();
            }
            var accessibleDevicesWithRooms = devicesWithRooms.Select(d => new AccessibleDeviceWithRoomDto
            {
                Device = new DeviceDto
                {
                    CreatedAt = d.CreatedAt,
                    Id = d.Id,
                    IpAddress = d.IpAddress,
                    Description = d.Description,
                    DeviceGroups = d.DeviceGroups.Select(g => new DeviceGroupDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList(),
                    DeviceType = new DeviceTypeDto
                    {
                        Id = d.DeviceType.Id,
                        Name = d.DeviceType.Name
                    },
                    FirmwareVersion = d.FirmwareVersion,
                    LastSeen = d.LastSeen,
                    Manufacturer = d.Manufacturer,
                    Model = d.Model,
                    MqttPayloadOrders = d.MqttPayloadOrders.Select(m => new MqttPayloadOrderDto
                    {
                       MqttPayload = new MqttPayloadDto
                       {
                           Id = m.MqttPayload.Id,
                           Name = m.MqttPayload.Name,
                           Topic = m.MqttPayload.Topic,
                           Payload = m.MqttPayload.Payload
                       },
                       DisplayOrder = m.DisplayOrder
                    }).ToList(),
                },
                Room = new RoomDto
                {
                    Id = d.Room.Id,
                    Name = d.Room.Name
                }
            }).ToList();
            return accessibleDevicesWithRooms;
        }        
    }
}
