using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Enums
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public enum DeviceStatus
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 温度异常
        /// </summary>
        TempAnomaly = 1,

        /// <summary>
        /// 烟雾报警
        /// </summary>
        FireAlarm = 2,

        /// <summary>
        /// 设备故障
        /// </summary>
        Fault = 3,

        /// <summary>
        /// 其他异常
        /// </summary>
        OtherEx = 4,
    }
}
