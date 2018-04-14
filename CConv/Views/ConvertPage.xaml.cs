using CConv.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CConv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvertPage : ContentPage
    {
        public ConvertPage()
        {
            InitializeComponent();
            BindingContext = Container.Resolve<ConvertViewModel>();
        }
    }
}