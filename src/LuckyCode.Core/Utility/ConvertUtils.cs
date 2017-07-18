using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace LuckyCode.Core.Utility {
    /// <summary>
    ///     数据类型转换工具类
    /// </summary>
    public static class ConvertUtils {
        /// <summary>
        ///     转为字符串
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public static string To(object value) {
            return To(value, string.Empty);
        }

        /// <summary>
        ///     数据类型转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">源数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>结果</returns>
        public static T To<T>(object value, T defaultValue = default(T)) {
            var obj = default(T);
            try {
                if (value == null) {
                    return defaultValue;
                }
                var valueType = value.GetType();
//                var valueTypeInfo = valueType.GetTypeInfo();
                var targetType = typeof(T);
                var targetTypeInfo = targetType.GetTypeInfo();
                tag1:
                if (valueType == targetType) {
                    return (T)value;
                }
                if (targetTypeInfo.IsEnum) {
                    if (value is string) {
                        return (T)Enum.Parse(targetType, value as string);
                    }
                    return (T)Enum.ToObject(targetType, value);
                }
                if (targetType == typeof(Guid)) {
                    object obj1 = Guid.Parse(value.ToString());
                    return (T)obj1;
                }
                if (targetType == typeof(DateTime) && (value is string /*|| value is StringValues*/)) {
                    DateTime d1;
                    if (DateTime.TryParse((string)value, out d1)) {
                        object obj1 = d1;
                        return (T)obj1;
                    }
                }

                if (targetTypeInfo.IsGenericType) {
                    if (targetType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                        targetType = Nullable.GetUnderlyingType(targetType);
                        goto tag1;
                    }
                }
                if (value is IConvertible) {
                    obj = (T)Convert.ChangeType(value, targetType);
                }
                if (obj == null) {
                    obj = defaultValue;
                }

            } catch {
                obj = defaultValue;
            }
            return obj;
        }

        public static object ToObject(string value, Type targetType, object defaultValue = null) {
            object obj = null;
            try {
                if (value == null) {
                    return defaultValue;
                }
                if (targetType == null) {
                    return defaultValue;
                }
                
                var valueType = value.GetType();
                var targetTypeInfo = targetType.GetTypeInfo();
                Label:
                if (valueType == targetType) {
                    return value;
                }
                if (targetTypeInfo.IsEnum) {
                    return Enum.Parse(targetType, value);

                }
                if (targetType == typeof(Guid)) {
                    return Guid.Parse(value);
                }
                if (targetType == typeof(DateTime)) {
                    DateTime d1;
                    if (DateTime.TryParse(value, out d1)) {
                        return d1;
                    }
                }
                if (targetTypeInfo.IsGenericType) {
                    if (targetType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                        targetType = Nullable.GetUnderlyingType(targetType);
                        goto Label;
                    }
                }
                obj = Convert.ChangeType(value, targetType);
                if (obj == null) {
                    obj = defaultValue;
                }
                if (obj == null && (targetType == typeof(int) || targetType == typeof(float))) {
                    obj = 0;
                }
            } catch {
                obj = defaultValue;
            }
            return obj;
        }
        /// <summary>
        /// 将对象转换Js字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Js字符串</returns>
        public static string ToJs(object item) {
            if (item == null) {
                return string.Empty;
            }
            object result = item;
            if (item is Enum) {
                // ReSharper disable once PossibleInvalidCastException
                result = ((int)item).ToString();
            }
            var json = JsonConvert.SerializeObject(result);

            json = json.Replace("'", "\\'");

            json = json.Replace("\"", "'");

            return json;
        }
        /// <summary>
        /// 转换为js 脚本可以使用的 Date
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToJsDate(DateTime d) {
            var dateConstructor = new StringBuilder();
            dateConstructor.Append("new Date(");
            dateConstructor.Append(d.Year);
            dateConstructor.Append(",");
            dateConstructor.Append(d.Month - 1); // In JavaScript, months are zero-delimited
            dateConstructor.Append(",");
            dateConstructor.Append(d.Day);
            dateConstructor.Append(",");
            dateConstructor.Append(d.Hour);
            dateConstructor.Append(",");
            dateConstructor.Append(d.Minute);
            dateConstructor.Append(",");
            dateConstructor.Append(d.Second);
            if (d.Millisecond > 0) {
                dateConstructor.Append(",");
                dateConstructor.Append(d.Millisecond);
            }
            dateConstructor.Append(")");
            return dateConstructor.ToString();
        }

        public static string ToJsonString(object obj) {
            return JsonConvert.SerializeObject(obj);
        }

        public static T GetJson<T>(string jsonString) {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
   
        public static string ToSecret(string value, char mask = '*', int startCount = 0, int endCount = 0) {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }
            if (value.Length < startCount || value.Length < endCount) {
                return value;
            }
            var startPart = value.Substring(0, startCount);

            var centerPart = value.Substring(startCount, value.Length - endCount);

            var endPart = value.Substring(value.Length - endCount);

            var maskCount = centerPart.Length;

            if (maskCount > 10) {
                maskCount = 10;
            }
            return $"{startPart}{string.Empty.PadLeft(maskCount, mask)}{endPart}";

        }
    }
}