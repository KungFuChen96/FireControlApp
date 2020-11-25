using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Enums
{
    /// <summary>
    /// PLC信号类型
    /// </summary>
    public enum SignalType
    {
        #region 分容

        #region 写
        /// <summary>
        /// 放盘完成
        /// </summary>
        W_InFinish = 1,

        /// <summary>
        /// 入盘规格
        /// </summary>
        W_InType = 2,

        /// <summary>
        /// 故障复位
        /// </summary>
        W_FalutReset = 3,

        /// <summary>
        /// 出盘完成
        /// </summary>
        W_OutFinish = 4,

        /// <summary>
        /// 确认开启消防
        /// </summary>
        W_ConfirmFire = 5,
        #endregion

        #region 批量读
        /// <summary>
        /// 压床请求放盘信号
        /// </summary>
        R_RequestPress = 6,

        /// <summary>
        /// 正常运行信号
        /// </summary>
        R_RunningFlag = 7,

        /// <summary>
        /// 故障代码
        /// </summary>
        R_FaultFlag = 8,

        /// <summary>
        /// 请求出盘
        /// </summary>
        R_RequestOut = 9,

        /// <summary>
        /// 请求打开消防
        /// </summary>
        R_OpenFire = 10,
        #endregion

        #region 单个读
        /// <summary>
        /// 压床请求放盘信号
        /// </summary>
        OR_RequestPress = 11,

        /// <summary>
        /// 正常运行信号
        /// </summary>
        OR_RunningFlag = 12,

        /// <summary>
        /// 故障代码
        /// </summary>
        OR_FaultFlag = 13,

        /// <summary>
        /// 请求出盘
        /// </summary>
        OR_RequestOut = 14,

        /// <summary>
        /// 请求打开消防
        /// </summary>
        OR_OpenFire = 15,
        #endregion

        #region 烟雾报警
        /// <summary>
        /// 有烟雾报警
        /// </summary>
        R_HasSmoke = 16,
        #endregion

        #endregion

        #region 常温静置
        /// <summary>
        /// 批量读常温静置
        /// </summary>
        R_FcStandby = 17,
        #endregion

        #region 分容压床
        /// <summary>
        /// 分容烟雾判断
        /// </summary>
        R_Fc = 18,

        /// <summary>
        /// 分容温度判断
        /// </summary>
        R_Fc_Temp = 19,
        #endregion

        #region 高温静置
        /// <summary>
        /// 批量读常温静置烟雾
        /// </summary>
        R_HotStandby = 20,
        #endregion

        #region Modbus 感温光纤
        /// <summary>
        /// 批量读取modbus温度
        /// </summary>
        R_Modbus = 21,
        #endregion

        #region 心跳
        /// <summary>
        /// 常温静置心跳
        /// </summary>
        W_FcStandbyBit = 22,

        /// <summary>
        /// 高温静置心跳
        /// </summary>
        W_HotBit = 23,
        #endregion

        #region 喇叭
        /// <summary>
        /// 温度异常、烟雾报警则喇叭响起
        /// </summary>
        W_Speaker = 24,
        #endregion

        #region 定制化需求
        /// <summary>
        /// 通知电压要喷淋，电源会做一系列的动作，如断电
        /// </summary>
        W_NotifyFcSpray = 25,

        /// <summary>
        /// 弹开压床
        /// </summary>
        W_DoBrakeUp = 26,

        /// <summary>
        /// 感温光纤通道1(常温静置架1：由出入盘口的那个)
        /// </summary>
        R_Modbus1 = 27,

        /// <summary>
        /// 感温光纤通道2
        /// </summary>
        R_Modbus2 = 28,

        /// <summary>
        /// 感温光纤通道3（高温静置架1：靠近分容压床的那列）
        /// </summary>
        R_Modbus3 = 29,

        /// <summary>
        /// 感温光纤通道2
        /// </summary>
        R_Modbus4 = 30,
        #endregion
    }
}
