namespace Demo.Application.ViewModels
{
    // todo: add annotations to be validated via model state
    public class AccountViewModel
    {
        public Guid ID { get; set; }
        public int Number { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public decimal Balance { get; set; }
        public bool Active { get; set; }

        public Guid BankID { get; set; } //todo: use automapper to map this property
        public Guid CustomerID { get; set; } //todo: use automapper to map this property
    }
}
