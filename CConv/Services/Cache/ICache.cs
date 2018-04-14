namespace CConv.Services.Cache
{
    public interface ICache<T> where T: class
    {
        void Set(T item);
        T Get();
        bool Valid();
    }
}
