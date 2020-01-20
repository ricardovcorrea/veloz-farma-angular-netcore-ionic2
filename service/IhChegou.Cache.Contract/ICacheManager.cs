using IhChegou.Global.Enumerators;
using System;
using System.Collections.Generic;

namespace IhChegou.Cache.Contract
{
    public interface ICacheManager
    {
        void Delete(CacheKey key, string Id);
        void DeleteHash(CacheKey key, string Id, string hashId);
        T Get<T>(CacheKey key, string Id);
        List<KeyValuePair<string, T>> GetHashs<T>(CacheKey key, string Id);
        void Set(CacheKey key, string Id, object value, TimeSpan? TTL = default(TimeSpan?));
        void SetHash(CacheKey key, string Id, string hashId, object value);
    }
}