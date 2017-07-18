using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace LuckyCode.Core.FileSystem
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class LiteFileSystem:ILiteFileSystem
    {
        /// <summary>
        /// 目录分格符 windiws '\' Mac Os linux '/'
        /// </summary>
        private readonly string _separatorChar = Path.DirectorySeparatorChar.ToString();
       
        /// <summary>
        /// 服务提供
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 根路径
        /// </summary>
        private readonly string _rootPath;
        private readonly IFileProvider _fileProvider;
        /// <summary>
        /// 构造函数，注入服务提供器，文件提供器
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="fileProvider"></param>
        public LiteFileSystem(IServiceProvider serviceProvider, IFileProvider fileProvider)
        {
            _serviceProvider = serviceProvider;
            _rootPath = _serviceProvider.GetRequiredService<IHostingEnvironment>().ContentRootPath;
            _fileProvider = fileProvider;
        }
        /// <summary>
        /// 根据给出路径创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public void CreateDirectory(string path)
        {
            GetDirectoryInfo(path).Create();
        }
        /// <summary>
        /// 异步创建文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task CreateFileAsync(string path, string content)
        {
            using (var stream = CreateFile(path))
            {
                using (var tw = new StreamWriter(stream))
                {
                    await tw.WriteAsync(content);
                }
            }
        }
        /// <summary>
        /// 连接路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public string Combine(params string[] paths)
        {
            return Path.Combine(paths).Replace(_rootPath, string.Empty).Replace(Path.DirectorySeparatorChar, '/').TrimStart('/');
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Stream CreateFile(string path)
        {
            var fileInfo = _fileProvider.GetFileInfo(path);

            if (!fileInfo.Exists)
            {
                CreateDirectory(Path.GetDirectoryName(path));
            }

            return File.Create(fileInfo.PhysicalPath);
        }
        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DirectoryInfo GetDirectoryInfo(string path)
        {
            return new DirectoryInfo(Path.Combine(_rootPath, Combine(path)));
        }
        /// <summary>
        /// 是否是绝对路径
        /// windows下判断 路径是否包含 ":"
        /// Mac OS、Linux下判断 路径是否包含 "\"
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public  bool IsAbsolute(string path)
        {
            return Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }
        /// <summary>
        　　/// 获取文件绝对路径
        　　/// </summary>
        　　/// <param name="path">文件路径</param>
        　　/// <returns></returns>
        public  string MapPath(string path)
        {
            return IsAbsolute(path) ? path : Path.Combine(_rootPath, path.TrimStart('~', '/').Replace("/", _separatorChar));
        }
    }
}
