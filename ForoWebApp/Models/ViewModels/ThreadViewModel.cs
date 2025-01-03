namespace ForoWebApp.Models.ViewModels
{
    public class ThreadViewModel
    {
        public string ThreadId { get; set; }
        public string ThreadName { get; set; }
        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}
