using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace KVStore
{
    public class WebCache : Cache
    {
        const int DefaultCacheExpiredMinutes = 20;

        public int ExpiredMinutes
        {
            get;
            private set;
        }
        public WebCache()
            : this(DefaultCacheExpiredMinutes)
        { }

        public WebCache(int expiredMinutes)
        {
            this.ExpiredMinutes = expiredMinutes;
        }

        public T Get<T>(string key)
        {
            object val = GetCache<T>(key,key);//本地版本
            //object val = GetCache<T>(key); //服务器版本
            if (val != null && val is T)
            {
                return (T)val;
            }
            return default(T);
        }
        public T Get<T>(string key, Func<T> getData)
        {
            return this.Get<T>(key, getData, TimeSpan.FromMinutes(DefaultCacheExpiredMinutes));
        }

        public T Get<T>(string key, Func<T> getData, TimeSpan timespan)
        {
            T val = this.Get<T>(key);
            if (val == null)
            {
                val = getData();
                if (val != null)
                {
                    this.Set(key, val, timespan);
                }
            }
            return val;
        }

        public void Set(string key, object data)
        {
            Set(key, data, TimeSpan.FromMinutes(ExpiredMinutes));
        }

        public void Set(string key, object data, TimeSpan cacheTime)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (data == null)
            {
                return;
            }

            DateTime time = DateTime.Now.Add(cacheTime);
            DateTime now = DateTime.Now;
            if (now >= time)
            {
                throw new Exception("时间设置不正确");
            }
            SetCache(key,data);
        }
    }
}
