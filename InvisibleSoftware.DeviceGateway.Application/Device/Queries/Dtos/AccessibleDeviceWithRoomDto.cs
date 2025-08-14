namespace InvisibleSoftware.DeviceGateway.Application.Device.Queries.Dtos
{
    public class AccessibleDeviceWithRoomDto
    {
        public DeviceDto Device { get; set; } = new DeviceDto();       

        public static  AccessibleDeviceWithRoomDto Convert(Devicegateway.Domain.Device d)
        {
            return new AccessibleDeviceWithRoomDto
            {
                Device = new DeviceDto
                {                   
                    Id = d.Id,
                    IpAddress = d.IpAddress,    
                    Name = d.Name,
                    Description = d.Description,
                    FirmwareVersion = d.FirmwareVersion,
                    LastSeen = d.LastSeen,
                    Manufacturer = d.Manufacturer,
                    Model = d.Model,
                    SerialNumber = d.SerialNumber,
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt,
                    Room = new RoomDto
                    {
                        Id = d.Room.Id,
                        Name = d.Room.Name,
                        Description = d.Room.Description,
                        CreatedAt = d.Room.CreatedAt,
                        UpdatedAt = d.Room.UpdatedAt
                    },
                    DeviceGroups = d.DeviceGroups.Select(g => new DeviceGroupDto
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description,
                        CreatedAt = g.CreatedAt,
                        UpdatedAt = d.DeviceType.UpdatedAt
                    }).ToList(),
                    DeviceType = new DeviceTypeDto
                    {
                        Id = d.DeviceType.Id,
                        Name = d.DeviceType.Name,
                        Description = d.DeviceType.Description,
                        Category = d.DeviceType.Category,
                        CreatedAt= d.DeviceType.CreatedAt,
                        UpdatedAt = d.DeviceType.UpdatedAt,                        
                    },                    
                    MqttPayloadOrders = d.MqttPayloadOrders.Select(m => new MqttPayloadOrderDto
                    {
                        MqttPayload = new MqttPayloadDto
                        {
                            Id = m.MqttPayload.Id,
                            CommandName = m.MqttPayload.CommandName,
                            DisplayCommandName = m.MqttPayload.DisplayCommandName,
                            Topic = m.MqttPayload.Topic,
                            Payload = m.MqttPayload.Payload,                            
                            Name = m.MqttPayload.Name,
                            Description = m.MqttPayload.Description,
                            CreatedAt = m.MqttPayload.CreatedAt,
                            UpdatedAt = m.MqttPayload.UpdatedAt
                        },
                        DisplayOrder = m.DisplayOrder
                    }).ToList(),
                },               
            };
        }
    }
}
