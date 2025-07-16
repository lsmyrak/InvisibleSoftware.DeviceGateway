namespace InvisibleSoftware.Devicegateway.Domain
{
    public class Role:BaseAggregate
    {
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
