using CConv.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CConv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvertPage : ContentPage
    {
        private readonly ConvertViewModel _vm;
        public ConvertPage(ConvertViewModel vm)
        {
            InitializeComponent();
            BindingContext = _vm = vm;
        }

        protected override async void OnAppearing()
        {
            await _vm.LoadProviders();
        }
    }
}
