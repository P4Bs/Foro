using ForoWebApp.Database.Documents;

namespace ForoWebApp.Models.ViewModels
{
    public class ThemeViewModel
    {
        public string ThemeId { get; set; }
        public string ThemeTitle { get; set; }
        public IList<ForumThread> Threads { get; set; }
    }
}
