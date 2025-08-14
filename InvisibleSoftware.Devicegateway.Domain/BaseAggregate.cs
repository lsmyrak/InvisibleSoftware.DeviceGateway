namespace InvisibleSoftware.Devicegateway.Domain
{
    public class BaseAggregate
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreateByFunction { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }    
        public int Version { get; set; } = 1;
        public bool IsDeleted { get; set; } = false;
        public bool isEnabled { get; set; } = true; 
        public void MarkAsDeleted()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
