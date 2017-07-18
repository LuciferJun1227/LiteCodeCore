using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LuckyCode.Core.Data
{
    /// <summary>
    /// 数据提供基类
    /// </summary>
    public class MainContext :DbContext, IMainContext
    {
        public MainContext(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public MainContext(DbContextOptions<DbContext> options) :base(options)
        {
            
        }

        public DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public IConfigurationRoot Configuration { get; }
    }
}
