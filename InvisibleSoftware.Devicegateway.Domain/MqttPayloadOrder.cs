namespace InvisibleSoftware.Devicegateway.Domain
{
    public class MqttPayloadOrder : BaseAggregate
    {
        public virtual MqttPayload MqttPayload { get; set; }
        public virtual Device Device { get; set; }
        public int DisplayOrder { get; set; }
    }
}