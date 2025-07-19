using Microsoft.AspNetCore.Identity;

namespace InvisibleSoftware.Devicegateway.Domain
{
    public class User : IdentityUser 
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; } = 1;
        public bool IsDeleted { get; set; } = false;
        public bool isEnabled { get; set; } = true;
    }
}
