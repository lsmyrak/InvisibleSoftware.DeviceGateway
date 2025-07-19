using InvisibleSoftware.Devicegateway.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvisibleSoftware.DeviceGateway.Infrastructure
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext()
        {            
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<CommandHistory> CommandHistories { get; set; }
        public DbSet<Device> Devices { get; set; }        
        public DbSet<DeviceGroup> DeviceGroups { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<MqttPayload> MqttPayloads { get; set; }
        public DbSet<MqttPayloadOrder> MqttPayloadOrders { get; set; }          
        public DbSet<Place> Places { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MqttConfig> MqttConfigs { get; set; }

    }
}
