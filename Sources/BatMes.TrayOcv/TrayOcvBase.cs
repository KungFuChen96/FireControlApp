using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using XY;

namespace BatMes.TrayOcv
{
    /// <summary>
    /// 基类封装通用业务逻辑
    /// </summary>
    public abstract class TrayOcvBase
    {
        /// <summary>
        /// 程序是否处于运行中
        /// </summary>
        public bool IsRunning = false;

        /// <summary>
        /// 通用系统事件
        /// </summary>
        public event EventHandler<EventArgs> SysEvent;
        protected void SysEventNotice(BatMes.Client.Enums.SysEventLevel level, string title, string message)
        {
            if (this.SysEvent != null)
            {
                this.SysEvent(this, new SysEventArgs { Level = level, Title = title, Message = message });
            }
        }

        #region 系统参数

        private Dictionary<string, string> sysParaList;
        /// <summary>
        /// 缓存中系统参数列表
        /// </summary>
        public Dictionary<string, string> SysParaList
        {
            get { return this.sysParaList; }
        }

        /// <summary>
        /// 从缓存中获取系统参数值(指定类型)
        /// </summary>
        /// <typeparam name="T">指定返回的参数值类型</typeparam>
        /// <param name="paraID">参数ID</param>
        /// <returns></returns>
        public T SysPara<T>(string paraID) where T : IConvertible
        {
            paraID = XY.Text.Trim(paraID);
            if (paraID.Length > 0 && this.SysParaList != null && this.SysParaList.Count > 0 && this.SysParaList.ContainsKey(paraID))
            {
                try
                {
                    var typeConverter = TypeDescriptor.GetConverter(typeof(T));
                    if (typeConverter != null)
                    {
                        return (T)typeConverter.ConvertFromString(this.SysParaList[paraID]);
                    }
                }
                catch
                {
                    return default(T);
                }
            }

            return default(T);
        }

        /// <summary>
        /// 更新缓存中系统参数值
        /// </summary>
        /// <param name="paraID"></param>
        /// <param name="paraVal"></param>
        /// <returns></returns>
        public bool SysParaEdit(string paraID, string paraVal)
        {
            if (paraID.Length > 0)
            {
                if (this.sysParaList != null && this.sysParaList.Count > 0 && this.sysParaList.ContainsKey(paraID))
                {
                    this.sysParaList[paraID] = paraVal;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 工序

        private List<KeyValuePair<string, string>> opsList;
        /// <summary>
        /// 工序列表
        /// </summary>
        public List<KeyValuePair<string, string>> OpsList
        {
            get { return this.opsList; }
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            //初始化系统参数
            this.sysParaList = BatMes.Client.Business.Instance.SysParaList();

            #region 心跳后台线程

            if (this.IsHeartbeat)
            {
                var heartbeatThread = new Thread(() =>
                {
                    while (true)
                    {
                        if (this.IsRunning)
                        {
                            this.Heartbeat();
                        }
                        Thread.Sleep(this.HeartbeatInterval);
                    }
                });
                heartbeatThread.IsBackground = true;
                heartbeatThread.Start();
            }

            #endregion
        }

        /// <summary>
        /// 启动
        /// </summary>
        public virtual void Start()
        {
            //程序运行中
            this.IsRunning = true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public virtual void Stop()
        {
            //程序非运行中
            this.IsRunning = false;
        }

        #region 心跳

        /// <summary>
        /// 是否开启心跳
        /// </summary>
        public abstract bool IsHeartbeat { get; }

        /// <summary>
        /// 心跳间隔时间（秒）
        /// </summary>
        public abstract int HeartbeatInterval { get; }

        /// <summary>
        /// 心跳逻辑
        /// </summary>
        public abstract void Heartbeat();

        #endregion

    }//end class
}