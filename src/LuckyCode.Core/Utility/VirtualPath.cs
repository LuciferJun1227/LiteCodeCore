using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyCode.Core.Utility {
    /// <summary>
    /// 表示一个虚拟路径,相对于当前网站内容目录或者Web目录
    /// </summary>
    public class VirtualPath {
        public static string WebRootPath;
        public static string ContentRootPath;
        /// <summary>
        /// 是否是目录
        /// </summary>
        private readonly bool _isDirectory;
        private readonly RootPathType _rootPathType;
        /// <summary>
        /// 当前虚拟路径
        /// </summary>
        private string _virtualPath;
        private readonly string _rootPath;

        /// <summary>
        /// 构建一个虚拟路径对象
        /// </summary>
        /// <param name="virtualPath">虚拟路径</param>
        /// <param name="isDirectory">是否是目录,默认为文件路径</param>
        /// <param name="rootPathType"></param>
        public VirtualPath(string virtualPath, bool isDirectory = false, RootPathType rootPathType = RootPathType.WebRootPath) {
            if (string.IsNullOrEmpty(virtualPath)) {
                throw new ArgumentNullException(nameof(virtualPath));
            }
            _virtualPath = virtualPath;
            _isDirectory = isDirectory;
            _rootPathType = rootPathType;
            if (string.IsNullOrEmpty(WebRootPath) && string.IsNullOrEmpty(ContentRootPath)) {
                var hostingEnvironment = HttpContext.Current.RequestServices.GetRequiredService<IHostingEnvironment>();
                WebRootPath = hostingEnvironment.WebRootPath;
                ContentRootPath = hostingEnvironment.ContentRootPath;
            }
            _rootPath = _rootPathType == RootPathType.WebRootPath ? WebRootPath : ContentRootPath;

        }
        /// <summary>
        /// 创建一个类型为目录的对象,起始路径为根节点
        /// </summary>
        /// <param name="rootPathType"></param>
        public VirtualPath(RootPathType rootPathType = RootPathType.WebRootPath) : this("/", true, rootPathType) {

        }
        public bool IsDirectory => _isDirectory;
        /// <summary>
        /// 判断当前路径(文件夹或文件)是否存在
        /// </summary>
        public bool Exists {
            get {
                try {
                    if (_isDirectory) {
                        return Directory.Exists(ToAbsolute());
                    }
                    return File.Exists(ToAbsolute());
                } catch {
                    return false;
                }
            }
        }
        /// <summary>
        /// 当前文件名(仅在文件虚拟路径时可用)
        /// </summary>
        public string FileName {
            get {
                if (!_isDirectory) {
                    return Path.GetFileName(_virtualPath);
                }
                throw new NotSupportedException("当前是路径是目录,无法获取文件名");
            }
        }
        /// <summary>
        /// 获取文件扩展名,扩展名包含点
        /// </summary>
        public string Extension {
            get {
                if (!_isDirectory) {
                    return Path.GetExtension(_virtualPath);
                }
                throw new NotSupportedException("当前是路径是目录,无法获取文件扩展名");
            }
        }
        /// <summary>
        /// 获取当前虚拟目录
        /// </summary>
        public VirtualPath CurrentDirectory {
            get {
                if (_isDirectory) {
                    return this;
                }
                return new VirtualPath(Path.GetDirectoryName(ToString()), true);
            }
        }
        /// <summary>
        /// 创建当前路径目录
        /// </summary>
        public void CreateDirectory() {
            string path;
            if (_isDirectory) {
                path = ToAbsolute();
            } else {
                path = System.IO.Path.GetDirectoryName(ToAbsolute());
            }
            if (path != null && !Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// 删除当前目录或文件
        /// </summary>
        public void Delete(bool recursive = true) {
            if (Exists) {
                if (_isDirectory) {
                    Directory.Delete(ToAbsolute(), recursive);
                } else {
                    File.Delete(ToAbsolute());
                }
            }
        }
        /// <summary>
        /// 添加目录,仅在目录类型下可以使用
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="newInstance">是否创建新的实例,默认为false</param>
        /// <returns></returns>
        public VirtualPath Append(string folder, bool newInstance = false) {
            if (!_isDirectory) {
                throw new NotSupportedException("当前是文件路径,不支持在次构造路径");
            }
            if (newInstance) {
                return new VirtualPath(Combine(_virtualPath, folder), true, _rootPathType);
            }
            _virtualPath = Combine(_virtualPath, folder);
            return this;
        }
        /// <summary>
        /// 获取当前目录下虚拟路径下指定文件的虚拟路径,如果当前如果是文件,则获取当前目录下所在的文件
        /// </summary>
        /// <param name="pathName">文件名</param>
        /// <param name="isDirectory"></param>
        /// <returns></returns>
        public VirtualPath GetVirtualPath(string pathName, bool isDirectory = false) {
            if (!_isDirectory) {
                var directoryName = Path.GetDirectoryName(ToString());
                return new VirtualPath(Combine(directoryName, pathName), isDirectory, _rootPathType);
            }
            return new VirtualPath($"{_virtualPath}{Path.DirectorySeparatorChar}{pathName}", isDirectory,
                _rootPathType);
        }
        /// <summary>
        /// 获取当前目录下指定文件的绝对路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>指定文件的</returns>
        public string GetAbsolutePath(string fileName) {
            return GetVirtualPath(fileName).ToAbsolute();
        }
        /// <summary>
        ///  转换为绝对路径
        /// </summary>
        /// <returns></returns>
        public string ToAbsolute() {
            return GetAbsolute(_virtualPath);
        }
        /// <summary>
        /// 获取指定格式的虚拟路径
        /// </summary>
        /// <returns></returns>
        public string GetFormatedVirtualPath(DirectorySeparatorType separatorType = DirectorySeparatorType.LocalPath) {
            if (separatorType == DirectorySeparatorType.LocalPath) {
                _virtualPath = _virtualPath
                    .Replace("\\", Path.DirectorySeparatorChar.ToString())
                    .Replace("/", Path.DirectorySeparatorChar.ToString());
            } else {
                _virtualPath = _virtualPath.Replace("\\", "/");
            }
            return _virtualPath;
        }
        /// <summary>
        /// 获取当前路径的虚拟路径
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return GetFormatedVirtualPath(DirectorySeparatorType.WebUrl);
        }
        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public string GetAbsolute(string virtualPath) {
            return Combine(_rootPath, virtualPath.TrimStart('~', '/', '\\'));
        }
        /// <summary>
        /// 合并路径,(系统自带的合并会将第一'/'认为是根目录,会忽略之前的路径,大部分情况下不方便,另外路径的斜杠方向不一致影响心情)
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static string Combine(params string[] parts) {
            if (parts == null) {
                return string.Empty;
            }
            var path = string.Join("/", parts.Where(t => !string.IsNullOrEmpty(t)).
                Select(t => t.Trim().TrimEnd('\\', '/')));
            return path.Replace("\\", Path.DirectorySeparatorChar.ToString())
                .Replace("//", Path.DirectorySeparatorChar.ToString());
        }

        public VirtualPath Clone() {
            return new VirtualPath(_virtualPath, _isDirectory, _rootPathType);
        }
    }
    /// <summary>
    ///     表示一个虚拟路径
    /// </summary>
    public enum RootPathType {
        WebRootPath,
        ContentRootPath
    }

    public enum DirectorySeparatorType {
        LocalPath,
        WebUrl
    }
    public static class VirtualPathExtensions {
        public static List<string> SerachFiles(this VirtualPath directory, string[] serachPatterns, SearchOption searchOption = SearchOption.AllDirectories) {
            if (directory == null) {
                throw new ArgumentNullException(nameof(directory));
            }
            if (!directory.IsDirectory) {
                throw new NotSupportedException("当前路径无效,必须是目录模式");
            }
            if (serachPatterns == null) {
                throw new ArgumentNullException(nameof(serachPatterns));
            }
            var cachedFiles = new List<string>();
            //初次检索
            foreach (var pattern in serachPatterns) {
                var files = Directory.EnumerateFiles(directory.ToAbsolute(), pattern, searchOption);
                cachedFiles.AddRange(files);
            }

            return cachedFiles.Distinct().ToList();
        }
        public static void SerachAndCopyTo(this VirtualPath virtualPath, string[] serachPatterns, VirtualPath otherDirectory, SearchOption searchOption = SearchOption.AllDirectories) {
            if (virtualPath == null) {
                throw new ArgumentNullException(nameof(virtualPath));
            }
            if (otherDirectory == null) {
                throw new ArgumentNullException(nameof(otherDirectory));
            }
            if (!otherDirectory.IsDirectory) {
                throw new NotSupportedException("目标是路径无效,必须是目录模式");
            }

            var files = virtualPath.SerachFiles(serachPatterns, searchOption);
            foreach (var fullName in files) {
                var fileName = fullName.Replace(virtualPath.ToAbsolute(), String.Empty);
                var otherFileName = otherDirectory.GetVirtualPath(fileName);
                otherFileName.CreateDirectory();
                File.Copy(fullName, otherDirectory.GetAbsolutePath(fileName), true);
            }
        }

    }
}
