using System;

namespace CConv.Services.Cache
{
    public interface ICache<T> where T: class
    {
        T Get();
        void Set(T item);
        DateTime UpdatedOn { get; }
        bool Expired { get; }
        void Expire();
    }
}
