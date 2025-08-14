namespace InvisibleSoftware.Devicegateway.Domain
{
    public class Place : BaseAggregate
    {
        public string Location { get; set; } = string.Empty;
        public string Addreass { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
