namespace Demo.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}
