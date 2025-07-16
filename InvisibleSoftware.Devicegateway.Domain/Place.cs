namespace InvisibleSoftware.Devicegateway.Domain
{
    public class Place : BaseAggregate
    {
        public string Location { get; set; }
        public string Addreass { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
