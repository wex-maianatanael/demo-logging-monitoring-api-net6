namespace Demo.Application.ViewModels
{
    public class BankViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
    }
}
