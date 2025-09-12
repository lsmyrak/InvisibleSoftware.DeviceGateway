namespace InvisibleSoftware.Devicegateway.Domain
{
    public class MqttPayload : BaseAggregate
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
        public string CommandName { get; set; }
        public string DisplayCommandName { get; set; }
        public virtual Device Device { get; set; }
    }
}