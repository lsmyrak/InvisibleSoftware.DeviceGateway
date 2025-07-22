using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvisibleSoftware.Devicegateway.Domain
{
    public class Room :BaseAggregate
    {
      public virtual Place Place { get; set; }
      public virtual ICollection<Device> Devices { get; set; }
      public virtual List<User> Users { get; set; } = new List<User>();      
    }
}
