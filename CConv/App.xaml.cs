﻿using System;
using System.Collections.Generic;
using System.Linq;
using CConv.Services.Conversion;
using CConv.Services.CurrencyProviders;
using CConv.ViewModels;
using Xamarin.Forms;

namespace CConv
{
    public partial class App : Application
    {
        private DateTime _sleepStart;
        private readonly TimeSpan _sleepDurationLimit;

        public App()
        {
            ConfigureContainer();
            InitializeComponent();
            MainPage = new Views.MainPage();

            _sleepDurationLimit = TimeSpan.FromMinutes(30);
        }

        protected override async void OnStart()
        {
            var providers = Container.Resolve<IList<ICurrencyProvider>>();
            foreach (var p in providers)
            {
                await p.Load();
            }

            if (providers.Any(p => p.UpdatedOn == DateTime.MinValue))
            {
                MessagingCenter.Send(this, MessageType.UninitializedRates);
            }
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

        private static void ConfigureContainer()
        {
            Container.Register(new CurrencyConversionService());
            Container.Register(new List<ICurrencyProvider>
            {
                new FakeCurrencyProvider(),
                new NbpCurrencyProvider(NbpTable.A),
                new NbpCurrencyProvider(NbpTable.B)
            });

            Container.Register(new ConvertViewModel(
                Container.Resolve<ICurrencyConversionService>(),
                Container.Resolve<IList<ICurrencyProvider>>()
            ));
        }
    }
}
