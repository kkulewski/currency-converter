using CConv.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CConv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage(AboutViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
