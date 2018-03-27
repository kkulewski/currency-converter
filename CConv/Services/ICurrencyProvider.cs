using System.Collections.Generic;
using CConv.Models;

namespace CConv.Services
{
    public interface ICurrencyProvider
    {
        string Name { get; }

        IList<ICurrency> Currencies { get; }
    }
}
