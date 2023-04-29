namespace Demo.Domain.Entities
{
    public class Bank : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Account> Accounts { get; set; }
    }
}
