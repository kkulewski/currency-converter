using CConv.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CConv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvertPage : ContentPage
    {
        private readonly ConvertViewModel _vm;
        public ConvertPage()
        {
            InitializeComponent();
            BindingContext = _vm = new ConvertViewModel();
            InitializeViewModel();
        }

        private async void InitializeViewModel()
        {
            await _vm.LoadProviders();
        }
    }
}
