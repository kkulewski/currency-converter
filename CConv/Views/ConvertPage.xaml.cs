using CConv.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CConv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvertPage : ContentPage
    {
        private ConvertViewModel _viewModel;

        public ConvertPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ConvertViewModel();
        }
    }
}