namespace CConv.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string HomeUrl { get; set; }
        public string GitHubUrl { get; set; }

        public AboutViewModel()
        {
            HomeUrl = "kkulewski.pl";
            GitHubUrl = "github.com/kkulewski";
        }
    }
}
