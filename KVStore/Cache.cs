using Common;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVStore
{
    public class Cache : DataCache
    {
        public void SetCache_System(string CacheKey, object obj)
        {
            SetCache(CacheKey, obj);
        }

        public List<T> GetCache_System<T>(string CacheKey)
        {
            return GetCache<T>(CacheKey);
        }

        public List<T> GetCache<T>(string CacheKey)
        {
            var result = new List<T>();
            var list = (Dictionary<string, string>)GetCache(CacheKey);
            if (list != null && list.Count > 0)
            {
                foreach (KeyValuePair<string, string> kv in list)
                {
                    var value = JsonSerializer.DeserializeFromString<T>(kv.Value);
                    result.Add(value);
                }
            }
            return result;
        }

        public T GetCache<T>(string CacheKey, string key)
        {
            return (T)GetCache(CacheKey);
        }

        public void Remove(string CacheKey)
        {
            ClearCache(CacheKey);
        }

        public void Remove_System(string CacheKey)
        {
            ClearCache(CacheKey);
        }
    }
}
