using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyCode.Core.Redis
{
    public class RedisConnection
    {
        /// <summary>
        /// 主机地址
        /// </summary>
        public  string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName { get; set; }
    }
}
