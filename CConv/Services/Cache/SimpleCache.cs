using System;

namespace CConv.Services.Cache
{
    internal class SimpleCache<T> : ICache<T> where T: class
    {
        private T _item;
        private DateTime _updatedOn;
        private TimeSpan _lifetime;

        public SimpleCache(TimeSpan lifetime)
        {
            _lifetime = lifetime;
        }

        public void Set(T item, TimeSpan lifetime)
        {
            _lifetime = lifetime;
            Set(item);
        }

        public void Set(T item)
        {
            _item = item;
            _updatedOn = DateTime.Now;
        }

        public T Get()
        {
            return _item;
        }

        public bool Valid()
        {
            return DateTime.Now - _updatedOn >= _lifetime;
        }
    }
}
