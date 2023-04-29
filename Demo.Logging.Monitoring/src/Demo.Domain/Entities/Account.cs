namespace Demo.Domain.Entities
{
    public class Account : BaseEntity
    {
        public int Number { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Balance { get; set; }
        public bool Active { get; set; }

        public Guid BankID { get; set; }
        public virtual Bank Bank { get; set; }

        public Guid CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
