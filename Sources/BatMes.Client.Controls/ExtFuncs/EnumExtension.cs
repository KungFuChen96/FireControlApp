using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ExtFuncs
{
    public class EnumDescription : Attribute//可以使用系统的DescriptionAttribute代替
    {
        public string Text
        {
            get { return _text; }
        }

        private string _text;

        public EnumDescription(string text)
        {
            _text = text;
        }
    }

    public static class EnumExt
    {
        private static ConcurrentDictionary<Enum, string> _ConcurrentDictionary = new ConcurrentDictionary<Enum, string>();

        /// 获取枚举的描述信息(Descripion)。
        /// 支持位域，如果是位域组合值，多个按分隔符组合。
        /// </summary>
        public static string ToDescription(this Enum @this)
        {
            return _ConcurrentDictionary.GetOrAdd(@this, (key) =>
            {
                var type = key.GetType();
                var field = type.GetField(key.ToString());
                //如果field为null则应该是组合位域值，
                return field == null ? key.GetDescriptions() : GetDescription(field);
            });
        }

        /// <summary>
        /// 获取位域枚举的描述，多个按分隔符组合
        /// </summary>
        public static string GetDescriptions(this Enum @this, string separator = ",")
        {
            var names = @this.ToString().Split(',');
            string[] res = new string[names.Length];
            var type = @this.GetType();
            for (int i = 0; i < names.Length; i++)
            {
                var field = type.GetField(names[i].Trim());
                if (field == null) continue;
                res[i] = GetDescription(field);
            }
            return string.Join(separator, res);
        }

        private static string GetDescription(FieldInfo field)
        {
            var att = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return att == null ? field.Name : ((DescriptionAttribute)att).Description;
        }
    }

    public static class EnumExtension
    {
        private static object synclock = new object();

        //static Type _type;
        private static Dictionary<string, Dictionary<string, string>> _typeDesPair = new Dictionary<string, Dictionary<string, string>>();

        public static string Description(this Enum enumeration)
        {
            string fullName = enumeration.GetType().FullName;
            lock (synclock)//同步锁
            {
                if (!_typeDesPair.Keys.Contains(fullName))
                {
                    Dictionary<string, string> descrips = GetDescription(enumeration);
                    if (descrips.Count != 0)
                        _typeDesPair.Add(fullName, descrips);
                }
            }
            //if (_type == null)
            //{
            //    _type = enumeration.GetType();
            //    lock (synclock)//此时可能有多个线程在等待锁
            //    {
            //        if (_nameMemberInfoPair == null)//第一个线程处理结束后此条件已经不成立，另外的线程就不会再进入处理
            //        {
            //            _nameMemberInfoPair = GetDescription(enumeration);
            //        }
            //    }
            //}
            if (_typeDesPair.Keys.Contains(fullName))
            {
                var des = _typeDesPair[fullName];
                if (des.Keys.Contains(enumeration.ToString()))
                    return des[enumeration.ToString()];
            }
            return enumeration.ToString();
        }

        public static Dictionary<string, string> GetDescription(this Enum enumeration)
        {
            Dictionary<string, string> descriptions = new Dictionary<string, string>();
            MemberInfo[] mem = enumeration.GetType().GetFields();
            DescriptionAttribute attr = null;
            var memberInfos = mem.Skip(1).Take(mem.Length - 1);//enum的第一个是 "Int32 value__"
            foreach (var memberInfo in memberInfos)
            {
                attr = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                if (attr != null)
                    descriptions.Add(memberInfo.Name, attr.Description);
            }
            return descriptions;
            //return mem.Skip(1).Take(mem.Length - 1).ToDictionary(k => { attr = k.GetCustomAttribute<DescriptionAttribute>(); return k.Name; }, k => k.GetCustomAttribute<DescriptionAttribute>().Description);
            //GetCustomAttribute<DescriptionAttribute>()方法有可能得到的是null
        }
    }
}