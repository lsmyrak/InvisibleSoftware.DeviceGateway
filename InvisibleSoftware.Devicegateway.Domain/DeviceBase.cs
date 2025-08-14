namespace InvisibleSoftware.Devicegateway.Domain
{
    public class DeviceBase : BaseAggregate
    {
        public virtual DeviceType DeviceType { get; set; }
        public string Manufacturer { get; set; } 
        public string Model { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;
        public DateTime? LastSeen { get; set; }

    }
}
