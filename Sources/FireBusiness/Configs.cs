
using BatMes.Client;
using FireBusiness.Enums;
using System;
using System.Configuration;

namespace FireBusiness
{
    public class Configs
    {
        #region MES
        /// <summary>
        /// 和MES自定义的NgCode
        /// </summary>
        public static string ngCode = "-1";
        #endregion

        #region PLC

        /// <summary>
        /// 请求长度
        /// </summary>
        public static int resquestLen => 30;

        /// <summary>
        /// 转折点
        /// </summary>
        public static int turningPoint => 16;

        /// <summary>
        /// 请求压合开始信号点  PLC TO 调度信号
        /// </summary>
        public static string requestPressingStart => "R7000";

        /// <summary>
        /// 放盘完成开始信号 调度信号 TO PLC 
        /// </summary>
        public static string inFinishStart => "R706";

        /// <summary>
        /// 程序正常运行信号 PLC TO 调度信号
        /// </summary>
        public static string runningFlagStart => "R7180";

        /// <summary>
        /// 故障开始信号点DT1000 PLC TO 调度信号
        /// </summary>
        public static string faultFlagStart => "D10000";

        /// <summary>
        /// 入盘规格 调度信号 TO PLC 
        /// </summary>
        public static string inType => "D10050";

        /// <summary>
        /// 单层故障复位
        /// </summary>
        public static string resetStart => "R730";

        /// <summary>
        /// 请求取盘信号
        /// </summary>
        public static string resquestOut => "R7030";

        /// <summary>
        /// 出盘完成信号
        /// </summary>
        public static string outFinishStart => "R709";

        /// <summary>
        /// 火警消防启动允许 PLC TO 调度信号
        /// </summary>
        public static string openFireFlag => "R7240";

        /// <summary>
        /// 火警消防允许确认 调度信号 TO PLC 
        /// </summary>
        public static string confirmFireFlag => "R727";
        #endregion

        #region quartz变量
        /// <summary>
        /// quartz任务名称
        /// </summary>
        public static string paramName => "quartzObjName";

        /// <summary>
        /// quartz任务类型名称
        /// </summary>
        public static string typeName => "quartzObjType";

        /// <summary>
        /// 公共的任务Id变量名
        /// </summary>
        public static string taskIdName => "taskIdName";

        /// <summary>
        /// 格式化重复的日志字符串,前标题
        /// </summary>
        public static string preTitle => "托盘【{0}】-工序【{1}】-库位【{2}】，";

        /// <summary>
        /// 格式化重复的日志字符串,前标题
        /// </summary>
        public static string preTitleNoOps => "托盘【{0}】-库位【{1}】，";

        /// <summary>
        /// jobKey前缀
        /// </summary>
        public static string jobKey => "jobKey_{0}_{1}_{2}";

        #endregion

        #region quartz时间
        /// <summary>
        /// 加入队列后，隔多久才开始(单位：秒)
        /// </summary>
        public static int startTimeSeconds => 1;

        /// <summary>
        /// 加入队列后，每隔多久执行（单位：分钟）
        /// </summary>
        public static int intervalInMinutes => 1;

        /// <summary>
        /// 每隔多少秒执行队列任务
        /// </summary>
        public static int intervalInseconds => 60;
        #endregion quartz时间

        #region 状态

        #endregion

        #region 消防
        /// <summary>
        /// 消防任务名称
        /// </summary>
        public static string fireName => "FcFireTask";

        /// <summary>
        /// 故障任务名称
        /// </summary>
        public static string faultName => "FcFaultTask";

        /// <summary>
        /// 消防开始时间
        /// </summary>
        public static int startFire => 2;

        /// <summary>
        /// 间隔时间（秒）
        /// </summary>
        public static int intervalFire => ConfigurationManager.AppSettings["intervalFire"].ToStringEx().ToInt32();

        /// <summary>
        /// 故障查询间隔时间（秒）
        /// </summary>
        public static int intervalFault => ConfigurationManager.AppSettings["intervalFault"].ToStringEx().ToInt32();

        /// <summary>
        /// 静置架第一行的标识
        /// </summary>
        public static int fireOneFlag => 1;

        /// <summary>
        /// 静置架第二行的标识
        /// </summary>
        public static int fireTwoFlag => 2;

        /// <summary>
        /// 消防发送频率，考虑到消防信号不能复位的情况
        /// </summary>
        public static int fireFrequency => 30;

        /// <summary>
        /// 烟雾感应起始地址，共28个库位
        /// </summary>
        public static string smokeStartAt => "R3015";

        /// <summary>
        /// 总共有多少个烟雾感应库位
        /// </summary>
        public static int smokeCount => 28;

        /// <summary>
        /// 喷淋起始地址
        /// </summary>
        public static string doSpray => "D1020";

        /// <summary>
        /// 喷淋地址长度
        /// </summary>
        public static int sprayCount => 7;

        /// <summary>
        /// 是否开启烟雾报警判断
        /// </summary>
        public static bool IsOpenSmoke = Convert.ToBoolean(ConfigurationManager.AppSettings["IsOpenSmoke"].ToInt32());

        /// <summary>
        /// 是否开启感光纤维判断
        /// </summary>
        public static bool IsOpenMBus = Convert.ToBoolean(ConfigurationManager.AppSettings["IsOpenMBus"].ToInt32());

        /// <summary>
        /// 喷淋PLC的IP地址
        /// </summary>
        public static string PlcAddIpForSpray => ConfigurationManager.AppSettings["PlcAddIpForSpray"].ToStringEx();

        /// <summary>
        /// 喷淋PLC的端口
        /// </summary>
        public static int PlcAddPortForSpray => ConfigurationManager.AppSettings["PlcAddPortForSpray"].ToInt32();
        #endregion

        #region 出盘任务
        /// <summary>
        /// 出盘任务的队列
        /// </summary>
        public static string outReadyJobKey => "out_{0}_{1}";

        /// <summary>
        /// 出盘任务名称
        /// </summary>
        public static string outReadyName => "outReadyTask";

        /// <summary>
        /// 出盘开始时间
        /// </summary>
        public static int startOutReady => 2;

        /// <summary>
        /// 间隔时间（秒）
        /// </summary>
        public static int intervalOutReady => 6;
        #endregion

        #region ModBus
        /// <summary>
        /// 从站字节
        /// </summary>
        public static byte slaveAddR => Business.Instance.SysPara<byte>("SlaveAddress");

        /// <summary>
        /// 读取地址
        /// </summary>
        public static string addrByModBus => Business.Instance.SysPara<string>("AddrByModBus");

        /// <summary>
        /// 总共有多少个
        /// </summary>
        public static int addrCountByBus => 30;

        /// <summary>
        /// 读取到的值除数
        /// </summary>
        public static int divVal => 1;
        #endregion

        #region 消防温度变量
        /// <summary>
        /// 分容工艺ID：获取工艺信息
        /// </summary>
        public static int procID => Business.Instance.SysPara<byte>("ProcID");

        /// <summary>
        /// 消防报警温度(℃)
        /// </summary>
        public static decimal FireWarnTemp;

        /// <summary>
        /// 消防执行温度(℃)
        /// </summary>
        public static decimal FireActTemp;
        #endregion

        #region MES自定义代码
        /// <summary>
        /// 电芯合格
        /// </summary>
        public static string OkCode => "1";
        #endregion
    }
}
