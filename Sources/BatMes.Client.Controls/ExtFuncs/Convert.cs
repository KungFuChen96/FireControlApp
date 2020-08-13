using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace ExtFuncs
{
    /// <summary>
    /// 转换
    ///
    /// created at 2017-03-03 by w.j
    /// updated at 2017-03-03 by w.j
    /// </summary>
    public static class ConvertExt
    {
        #region 常用

        /// <summary>
        /// 将字符串转换为int类型，转换失败时返回defaultResult
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <param name="defaultResult">转换失败返回的默认值</param>
        /// <returns></returns>
        public static int TryParseInt(this string s, int defaultResult = 0)
        {
            int result = 0;
            if (int.TryParse(s, out result))
                return result;
            else
                return defaultResult;
        }

        /// <summary>
        /// 将字符串转换为decimal类型，转换失败时返回defaultResult
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <param name="defaultResult">转换失败返回的默认值</param>
        /// <returns></returns>
        public static decimal TryParseDecimal(this string s, decimal defaultResult = 0M)
        {
            decimal result = 0;
            if (decimal.TryParse(s, out result))
                return result;
            else
                return defaultResult;
        }

        /// <summary>
        /// 将字符串转换为long类型，转换失败时返回defaultResult
        /// </summary>
        /// <param name="s">待转换的字符串</param>
        /// <param name="defaultResult">转换失败返回的默认值</param>
        /// <returns></returns>
        public static long TryParseLong(this string s, long defaultResult = 0L)
        {
            long result = 0;
            if (long.TryParse(s, out result))
                return result;
            else
                return defaultResult;
        }

        #endregion 常用

        #region Stream

        /// <summary>
        /// Stream To byte[]
        /// </summary>
        /// <param name="s">Stream</param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream s)
        {
            Int32 length = s.Length > Int32.MaxValue ? Int32.MaxValue : System.Convert.ToInt32(s.Length);
            Byte[] buffer = new Byte[length];
            s.Read(buffer, 0, length);
            return buffer;
        }

        /// <summary>
        /// Stream To String
        /// </summary>
        /// <param name="s">Stream</param>
        /// <returns></returns>
        public static string StreamToString(Stream s)
        {
            string result = "";
            using (StreamReader sr = new StreamReader(s))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

        #endregion Stream

        #region Byte

        /// <summary>
        /// String To byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] StringToByte(string s, System.Text.Encoding encoding)
        {
            return encoding.GetBytes(s);
        }

        /// <summary>
        /// byte[] To String
        /// </summary>
        /// <param name="b"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ByteToString(byte[] b, System.Text.Encoding encoding)
        {
            return encoding.GetString(b);
        }

        #endregion Byte

        #region Dictionary

        /// <summary>
        /// 字符串转换为 Dictionary[string, string]
        /// </summary>
        /// <param name="sourceString">源字符串如 k1=v1,k2=v2,k3=v3</param>
        /// <param name="arraySplit">数组分隔符如 ,</param>
        /// <param name="keyValueSplit">键值对分隔符如 =</param>
        /// <param name="duplicateModel">重复处理（0：忽略，1：覆盖，2：合并）</param>
        /// <param name="duplicateSplit">重复合并间隔符</param>
        /// <returns></returns>
        public static Dictionary<string, string> StringToDictionary(string sourceString, char arraySplit, char keyValueSplit, int duplicateModel = 0, char duplicateSplit = ',')
        {
            if (sourceString.Length == 0)
                return null;

            Dictionary<string, string> d = new Dictionary<string, string>();

            string[] arr1 = sourceString.TrimEnd(arraySplit).Split(arraySplit);
            if (arr1 != null && arr1.Length > 0)
            {
                foreach (string s in arr1)
                {
                    string[] arr2 = s.Split(keyValueSplit);
                    if (arr2.Length == 2)
                    {
                        //新增
                        if (!d.ContainsKey(arr2[0]))
                        {
                            d.Add(arr2[0], arr2[1]);
                        }
                        //重复处理
                        else
                        {
                            switch (duplicateModel)
                            {
                                //覆盖
                                case 1:
                                    d[arr2[0]] = arr2[1];
                                    break;
                                //合并
                                case 2:
                                    d[arr2[0]] = d[arr2[0]] + duplicateSplit + arr2[1];
                                    break;
                                //忽略
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            return d;
        }

        /// <summary>
        /// Dictionary 转换为字符串
        /// </summary>
        /// <param name="dict">Dictionary 对象</param>
        /// <param name="arraySplit">数组分隔符</param>
        /// <param name="keyValueSplit">键值分隔符</param>
        /// <returns></returns>
        public static string DictionaryToString<TKey, TValue>(Dictionary<TKey, TValue> dict, string arraySplit, string keyValueSplit)
            where TKey : IConvertible
            where TValue : IConvertible
        {
            string s = "";

            if (dict != null && dict.Count > 0)
                s = string.Join(arraySplit, dict.Select(item => item.Key + keyValueSplit + item.Value));

            return s;
        }

        /// <summary>
        /// 将NameValueCollection转换为Dictionary[string, string]
        /// </summary>
        /// <remarks>当键重复时，值自动累加，</remarks>
        /// <param name="nvc">原始NameValueCollection对象</param>
        /// <param name="duplicateModel">
        /// 重复处理（
        ///     0：丢弃，如：a=1&b=2&c=3&a=4，a的值为：1
        ///     1：覆盖，如：a=1&b=2&c=3&a=4，a的值为：4
        ///     2：合并，如：a=1&b=2&c=3&a=4，a的值为：1,4
        /// ）</param>
        /// <returns></returns>
        public static Dictionary<string, string> NameValueCollectionToDictionary(NameValueCollection nvc, int duplicateModel = 0)
        {
            if (nvc == null)
                return null;

            Dictionary<string, string> d = new Dictionary<string, string>();

            if (nvc.Count > 0)
            {
                foreach (string key in nvc)
                {
                    //新增
                    if (!d.ContainsKey(key))
                    {
                        d.Add(key, nvc[key]);
                    }
                    //重复处理
                    else
                    {
                        switch (duplicateModel)
                        {
                            //覆盖
                            case 1:
                                d[key] = nvc[key];
                                break;
                            //合并
                            case 2:
                                d[key] = d[key] + "," + nvc[key];
                                break;
                            //忽略
                            default:
                                break;
                        }
                    }
                }
            }

            return d;
        }

        #endregion Dictionary
    }//end class
}