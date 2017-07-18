using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LuckyCode.Core.Data
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交数据
        /// </summary>
        int SaveChanges();
        /// <summary>
        /// 异步数据提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess=true, CancellationToken cancellationToken=new CancellationToken());

    }
    public interface IMainContext : IUnitOfWork
    {
        /// <summary>
        /// Create a object set for a type TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type of elements in object set</typeparam>
        /// <returns>Object set of type {TEntity}</returns>
        DbSet<TEntity> DbSet<TEntity>() where TEntity : class;
        /// <summary>
        /// 数据
        /// </summary>
        DatabaseFacade Database { get; }
        IModel Model { get; }
    }
}
