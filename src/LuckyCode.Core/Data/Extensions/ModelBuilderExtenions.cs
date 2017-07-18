using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LuckyCode.Core.Data.Extensions
{
    /// <summary>
    /// 实体映射配置接口
    /// </summary>
    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder b);
    }
    /// <summary>
    /// 泛型实体映射配置接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
    /// <summary>
    /// 实体映射抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityMappingConfiguration<T> : IEntityMappingConfiguration<T> where T : class
    {
        /// <summary>
        /// 定义子类必须实现的映射方法
        /// </summary>
        /// <param name="b"></param>
        public abstract void Map(EntityTypeBuilder<T> b);

        public void Map(ModelBuilder b)
        {
            Map(b.Entity<T>());
        }
    }
    /// <summary>
    /// 对实体映射扩展
    /// </summary>
    public static class ModelBuilderExtenions
    {
        /// <summary>
        /// 对Assembly扩展，获取映射类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="mappingInterface">映射的接口类型</param>
        /// <returns></returns>
        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
        {
            //查找程序集中，非抽象类型，且类型接口中存在（泛型接口且可以用给出的mappingInterface作为泛型类型进行构造）的类型
            return assembly.GetTypes().Where(x => !x.GetTypeInfo().IsAbstract && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
        }
        /// <summary>
        /// 自动配置程序集映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assembly"></param>
        public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            //取出可映射的类型信息
            var mappingTypes = assembly.GetMappingTypes(typeof(IEntityMappingConfiguration<>));
            //遍历类型集合，创建类型实例并强制转换为IEntityMappingConfiguration，调用配置映射方法
            foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityMappingConfiguration>())
            {
                config.Map(modelBuilder);
            }
        }
    }
}
