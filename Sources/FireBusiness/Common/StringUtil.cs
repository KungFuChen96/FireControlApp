using Newtonsoft.Json;
using System.Collections;

namespace FireBusiness
{
    /// <summary>
    /// 扩展类
    /// </summary>
    static class StringUtil
    {
        /// <summary>
        /// 判断对象是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEmpty(this object obj)
        {
            if (obj == null) return true;
            if (obj.GetType() == typeof(IList))
            {
                return (obj as IList).Count == 0;
            }
            return string.IsNullOrWhiteSpace(obj.ToStringEx());
        }

        public static string ToStringEx(this object obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString().Trim();
        }

        /// <summary>
        /// 字符串转Int
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static int ToInt32(this string objValue)
        {
            var opFlag = int.TryParse(objValue, out int opValue);
            return opFlag ? opValue : default;
        }

        /// <summary>
        /// 实现深复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T source)
        {
            if (source == null)
                return default;
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        //public static void ForEach<T>(this IEnumerable<T> collection, Action<T> act)
        //{
        //    foreach (var item in collection)
        //    {
        //        act(item);
        //    }
        //}
    }
}
