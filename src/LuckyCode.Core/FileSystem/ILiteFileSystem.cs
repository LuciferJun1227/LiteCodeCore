using System.Threading.Tasks;

namespace LuckyCode.Core.FileSystem
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public interface ILiteFileSystem
    {
        /// <summary>
        /// 创建文件目录
        /// </summary>
        /// <param name="path"></param>
        void CreateDirectory(string path);
        /// <summary>
        /// 异步创建文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task CreateFileAsync(string path, string content);
    }
}
