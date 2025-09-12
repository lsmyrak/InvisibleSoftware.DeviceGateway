namespace InvisibleSoftware.Devicegateway.Domain
{
    public class MqttConfig : BaseAggregate
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BrokerAddress { get; set; }
        public int BrokerPort { get; set; }
        public string ClientId { get; set; }
    }
}