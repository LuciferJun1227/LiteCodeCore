using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LuckyCode.Core.Redis
{
    public class RedisCacheClient : ICacheClient
    {
        private RedisClientManager _manager;
        public RedisCacheClient(RedisClientManager manager)
        {
            _manager = manager;
        }
        public IDatabase Database => _manager.GetDatabase();

        public bool Add<T>(string key, T value)
        {
            return Database.StringSet(key, JsonConvert.SerializeObject(value));
        }

        public bool Add<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);

            return Database.StringSet(key, JsonConvert.SerializeObject(value), expiration);
        }

        public async Task<bool> AddAsync<T>(string key, T value)
        {
            return await Database.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task<bool> AddAsync<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);

            return await Database.StringSetAsync(key, JsonConvert.SerializeObject(value), expiration);
        }

        

        public bool Exists(string key)
        {
            return Database.KeyExists(key);
        }

        public Task<bool> ExistsAsync(string key)
        {
            return Database.KeyExistsAsync(key);
        }

        public T Get<T>(string key)
        {
            var valueBytes = Database.StringGet(key);

            if (!valueBytes.HasValue)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(valueBytes);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var valueBytes = await Database.StringGetAsync(key);

            if (!valueBytes.HasValue)
            {
                return default(T);
            }

            return  JsonConvert.DeserializeObject<T>(valueBytes);
        }

        public bool Remove(string key)
        {
            return Database.KeyDelete(key);
        }

        public Task<bool> RemoveAsync(string key)
        {
            return Database.KeyDeleteAsync(key);
        }

        public bool Replace<T>(string key, T value)
        {
            return Add(key, value);
        }

        public Task<bool> ReplaceAsync<T>(string key, T value)
        {
            return AddAsync(key, value);
        }

        public bool SetAdd<T>(string key, T item) where T : class
        {
            var serializedObject = JsonConvert.SerializeObject(item);

            return Database.SetAdd(key, serializedObject);
        }

        public async Task<bool> SetAddAsync<T>(string key, T item) where T : class
        {
            var serializedObject = JsonConvert.SerializeObject(item);

            return await Database.SetAddAsync(key, serializedObject);
        }
    }
}
