using BatMes.Client;
using BatMes.Client.Enums;
using FireBusiness.Dev;
using FireBusiness.Enums;
using FireBusiness.Model;
using FireBusiness.WCF;
using System;
using System.Threading.Tasks;

namespace FireBusiness
{
    /// <summary>
    /// 核心业务逻辑
    /// </summary>
    public class CORE : BatMes.TrayOcv.TrayOcvBase, IClient, BatMes.TrayOcv.ITrayOcv
    {
        #region 单例

        private static volatile CORE mInstance = null;
        private static readonly object syncLock = new Object();

        private CORE() { }

        public static CORE Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (syncLock)
                    {
                        if (mInstance == null)
                            mInstance = new CORE();
                    }
                }
                return mInstance;
            }
        }

        #endregion

        #region 公共

        /// <summary>
        /// 服务编号
        /// </summary>
        public string ServiceCode { get { return "2K92C3XY01"; } }

        /// <summary>
        /// 软件交付时间
        /// </summary>
        public DateTime DeliveryTime { get { return new DateTime(2020, 7, 15); } }

        /// <summary>
        /// 免费软件服务天数
        /// </summary>
        public int FreeDays { get { return 1095; } }

        /// <summary>
        /// 软件授权使用天数
        /// </summary>
        public int AuthDays { get { return 0; } }

        /// <summary>
        /// 软件版本号
        /// </summary>
        public string Version { get { return "1.0.0"; } }

        /// <summary>
        /// 初始化
        /// </summary>
        public async Task StartTask()
        {
            await Task.Run(() => base.Start());
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        public void StartService()
        {
            Instance.StartTask().ContinueWith(t =>
            {
                //CORE.Instance.OnMessage("初始化完成", SysEventLevel.Info);
                //WcfService.WcfServiceForMES(CORE.Instance.SysPara<string>("TcpServiceForMES"), CORE.Instance.SysPara<string>("HttpServiceForMES"));
                DEV.Instance.OpenDevice().ContinueWith(k =>
                {
                    //DEV.Instance.AlwaysOnDev();
                    CoreBusiness.GreatHub();
                    CoreBusiness.Hub.StartTaskWhenInit();
                });
            });
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// 启动
        /// </summary>
        public override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop()
        {
            base.Stop();
        }

        #endregion

        #region 消息\日志\事件
        /// <summary>
        /// 消息发布
        /// </summary>
        public event EventHandler<EventArgs> NoticeLogEvent;

        /// <summary>
        /// 写日志到面板
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="eventLevel"></param>
        public void OnMessage(string strMsg, SysEventLevel eventLevel)
        {
            NoticeLogEvent?.Invoke(this, new LogEventArgs { Message = strMsg, EventLevel = eventLevel});
        }

        /// <summary>
        ///  添加系统之间的交互日志到数据库\可选打印日志
        /// </summary>
        /// <returns></returns>
        public bool AddLog(string titleMsg, 
                           string conMsg,
                           LogType logType, 
                           bool showMsg = false, 
                           SysEventLevel eventLevel = SysEventLevel.Info)
        {
            Business.Instance.LogAdd(titleMsg, conMsg, logType);
            if (showMsg)
                OnMessage($"{titleMsg}：{conMsg}", eventLevel);
            return true;
        }

        /// <summary>
        /// 添加本地系统事件到数据库，便于运维和定位问题
        /// </summary>
        /// <returns></returns>
        public bool AddSysEventLog(string titleMsg, string conMsg, SysEventLevel level)
        {
            return Business.Instance.SysEventAdd(titleMsg, conMsg, level);
        }

        /// <summary>
        /// 异常退出事件(如发生火警等情况)
        /// </summary>
        public event EventHandler<EventArgs> AbnormalExitEvent;

        /// <summary>
        /// 非正常停止程序通知
        /// </summary>
        /// <param name="isEnd"></param>
        public void AbnormalStopNotice(LogEventArgs logEvent)
        {
            AbnormalExitEvent?.Invoke(this, logEvent);
        }
        #endregion
        #region 心跳

        /// <summary>
        /// 是否开启心跳
        /// </summary>
        public override bool IsHeartbeat { get { return false; } }

        /// <summary>
        /// 心跳间隔时间（秒）
        /// </summary>
        public override int HeartbeatInterval { get { return 1000; } }

        /// <summary>
        /// 心跳逻辑
        /// </summary>
        public override void Heartbeat()
        {
            //向PLC发送心跳信号
        }
        #endregion
    }
}