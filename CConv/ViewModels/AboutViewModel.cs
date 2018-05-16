namespace CConv.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string HomeUrl { get; set; }
        public string GitHubUrl { get; set; }

        public AboutViewModel(string homeUrl, string githubUrl)
        {
            HomeUrl = homeUrl;
            GitHubUrl = githubUrl;
        }
    }
}
