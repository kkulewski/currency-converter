﻿namespace CConv.Models
{
    internal interface ICurrency
    {
        /// <summary>
        /// Currency short name.
        /// Example: EUR, USD
        /// </summary>
        string ShortName { get; set; }

        /// <summary>
        /// Currency long name.
        /// Example: Euro, United States dollar
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Conversion rate to EUR.
        /// </summary>
        decimal Rate { get; set; }

        /// <summary>
        /// Currency exchange rate provider.
        /// </summary>
        IExchangeRateProvider Provider { get; set; }
    }
}