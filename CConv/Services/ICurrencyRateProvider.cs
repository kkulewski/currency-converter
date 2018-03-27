using System.Collections.Generic;
using CConv.Models;

namespace CConv.Services
{
    public interface ICurrencyRateProvider
    {
        IList<ICurrency> Currencies { get; }
    }
}
