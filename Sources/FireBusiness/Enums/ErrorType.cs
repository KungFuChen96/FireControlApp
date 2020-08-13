using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Enums
{
    /// <summary>
    /// 告警类型
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// 设备正常
        /// </summary>
        Ok = 0,

        /// <summary>
        /// 设备故障停机
        /// </summary>
        FalutStop = 1,

        /// <summary>
        /// 压床处于手动状态
        /// </summary>
        Byhand = 2,

        /// <summary>
        /// 压床急停中
        /// </summary>
        Scram = 3,

        /// <summary>
        /// 压床料盘反向放置
        /// </summary>
        Reverse = 4,

        /// <summary>
        /// 阀门故障
        /// </summary>
        ValveFailure = 5,

        /// <summary>
        /// 电源工步执行失败
        /// </summary>
        ExcFailure = 6,

        /// <summary>
        /// 压床火警报警
        /// </summary>
        FireAlarm = 7,

        /// <summary>
        /// 门YV1阀开关不到位故障
        /// </summary>
        YV1 = 100,

        /// <summary>
        /// 模组YV2阀开关不到位故障
        /// </summary>
        YV2 = 101,

        /// <summary>
        /// 定位YV3阀开关不到位故障
        /// </summary>
        YV3 = 102,

        /// <summary>
        /// 探针YV4阀开关不到位故障
        /// </summary>
        YV4 = 103,

        /// <summary>
        /// 压床风扇故障
        /// </summary>
        PreeWindFault = 104,

        /// <summary>
        /// 风道提速风扇故障
        /// </summary>
        SpeedFault = 105,

        /// <summary>
        /// 风道风扇故障
        /// </summary>
        WindLevelFault = 106
    }
}
