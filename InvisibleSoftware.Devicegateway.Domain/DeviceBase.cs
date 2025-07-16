namespace InvisibleSoftware.Devicegateway.Domain
{
    public class DeviceBase : BaseAggregate
    {
        public virtual DeviceType DeviceType { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public DateTime? LastSeen { get; set; }

    }
}
