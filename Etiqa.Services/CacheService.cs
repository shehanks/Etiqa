using Etiqa.Services.Contract;
using System.Runtime.Caching;

namespace Etiqa.Services
{
    public sealed class CacheService : ICacheService
    {
        private ObjectCache memoryCache = MemoryCache.Default;

        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)memoryCache.Get(key);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveData(string key)
        {
            try
            {
                var removed = true;
                if (!string.IsNullOrEmpty(key))
                {
                    var result = memoryCache.Remove(key);
                }
                else
                    removed = false;

                return removed;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveByPrefix(string prefix)
        {
            try
            {
                if (!string.IsNullOrEmpty(prefix))
                {
                    var removeKeys = memoryCache
                        .Where(x => x.Key.StartsWith(prefix))
                        .Select(y => y.Key);
                    foreach (var key in removeKeys)
                        memoryCache.Remove(key);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset? expirationTime = null)
        {
            try
            {
                if (expirationTime == null)
                    expirationTime = DateTimeOffset.Now.AddMinutes(2);
                var set = true;
                if (!string.IsNullOrEmpty(key))
                    memoryCache.Set(key, value, expirationTime.Value);
                else
                    set = false;

                return set;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
