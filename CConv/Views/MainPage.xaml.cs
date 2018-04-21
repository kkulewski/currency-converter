using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CConv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<App>
            (
                this,
                MessageType.UninitializedRates,
                sender => DisplayAlert
                (
                    MessageType.UninitializedRates,
                    "Some exchange rate providers might not be available until fetched.",
                    "Close"
                )
            );

            MessagingCenter.Subscribe<App>
            (
                this,
                MessageType.OutdatedRates,
                sender => DisplayAlert
                (
                    MessageType.OutdatedRates,
                    "Exchange rates might be out of date. Fetch is recommended.",
                    "Close"
                )
            );
        }
    }
}
