namespace Demo.Application.ViewModels
{
    // todo: add annotations to be validated via model state
    public class CustomerViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public AccountViewModel Account { get; set; }
    }
}
