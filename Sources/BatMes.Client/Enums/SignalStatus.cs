namespace BatMes.Client.Enums
{
    /// <summary>
    /// 信号状态（设备、PLC、仪表、MES系统等）
    /// </summary>
    public enum SignalStatus
    {
        /// <summary>
        /// 离线
        /// </summary>
        Offline = 1,

        /// <summary>
        /// 在线
        /// </summary>
        Online = 2,

        /// <summary>
        /// 异常
        /// </summary>
        Exception = 3
    }
}
