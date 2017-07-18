using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LuckyCode.Core.Utility.Extensions {

    public static class ObjectExtensions {
        /// <summary>
        /// 根据对象属性名称复制值到指定目标对象,或目标对象实例
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="source">源</param>
        /// <param name="instance">默认目标对象实例</param>
        /// <param name="enableIgnore"></param>
        /// <param name="cloneDeep"></param>
        /// <returns>目标对象</returns>
        public static T CloneTo<T>(this object source, T instance = default(T), bool enableIgnore = true, int cloneDeep = 2) where T : class, new() {
            if (source == null) {
                return null;
            }
            if (instance == default(T)) {
                instance = new T();
            }
            return (T)source.CloneToObject(instance, enableIgnore, cloneDeep);
        }

        public static object CloneToObject(this object source, object target, bool enableIgnore, int cloneDeep = 2) {
            return source.CloneToObject(target, enableIgnore, cloneDeep, 1);
        }

        private static object CloneToObject(this object source, object target, bool enableIgnore, int cloneDeep, int currdeep) {
            if (currdeep > cloneDeep) {
                return null;
            }
            if (source == null) {
                return null;
            }
            var sourceType = source.GetType();
            var sourceProperties = sourceType.GetProperties();
            var targetProperties = target.GetType().GetProperties(BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance);

            foreach (var property in targetProperties) {
                if (!property.CanWrite)
                    continue;
                var sourceProperty = sourceProperties.FirstOrDefault(t => t.Name == property.Name);
                if (sourceProperty == null || !sourceProperty.CanRead)
                    continue;
                if (enableIgnore) {
                    //忽略克隆
                    if (sourceProperty.GetCustomAttributes(true).OfType<IgnoreCloneAttribute>().Any()) {
                        continue;
                    }
                }
                var val = sourceProperty.GetValue(source);
                if (val != null) {
                    var type = val.GetType();
                    var typeInfo = type.GetTypeInfo();
                    if (typeInfo.IsValueType ||
                        typeInfo.IsEnum ||
                        type == typeof(string) ||
                        type.FullName == "System.RuntimeType" ||
                        val is IEnumerable) {
                        property.SetValue(target, val, null);
                    } else {
                        var obj = property.GetValue(target);
                        if (obj == null) {
                            obj = Activator.CreateInstance(property.PropertyType);
                            property.SetValue(target, obj);
                        }
                        val.CloneToObject(obj, enableIgnore, cloneDeep, ++currdeep);
                    }
                } else {
                    property.SetValue(target, null);
                }
            }

            return target;
        }
        public static IDictionary<string, object> ToDictionary(this object @object) {
            var dictionary = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            if (@object != null) {
                var enumerator = TypeDescriptor.GetProperties(@object).GetEnumerator();
                try {
                    while (enumerator.MoveNext()) {
                        var current = (PropertyDescriptor)enumerator.Current;
                        dictionary.Add(current.Name, current.GetValue(@object));
                    }
                } finally {
                    var disposable = enumerator as IDisposable;
                    disposable?.Dispose();
                }
            }
            return dictionary;
        }
    }
    public class IgnoreCloneAttribute : Attribute {
    }
}