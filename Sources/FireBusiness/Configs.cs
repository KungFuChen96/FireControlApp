using BatMes.Client;
using FireBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FireBusiness
{
    public class Configs
    {
        #region PLC

        /// <summary>
        /// 请求长度
        /// </summary>
        public static int ResquestLen => 30;

        /// <summary>
        /// 转折点
        /// </summary>
        public static int TurningPoint => 16;

        /// <summary>
        /// 请求压合开始信号点  PLC TO 调度信号
        /// </summary>
        public static string RequestPressingStart => "R7000";

        /// <summary>
        /// 放盘完成开始信号 调度信号 TO PLC 
        /// </summary>
        public static string InFinishStart => "R706";

        /// <summary>
        /// 程序正常运行信号 PLC TO 调度信号
        /// </summary>
        public static string RunningFlagStart => "R7180";

        /// <summary>
        /// 故障开始信号点DT1000 PLC TO 调度信号
        /// </summary>
        public static string FaultFlagStart => "D10000";

        /// <summary>
        /// 入盘规格 调度信号 TO PLC 
        /// </summary>
        public static string InType => "D10050";

        /// <summary>
        /// 单层故障复位
        /// </summary>
        public static string ResetStart => "R730";

        /// <summary>
        /// 请求取盘信号
        /// </summary>
        public static string ResquestOut => "R7030";

        /// <summary>
        /// 出盘完成信号
        /// </summary>
        public static string OutFinishStart => "R709";

        /// <summary>
        /// 火警消防启动允许 PLC TO 调度信号
        /// </summary>
        public static string OpenFireFlag => "R7240";

        /// <summary>
        /// 火警消防允许确认 调度信号 TO PLC 
        /// </summary>
        public static string ConfirmFireFlag => "R727";
        #endregion

        #region Quartz变量
        /// <summary>
        /// quartz任务名称
        /// </summary>
        public static string ParamName => "QuartzObjName";

        /// <summary>
        /// 执行消防任务名称
        /// </summary>
        public static string FireJob => "QuartzFireJob";

        /// <summary>
        /// 分容静置任务名称
        /// </summary>
        public static string FcStandyJob => "QuartzFcStandyJob";

        /// <summary>
        /// 分容任务名称
        /// </summary>
        public static string FcJob => "QuartzFcJob";

        /// <summary>
        /// 高温静置任务名称
        /// </summary>
        public static string HotStandyJob => "QuartzHotStandyJob";

        #endregion

        #region Quartz时间
        /// <summary>
        /// 加入队列后，隔多久才开始(单位：秒)
        /// </summary>
        public static int StartTimeSeconds => 1;

        /// <summary>
        /// 加入队列后，每隔多久执行（单位：分钟）
        /// </summary>
        public static int IntervalInMinutes => 1;

        /// <summary>
        /// 每隔多少秒执行队列任务
        /// </summary>
        public static int IntervalInseconds => 3;

        /// <summary>
        /// 每隔多少毫秒执行任务
        /// </summary>
        public static int IntervalInMm => 100;
        #endregion quartz时间

        #region 状态

        #endregion

        #region 消防
        /// <summary>
        /// 烟雾感应起始地址，共28个库位
        /// </summary>
        public static string SmokeStartAt => "R3015";

        /// <summary>
        /// 总共有多少个烟雾感应库位
        /// </summary>
        public static int SmokeCount => 28;

        /// <summary>
        /// 喷淋起始地址
        /// </summary>
        public static string DoSpray => "D1020";

        /// <summary>
        /// 通知压床断电起始地址
        /// </summary>
        public static string DoBlackout => "R734";

        /// <summary>
        /// 喷淋地址长度: 分容
        /// </summary>
        public static int SprayCount => 7;

        /// <summary>
        /// 喷淋地址长度映射，适用用静置 KEY: (位置，行) VALUE:(列， 层, 起始地址)
        /// </summary>
        public static Dictionary<(FirePost, int), (int, int, string)> SprayLenMap = new Dictionary<(FirePost, int), (int, int, string)> 
        {
            { (FirePost.FcStandby, 1), (9, 7, "D10101")},
            { (FirePost.FcStandby, 2), (36, 7, "D10001")},
            { (FirePost.HotStandby, 1), (21, 6, "D10101")},
            { (FirePost.HotStandby, 2), (21, 6, "D10001")}
        };

        /// <summary>
        /// 烟雾感应地址长度映射，适用用静置 KEY: (位置，行) VALUE:(列， 层, 起始地址)
        /// </summary>
        public static Dictionary<(FirePost, int), (int, int, string)> SmokeLenMap = new Dictionary<(FirePost, int), (int, int, string)>
        {
            { (FirePost.FcStandby, 1), (9, 7, "R5010")},
            { (FirePost.FcStandby, 2), (36, 7, "R4010")},
            { (FirePost.HotStandby, 1), (21, 6, "R5010")},
            { (FirePost.HotStandby, 2), (21, 6, "R4010")},
            { (FirePost.Fc, 1), (28, 12, "D20101")},
        };

        /// <summary>
        /// 信号点是否存在一对多
        /// </summary>
        public static Dictionary<(FirePost, SignalType), bool> SignHasMuti = new Dictionary<(FirePost, SignalType), bool>
        {
            { (FirePost.HotStandby, SignalType.R_HotStandby), true},
            { (FirePost.Fc, SignalType.R_Fc), true}
        };

        /// <summary>
        /// 分容喷淋信号点和压床信号点顺序是否相同
        /// </summary>
        public static bool FcSpraryIsSanme => ConfigurationManager.AppSettings["FcSpraryIsSanme"].To<bool>();
        #endregion

        #region ModBus
        /// <summary>
        /// 从站字节
        /// </summary>
        public static byte SlaveAddR => Business.Instance.SysPara<byte>("SlaveAddress");

        /// <summary>
        /// 读取地址
        /// </summary>
        public static string AddrByModBus => Business.Instance.SysPara<string>("AddrByModBus");

        /// <summary>
        /// 总共有多少个
        /// </summary>
        public static int AddrCountByBus => 30;

        /// <summary>
        /// Modbus读取范围
        /// </summary>
        public static int[] ModbusRange = new int[] { (int)FirePost.FcStandby, (int)FirePost.HotStandby };

        /// <summary>
        /// 感温光纤有个特殊的值为38221，其对应为-273.15的float的值，这是没有意义的值。									
        /// </summary>
        public static ushort[] SpecialVals = new ushort[] { 38222, 38221 };

        /// <summary>
        /// 感温光纤有报警是否要打开喇叭
        /// </summary>
        public static bool OpenSpeakerByModbus = ConfigurationManager.AppSettings["OpenSpeakerByModbus"].To<bool>();
        #endregion

        #region 消防温度变量
        /// <summary>
        /// 消防报警温度(℃)
        /// </summary>
        public static decimal FireWarnTemp;
        #endregion

        #region MES变量
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public static int CurrentUserID => Business.Instance.SysPara<int>("CurrentUserID");

        /// <summary>
        /// 分容段常温静置RGVID
        /// </summary>
        public static int FcStandbyRgvId => Business.Instance.SysPara<int>("FcStandbyRgvId");

        /// <summary>
        /// 分容压床RGVID
        /// </summary>
        public static int FcRgvId => Business.Instance.SysPara<int>("FcRgvId");

        /// <summary>
        /// 高温静置RGVID
        /// </summary>
        public static int HotStandbyRgvId => Business.Instance.SysPara<int>("HotStandbyRgvId");

        /// <summary>
        /// 分容压床延迟时间
        /// </summary>
        public static int AutoMinByFc = ConfigurationManager.AppSettings["AutoMinByFc"].To<int>();

        /// <summary>
        /// 静置架延迟时间
        /// </summary>
        public static int AutoMinByStandby = ConfigurationManager.AppSettings["AutoMinByStandby"].To<int>();

        /// <summary>
        /// 常温静置温度判断阈值
        /// </summary>
        public static decimal FcStandbyHotTemp = ConfigurationManager.AppSettings["FcStandbyHotTemp"].To<decimal>();

        /// <summary>
        /// 分容压床温度判断阈值
        /// </summary>
        public static decimal FcHotTemp = ConfigurationManager.AppSettings["FcHotTemp"].To<decimal>();

        /// <summary>
        /// 高温静置温度判断阈值
        /// </summary>
        public static decimal HighStandbyHotTemp = ConfigurationManager.AppSettings["HighStandbyHotTemp"].To<decimal>();

        /// <summary>
        /// 针对分容压床库位，多少个感温点超过阈值，就直接喷淋
        /// </summary>
        public static int FcTempTooHighCount = ConfigurationManager.AppSettings["FcTempTooHighCount"].To<int>();

        /// <summary>
        /// 针对分容压床库位，若存在烟雾报警，超过多少个阈值直接喷淋
        /// </summary>
        public static int FcHasFireTooHighCount = ConfigurationManager.AppSettings["FcHasFireTooHighCount"].To<int>();

        /// <summary>
        /// 静置区阈值最低的温度，默认值是常温静置区的温度
        /// </summary>
        public static decimal MinTempForStandby = ConfigurationManager.AppSettings["FcStandbyHotTemp"].To<decimal>();
        #endregion
    }
}
