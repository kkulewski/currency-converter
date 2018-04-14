namespace CConv.Services.Cache
{
    public interface ICache<T> where T: class
    {
        bool Expired { get; }
        void Expire();
        T Get();
        void Set(T item);
    }
}
