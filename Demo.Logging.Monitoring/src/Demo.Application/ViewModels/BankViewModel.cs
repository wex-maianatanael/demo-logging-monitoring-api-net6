namespace Demo.Application.ViewModels
{
    // todo: add annotations to be validated via model state
    public class BankViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
    }
}
