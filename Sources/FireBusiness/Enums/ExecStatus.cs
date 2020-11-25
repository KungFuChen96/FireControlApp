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

        /// <summary>
        /// 喷淋
        /// </summary>
        SprayByHand = 5,
    }

    /// <summary>
    /// 若发生火警，调度的执行状态
    /// </summary>
    public enum FireAction
    {
        /// <summary>
        /// 无动作
        /// </summary>
        Nothing = 0,

        /// <summary>
        /// 喷淋
        /// </summary>
        Spray = 1,
        
        /// <summary>
        /// 喷水
        /// </summary>
        WaterSpray = 2,

        /// <summary>
        /// 其他动作，待扩展
        /// </summary>
        Other = 3
    }
}
