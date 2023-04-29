namespace Demo.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual Account Account { get; set; }
    }
}
