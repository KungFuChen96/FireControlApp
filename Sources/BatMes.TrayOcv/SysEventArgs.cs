using System;

namespace BatMes.TrayOcv
{
    /// <summary>
    /// 通用系统通知事件数据
    /// </summary>
    public class SysEventArgs : EventArgs
    {
        /// <summary>
        /// 级别
        /// </summary>
        public BatMes.Client.Enums.SysEventLevel Level { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

    }
}
