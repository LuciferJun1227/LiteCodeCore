using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LuckyCode.Core.Data.Extensions
{
    /// <summary>
    /// 扩展通过主键查询实体
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        /// 扩展方法，按主键查找实体
        /// </summary>
        /// <typeparam name="TEntity">实体模型类型</typeparam>
        /// <param name="set"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public static TEntity Find<TEntity>(this DbSet<TEntity> set, params object[] keyValues) where TEntity : class
        {
            //通过SET找出当前表数据库上下文
            var context = set.GetService<DbContext>();
            //取出实体的类型信息
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            //取出实体主键信息
            var key = entityType.FindPrimaryKey();
            //改变追踪状态
            var entries = context.ChangeTracker.Entries<TEntity>();

            var i = 0;
            //根据主键集合信息建立查询
            foreach (var property in key.Properties)
            {
                var i1 = i;
                entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValues[i1]);
                i++;
            }
            //取出符合条件的数据
            var entry = entries.FirstOrDefault();
            if (entry != null)
            {
                // Return the local object if it exists.
                return entry.Entity;
            }
            //建立lambda表达式树
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = set.AsQueryable();
            i = 0;
            foreach (var property in key.Properties)
            {
                var i1 = i;
                query = query.Where((Expression<Func<TEntity, bool>>)
                 Expression.Lambda(
                     Expression.Equal(
                         Expression.Property(parameter, property.Name),
                         Expression.Constant(keyValues[i1])),
                     parameter));
                i++;
            }

            // Look in the database
            return query.FirstOrDefault();
        }
    }
}
