using System;

namespace Tools
{
    public delegate void NotifyLogHandler(string content);

    public class NotifyMiddle
    {
        #region 单例

        private static NotifyMiddle mInstance = null;
        private static readonly object syncLock = new Object();

        public static NotifyMiddle Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (syncLock)
                    {
                        if (mInstance == null)
                            mInstance = new NotifyMiddle();
                    }
                }
                return mInstance;
            }
        }

        #endregion 单例

        //public event NotifyLogHandler OnMesLogEvent; //MES/调度 日志
        public event NotifyLogHandler OnLogAEvent; //设备日志

        public event NotifyLogHandler OnLogBEvent; //设备日志

        public event NotifyLogHandler OnScanBatteryEvent;

        public event NotifyLogHandler OnScanTrayEvent;

        public void NotifyScanBattery(string content)
        {
            if (OnScanBatteryEvent != null)
                OnScanBatteryEvent(content);
        }

        public void NotifyLogA(string msg)
        {
            if (OnLogAEvent != null)
                OnLogAEvent(msg);
        }

        public void NotifyLogB(string msg)
        {
            if (OnLogBEvent != null)
                OnLogBEvent(msg);
        }

        public void NotifyScanTray(string content)
        {
            if (OnScanTrayEvent != null)
            {
                OnScanTrayEvent(content);
            }
        }
    }
}