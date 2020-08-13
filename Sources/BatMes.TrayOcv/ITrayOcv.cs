using System;

namespace BatMes.TrayOcv
{
    /// <summary>
    /// OCV、DCIR等电池测量设备开发规范
    /// </summary>
    public interface ITrayOcv
    {
        #region 公共

        /// <summary>
        /// 初始化
        /// </summary>
        void Init();

        /// <summary>
        /// 启动
        /// </summary>
        void Start();

        /// <summary>
        /// 停止
        /// </summary>
        void Stop();

        /// <summary>
        /// 通用系统事件
        /// </summary>
        event EventHandler<EventArgs> SysEvent;

        #endregion

        #region 心跳

        /// <summary>
        /// 是否开启心跳
        /// </summary>
        bool IsHeartbeat { get; }

        /// <summary>
        /// 心跳间隔时间（毫秒）
        /// </summary>
        int HeartbeatInterval { get; }

        /// <summary>
        /// 心跳逻辑
        /// </summary>
        void Heartbeat();

        #endregion

    }
}