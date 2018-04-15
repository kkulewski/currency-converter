using System;

namespace CConv.Services.Cache
{
    internal class SimpleCache<T> : ICache<T> where T: class
    {
        private T _item;
        private TimeSpan _lifetime;

        public SimpleCache(TimeSpan lifetime)
        {
            _lifetime = lifetime;
        }

        public T Get()
        {
            return _item;
        }

        public void Set(T item, TimeSpan lifetime)
        {
            _lifetime = lifetime;
            Set(item);
        }

        public void Set(T item)
        {
            _item = item;
            UpdatedOn = DateTime.Now;
        }

        public DateTime UpdatedOn { get; set; }

        public bool Expired => DateTime.Now - UpdatedOn >= _lifetime;

        public void Expire()
        {
            UpdatedOn = DateTime.MinValue;
        }
    }
}
