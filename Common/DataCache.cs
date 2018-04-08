using System;
using System.Web;
using System.Collections;
using System.Web.Caching;

namespace Common
{
    /// <summary>
    /// 缓存相关的操作类
    /// Copyright (C) Maticsoft
    /// </summary>
    public class DataCache
    {
        public static bool Exists(string CacheKey)
        {
            if (GetCache(CacheKey) != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void ClearCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 根据Key清空对应缓存
        /// </summary>
        /// <param name="Key"></param>
        public static void ClearCache(string Key)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(Key);
        }
    }
}
