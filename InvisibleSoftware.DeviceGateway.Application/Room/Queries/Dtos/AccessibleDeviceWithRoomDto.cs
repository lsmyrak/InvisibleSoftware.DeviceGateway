namespace InvisibleSoftware.DeviceGateway.Application.Room.Queries.Dtos
{
    public class AccessibleDeviceWithRoomDto
    {
        public DeviceDto Device { get; set; } = new DeviceDto();       
    
     public static AccessibleDeviceWithRoomDto Convert(Devicegateway.Domain.Device d)
        {
            return new AccessibleDeviceWithRoomDto
            {
                Device = new DeviceDto
                {                    
                    Id = d.Id,                  
                    Name = d.Name,
                    Description = d.Description,
                    DeviceGroups = d.DeviceGroups.Select(g => new DeviceGroupDto
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description
                    }).ToList(),
                    DeviceType = new DeviceTypeDto
                    {
                        Id = d.DeviceType.Id,
                        Category = d.DeviceType.Category,
                        Name = d.DeviceType.Name,
                        Description = d.DeviceType.Description,                        
                    },
                    Room = new RoomDto
                    {
                        Id = d.Room.Id,
                        Name = d.Room.Name,
                        Description = d.Room.Description,
                    },
                    MqttPayloadOrders = d.MqttPayloadOrders.Select(m => new MqttPayloadOrderDto
                    {
                        MqttPayload = new MqttPayloadDto
                        {
                            Id = m.MqttPayload.Id,
                            Name = m.MqttPayload.CommandName,
                            DisplayCommandName = m.MqttPayload.DisplayCommandName,
                        },
                        DisplayOrder = m.DisplayOrder,
                    }).ToList(),
                },
            };
        }
    }
}
