namespace InvisibleSoftware.Devicegateway.Domain
{
    public class CommandHistory : BaseAggregate
    {
        public string EventName { get; set; } = string.Empty;
        public virtual Device Device { get; set; }
        public virtual MqttPayloadOrder MqttPayloadOrder { get; set; }
        public DateTime CommandTime { get; set; } = DateTime.UtcNow;
    }
}