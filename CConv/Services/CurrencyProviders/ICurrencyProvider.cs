using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CConv.Models;

namespace CConv.Services
{
    public interface ICurrencyProvider
    {
        string Name { get; }

        IList<ICurrency> Currencies { get; }

        DateTime UpdatedOn { get; }

        Task<bool> Fetch();
    }
}
