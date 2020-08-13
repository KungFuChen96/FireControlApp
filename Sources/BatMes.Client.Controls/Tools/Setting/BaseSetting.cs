using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tools
{
    public class BaseSettings
    {
        readonly Dictionary<string, PropertyInfo> Properties = new Dictionary<string, PropertyInfo>();
        public BaseSettings()
        {
            PropertyInfo[] infos = this.GetType().GetProperties();

            foreach (var item in infos)
            {
                if (item.GetIndexParameters().Length > 0) continue;
                Properties.Add(item.Name, item);
            }
        }

        /// <summary>
        /// 根据字符串获取或设置属性值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual dynamic this[string propertyName]
        {
            get { return Properties[propertyName].GetValue(this); }
            set { Properties[propertyName].SetValue(this, value); }
        }

        /// <summary>
        /// 根据字符串获取属性类型
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="flag">为了重载上面的索引器，string类型是因为可以传null</param>
        /// <returns></returns>
        public virtual Type this[string propertyName, string flag]
        {
            get { return Properties[propertyName].PropertyType; }
        }
    }
}
