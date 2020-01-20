using IhChegou.Cache.Contract;
using IhChegou.Global.Enumerators;
using IhChegou.Global.Extensions.Object;
using IhChegou.Global.Extensions.String;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IhChegou.Cache
{
    public class CacheManager : ICacheManager
    {
        private ConnectionMultiplexer Redis { get; set; }
        private IDatabase Database { get; set; }

        public CacheManager()
        {
#if DEBUG
            Redis = ConnectionMultiplexer.Connect("localhost:6379");
#else
            Redis = ConnectionMultiplexer.Connect("saturno:6375");
#endif
            Database = Redis.GetDatabase(1);
        }

        public void Delete(CacheKey key, string Id)
        {
            string rediskey = MakeKey(key, Id);
            DeleteKey(rediskey);
        }
        public void DeleteHash(CacheKey key, string Id, string hashId)
        {
            string rediskey = MakeKey(key, Id);
            DeleteHash(rediskey, hashId);
        }

        public void Set(CacheKey key, string Id, object value, TimeSpan? TTL = null)
        {
            string rediskey = MakeKey(key, Id);
            string obj = value.ToJson();
            SetKey(rediskey, obj, TTL);
        }

        public void SetHash(CacheKey key, string Id, string hashId, object value)
        {
            string rediskey = MakeKey(key, Id);
            string obj = value.ToJson();
            SetHash(rediskey, hashId, value.ToJson());
        }


        public T Get<T>(CacheKey key, string Id)
        {
            var value = GetKey(MakeKey(key, Id));
            return value.ToObject<T>();
        }

        public List<KeyValuePair<string, T>> GetHashs<T>(CacheKey key, string Id)
        {
            var value = GetAllHashs(MakeKey(key, Id));

            var response = new List<KeyValuePair<string, T>>();

            foreach (var item in value)
            {
                var hash = new KeyValuePair<string, T>(item.Name, item.Value.ToString().ToObject<T>());
                response.Add(hash);
            }
            return response;
        }


        private static string MakeKey(CacheKey key, string Id)
        {
            return $"{key.ToString()}:{Id}";
        }

        private void SetKey(string key, string value, TimeSpan? TTL = null)
        {
            Database.StringSet(key, value, TTL ?? TimeSpan.FromDays(1.5));
        }
        private string GetKey(string key)
        {
            return Database.StringGet(key);
        }
        private void DeleteKey(string rediskey)
        {
            Database.KeyDelete(rediskey);
        }
        private void SetHash(string redisKey, string hashId, string value)
        {
            HashEntry[] entry = { new HashEntry(hashId, value) };
            Database.HashSet(redisKey, entry);
        }
        private void DeleteHash(string redisKey, string hashId)
        {
            Database.HashDelete(redisKey, hashId);
        }
        private List<HashEntry> GetAllHashs(string redisKey)
        {
            return Database.HashGetAll(redisKey)?.ToList();
        }
    }
}