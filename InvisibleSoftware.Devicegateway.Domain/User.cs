namespace InvisibleSoftware.Devicegateway.Domain
{
    public class User :BaseAggregate
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();


    }
}
