using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace LuckyCode.Core.Service
{
    /// <summary>
    /// 文件依赖缓存过期
    /// </summary>
    public class FileCacheDependency
    {
        public FileCacheDependency(string filename)
        {
            FileName = filename;
        }

        public string FileName { get; }
    }

    /// <summary>
    /// 对内存缓存扩展方法，对文件，时间 依赖缓存过期策略
    /// </summary>
    public static class MemoryCacheExtensions
    {
        public static void Set<TItem>(this IMemoryCache cache, string key, TItem value, FileCacheDependency dependency)
        {
            var fileInfo = new FileInfo(dependency.FileName);
            var fileProvider = new PhysicalFileProvider(fileInfo.DirectoryName);
            cache.Set(key, value, new MemoryCacheEntryOptions()
                .AddExpirationToken(fileProvider.Watch(fileInfo.Name)));

        }
        public static void Set<TItem>(this IMemoryCache cache, string key, TItem value, TimeCacheDependency dependency)
        {
            var options = new MemoryCacheEntryOptions();

            if (dependency.Policy == CacheItemPolicy.AbsoluteExpiration)
            {
                options.SetAbsoluteExpiration(dependency.Time);
            }
            else
            {
                options.SetSlidingExpiration(dependency.Time);
            }
            cache.Set(key, value, options);
        }
    }

    public enum CacheItemPolicy
    {
        AbsoluteExpiration,
        SlidingExpiration
    }

    /// <summary>
    /// 时间依赖缓存过期
    /// </summary>
    public class TimeCacheDependency
    {
        public TimeCacheDependency(TimeSpan time, CacheItemPolicy policy = CacheItemPolicy.AbsoluteExpiration)
        {
            Time = time;
        }

        public TimeSpan Time { get; }

        public CacheItemPolicy Policy { get; }
    }
}
