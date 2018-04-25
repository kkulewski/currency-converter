using System;
using CConv.Messages;
using Xamarin.Forms;

namespace CConv
{
    public partial class App : Application
    {
        private DateTime _sleepStart;
        private readonly TimeSpan _sleepDurationLimit;

        public App()
        {
            InitializeComponent();
            MainPage = new Views.MainPage();
            _sleepDurationLimit = TimeSpan.FromMinutes(60);
        }

        protected override void OnSleep()
        {
            _sleepStart = DateTime.Now;
        }

        protected override void OnResume()
        {
            if (DateTime.Now - _sleepStart > _sleepDurationLimit)
            {
                MessagingCenter.Send(this, MessageType.OutdatedRates);
            }
        }
    }
}
