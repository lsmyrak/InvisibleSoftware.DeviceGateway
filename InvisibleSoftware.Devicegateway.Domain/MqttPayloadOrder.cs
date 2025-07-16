namespace InvisibleSoftware.Devicegateway.Domain
{
    public class MqttPayloadOrder
    {
        public virtual MqttPayload MqttPayload { get; set; }
        public int DisplayOrder { get; set; }
    }
}
