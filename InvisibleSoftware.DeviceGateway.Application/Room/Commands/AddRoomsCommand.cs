using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvisibleSoftware.DeviceGateway.Application.Room.Commands
{
    public class AddRoomsCommand :IRequest<Unit>
    {
        public List<string> RoomNames { get; set; } = new List<string>();
    }
}
