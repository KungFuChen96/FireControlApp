using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Enums
{
    /// <summary>
    /// 消防地点
    /// </summary>
    public enum FirePost
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 分容段常温静置
        /// </summary>
        FcStandby = 1,

        /// <summary>
        /// 分容压床
        /// </summary>
        Fc = 2,

        /// <summary>
        /// 高温静置
        /// </summary>
        HotStandby = 3,

        /// <summary>
        /// 感温光纤
        /// </summary>
        ModBus = 10,
    }
}
