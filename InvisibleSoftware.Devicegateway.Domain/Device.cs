namespace InvisibleSoftware.Devicegateway.Domain
{
    public class Device :DeviceBase
    {
        public string IpAddress { get; set; } = string.Empty;
        public virtual List<MqttPayloadOrder> MqttPayloadOrders { get; set; }     
        public virtual List<DeviceGroup> DeviceGroups { get; set; }         
        public virtual Room Room { get; set; }
    }
}