/*
 
 {
    "RedisConfig": {
        "DefaultConnection": {
            "Host": "127.0.0.1",
            "HostName": "Redis1: ",
            "Port":6379
        }
    }
}
 
 */


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace LuckyCode.Core.Redis
{
    public class RedisClientManager 
    {
        private RedisConnection _config;
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;
        public RedisClientManager(IOptions<RedisConnection> config)
        {
            _config = config.Value;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }
        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect()
        {
            var redisInstanceName = _config.HostName;
            var connStr =_config.Host+":"+_config.Port;
            return _connections.GetOrAdd(redisInstanceName, p => ConnectionMultiplexer.Connect(connStr));
        }
        
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            
            return GetConnect().GetDatabase();
        }

        public IServer GetServer()
        {
            return GetConnect().GetServer(_config.Host,_config.Port);
        }

        public ISubscriber GetSubscriber(string configName = null)
        {
            
            return GetConnect().GetSubscriber();
        }

        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
    }
    
}
