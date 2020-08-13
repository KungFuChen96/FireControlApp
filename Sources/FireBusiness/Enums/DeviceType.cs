using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Enums
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// 所有硬件都生效
        /// </summary>
        ALLHardware = 0,

        /// <summary>
        /// plc
        /// </summary>
        PLC = 1,

        /// <summary>
        /// ModBus
        /// </summary>
        ModBus = 2,

        /// <summary>
        /// 其他类型设备
        /// </summary>
        Other = 7
    }

    /// <summary>
    /// 火警类型
    /// </summary>
    public enum FireType
    {
        /// <summary>
        /// 烟雾报警
        /// </summary>
        Smoke = 1,

        /// <summary>
        /// 温度报警
        /// </summary>
        FireTemp = 2
    }

    /// <summary>
    /// 设备连接状态
    /// </summary>
    public class ConnetStatus
    {
        /// <summary>
        /// 是否连接成功
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 失败消息
        /// </summary>
        public string Message { get; set; }
    }
}
