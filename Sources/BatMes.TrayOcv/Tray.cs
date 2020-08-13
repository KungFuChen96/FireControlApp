using System.Collections.Generic;

namespace BatMes.TrayOcv
{
    /// <summary>
    /// 托盘
    /// </summary>
    public class Tray
    {
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 电池条码
        /// </summary>
        public Dictionary<int, string> Batteries { get; set; }
    }
}
