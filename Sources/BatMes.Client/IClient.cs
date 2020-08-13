using System;
using System.Collections.Generic;

namespace BatMes.Client
{
    /// <summary>
    /// BATMES客户端开发规范
    /// </summary>
    public interface IClient
    {
        #region 系统参数

        /// <summary>
        /// 所有系统参数缓存
        /// </summary>
        Dictionary<string, string> SysParaList { get; }

        /// <summary>
        /// 获取一个指定数据类型的系统参数值
        /// </summary>
        /// <typeparam name="T">指定返回的参数值类型</typeparam>
        /// <param name="paraID">参数ID</param>
        /// <returns></returns>
        T SysPara<T>(string paraID) where T : IConvertible;

        /// <summary>
        /// 更新缓存中的系统参数
        /// </summary>
        /// <param name="paraID">参数ID</param>
        /// <param name="paraVal">参数值</param>
        /// <returns></returns>
        bool SysParaEdit(string paraID, string paraVal);

        #endregion

        #region 软件服务

        /// <summary>
        /// 服务编码
        /// </summary>
        string ServiceCode { get; }

        /// <summary>
        /// 软件交付时间
        /// </summary>
        DateTime DeliveryTime { get; }

        /// <summary>
        /// 免费软件服务天数（默认3年1095天）
        /// </summary>
        int FreeDays { get; }

        /// <summary>
        /// 软件授权使用天数（0：不限）
        /// </summary>
        int AuthDays { get; }

        /// <summary>
        /// 软件版本号（主版本号.功能迭代版本号.BUG修复版本号）
        /// </summary>
        string Version { get; }

        #endregion
    }
}
