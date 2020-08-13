using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ExtFuncs
{
    public static class GeneralExt
    {
        public static Dictionary<int, T> ToDictionary<T>(this T[] data)
        {
            var dic = new Dictionary<int, T>();
            for (int i = 0; i < data.Length; i++)
            {
                dic.Add(i + 1, data[i]);
            }
            return dic;
        }

        public static int TryParse(this string s, int defaultResult = 0)
        {
            if (int.TryParse(s, out int result))
                return result;
            else
                return defaultResult;
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> act)
        {
            foreach (var item in collection)
            {
                act(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<int, T> act)
        {
            int count = collection.Count();
            for (int index = 0; index < count; index++)
            {
                act(index, collection.ElementAt(index));
            }
        }

        public static void ForEach(this IEnumerable collection, Action<object> act)
        {
            foreach (var item in collection)
            {
                act(item);
            }
        }

        public static void ForEach<T>(this IEnumerable collection, Action<object> act)
        {
            foreach (var item in collection)
            {
                if (item is T)
                {
                    act(item);
                }
            }
        }

        public static List<T> Select<T>(this IEnumerable collection, Func<T, bool> func)
        {
            List<T> list = new List<T>();
            foreach (var item in collection)
            {
                if (item is T temp)
                {
                    if (func(temp))
                        list.Add(temp);
                }
            }
            return list;
        }

        public static string Pick(this Guid guid, int size, bool isUpper)
        {
            string ret = guid.ToString().Substring(0, size);
            if (isUpper)
                ret = ret.ToUpper();
            return ret;
        }

        /// <summary>
        /// 首字符转大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UpCaseFirst(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str[0] >= 'a' && str[0] <= 'z')
                str = (char)(str[0] - 32) + str.Substring(1);
            return str;
        }

        /// <summary>
        /// 首字符转小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LowCaseFirst(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str[0] >= 'A' && str[0] <= 'Z')
                str = (char)(str[0] + 32) + str.Substring(1);
            return str;
        }

        /// <summary>
        /// 缓存类型的 PropertyInfo
        /// </summary>
        /// <param name="type">需要缓存的类型</param>
        /// <returns>返回键值对，键：PropertyInfo字符串，值：PropertyInfo</returns>
        public static Dictionary<string, PropertyInfo> StoreProperyInfo(this Type type, bool toLower)
        {
            return type.GetProperties().ToDictionary(k => toLower ? k.Name.ToLower() : k.Name);
        }

      
        public static string RemoveLast(this string str, int size)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            int length = str.Length;
            if (length < size) return string.Empty;
            str = str.Remove(length - size);
            return str;
        }
    }
}