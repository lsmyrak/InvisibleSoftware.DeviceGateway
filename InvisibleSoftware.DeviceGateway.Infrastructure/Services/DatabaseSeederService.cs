using InvisibleSoftware.Devicegateway.Domain;
using InvisibleSoftware.DeviceGateway.Application.Auth.Commands.Dtos;
using InvisibleSoftware.DeviceGateway.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InvisibleSoftware.DeviceGateway.Infrastructure.Services
{
    public class DatabaseSeederService : IDatabaseSeederService
    {
        private readonly ApplicationContext _context;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public DatabaseSeederService(ApplicationContext context, IAuthService authService, UserManager<User> userManager)
        {
            _context = context;
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<Unit> SeedInitialDataAsync(CancellationToken cancellationToken)
        {
            if (!_context.DeviceTypes.Any())
            {
                var registerUser = new RegisterDto();
                registerUser.UserName = "admin";
                registerUser.Password = "AdmiN@123456";
                registerUser.Email = "admin@admin";

                var test = await _authService.RegisterAsync(registerUser, cancellationToken);

                var user = await _userManager.FindByEmailAsync(registerUser.Email);

                var blindsType = new DeviceType { Name = "Blinds", Category = "ESP" };

                var lightsType = new DeviceType { Name = "Lights", Category = "ESP" };

                var place = new Place
                {
                    Name = "Control room",
                };

                await _context.DeviceTypes.AddRangeAsync(blindsType, lightsType);
                await _context.Places.AddAsync(place);

                var users = new List<User>();
                users.Add(user);

                var room = new Room
                {
                    Name = "Control room",
                    Users = users,
                    Place = place
                };
                await _context.Rooms.AddAsync(room);

                var smallBlindsDevice = new Device
                {
                    Name = "Small Blinds",
                    DeviceType = blindsType,
                    Room = room,
                    Model = "ESP",
                    Manufacturer = "ESP"
                };

                var largeBlindsDevice = new Device
                {
                    Name = "Large Blinds",
                    DeviceType = blindsType,
                    Room = room,
                    Model = "ESP",
                    Manufacturer = "ESP"
                };

                var lightDevice = new Device
                {
                    Name = "Lights",
                    DeviceType = lightsType,
                    Room = room,
                    Model = "ESP",
                    Manufacturer = "ESP"
                };

                _context.Devices.AddRange(smallBlindsDevice, largeBlindsDevice, lightDevice);
                await _context.SaveChangesAsync(cancellationToken);

                var smallBlindsPayloads = new[]
                {
                CreatePayload("SmallManualUp", "small_manual_up", "node00/blinds/manual/small_blind/command", "100", smallBlindsDevice),
                CreatePayload("SmallManualDown", "small_manual_down", "node00/blinds/manual/small_blind/command", "-100", smallBlindsDevice),
                CreatePayload("SmallOpen", "small_open", "node00/blinds/cover/small_blind/command", "open", smallBlindsDevice),
                CreatePayload("SmallClose", "small_close", "node00/blinds/cover/small_blind/command", "close", smallBlindsDevice),
                CreatePayload("SmallStop", "small_stop", "node00/blinds/cover/small_blind/command", "stop", smallBlindsDevice),
        };

                var largeBlindsPayloads = new[]
                {
            CreatePayload("LargeManualUp", "large_manual_up", "blinds/manual/roleta_duza/command", "100", largeBlindsDevice),
            CreatePayload("LargeManualDown", "large_manual_down", "blinds/manual/roleta_duza/command", "-100", largeBlindsDevice),
            CreatePayload("LargeOpen", "large_open", "blinds/cover/roleta_duza/command", "OPEN", largeBlindsDevice),
            CreatePayload("LargeClose", "large_close", "blinds/cover/roleta_duza/command", "CLOSE", largeBlindsDevice),
            CreatePayload("LargeStop", "large_stop", "blinds/cover/roleta_duza/command", "STOP", largeBlindsDevice),
        };

                var lightPayloads = new[]
                {
            CreatePayload("FirstOn", "light_1_on", "lights/first/on", "1", lightDevice),
            CreatePayload("FirstOff", "light_1_off", "lights/first/off", "0", lightDevice),
            CreatePayload("SecondOn", "light_2_on", "lights/second/on", "1", lightDevice),
            CreatePayload("SecondOff", "light_2_off", "lights/second/off", "0", lightDevice),
            CreatePayload("ThirdOn", "light_3_on", "lights/third/on", "1", lightDevice),
            CreatePayload("ThirdOff", "light_3_off", "lights/third/off", "0", lightDevice),
        };

                _context.MqttPayloads.AddRange(smallBlindsPayloads);
                _context.MqttPayloads.AddRange(largeBlindsPayloads);
                _context.MqttPayloads.AddRange(lightPayloads);

                await _context.SaveChangesAsync(cancellationToken);

                AddPayloadOrders(smallBlindsPayloads, smallBlindsDevice);
                AddPayloadOrders(largeBlindsPayloads, largeBlindsDevice);
                AddPayloadOrders(lightPayloads, lightDevice);

                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }

        private MqttPayload CreatePayload(string commandName, string displayName, string topic, string payload, Device device)
        {
            return new MqttPayload
            {
                CommandName = commandName,
                DisplayCommandName = displayName,
                Topic = topic,
                Payload = payload,
                Device = device
            };
        }

        private void AddPayloadOrders(MqttPayload[] payloads, Device device)
        {
            int order = 0;
            foreach (var payload in payloads)
            {
                _context.MqttPayloadOrders.Add(new MqttPayloadOrder
                {
                    MqttPayload = payload,
                    Device = device,
                    DisplayOrder = order++
                });
            }
        }
    }
}