using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;

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

        /// <summary>
        /// 获取异常规范化信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionMsg(this Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            if (ex != null)
            {
                sb.Append(" 【异常类型】：" + ex.GetType().Name);
                sb.Append(" 【异常信息】：" + ex.Message);
                sb.Append(" 【堆栈调用】：" + ex.StackTrace);
                sb.Append(" 【异常方法】：" + ex.TargetSite);
            }
            return sb.ToString();
        }
    }
}
