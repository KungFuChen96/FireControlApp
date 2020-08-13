using BatMes.Client;
using FireBusiness.Model;
using Neware.BTS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.OutSystem
{
    class BtsHelper
    {
        #region 单例
        private static volatile BtsHelper mInstance = null;
        private static readonly object syncLock = new object();
        private BtsHelper() 
        {
            ConnectBtsService();
        }
        public static BtsHelper Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (syncLock)
                    {
                        if (mInstance == null)
                            mInstance = new BtsHelper();
                    }
                }
                return mInstance;
            }
        }
        #endregion

        #region 连接BTS
        /// <summary>
        /// 连接远程WCF For BTS
        /// </summary>
        private WcfClient<IBTSService> _btsWcfClient;

        /// <summary>
        /// Bts服务器远程WCF地址
        /// </summary>
        private string BtsTcpHost => Business.Instance.SysPara<string>("BtsRemoteAddress");

        /// <summary>
        /// 连接Bts服务器
        /// </summary>
        private void ConnectBtsService()
        {
            try
            {
                _btsWcfClient = _btsWcfClient ?? new WcfClient<IBTSService>(BtsTcpHost);
            }
            catch (Exception ex)
            {
                var strMsg = $"连接BTS服务器失败：{ex.Message}。";
                CORE.Instance.AddLog("连接BTS服务器失败", strMsg, BatMes.Client.Enums.LogType.Network);
                CORE.Instance.OnMessage(strMsg, BatMes.Client.Enums.SysEventLevel.Error);
            }
        }

        /// <summary>
        /// 函数描述映射
        /// </summary>
        private Dictionary<string, string> FunMap = new Dictionary<string, string>
        {
            { "TestReady" ,"托盘【{0}】向Bts发送测试指令【TestReady】"},
            { "CellsInfo", "向BTS获取库位信息【CellsInfo】"},
            { "Heartbeat", "向BTS发送心跳包"}
        };

        #endregion

        #region 调用BTS服务
        /// <summary>
        /// 向BTS发送准备测试（工步逻辑）请求
        /// </summary>
        /// <param name="trayModel"></param>
        public void TestReadyNotifyBts(TrayModel trayModel)
        {
            var trayCode = trayModel.TrayCode;
            var showMsg = string.Format(FunMap["TestReady"], trayCode);
            try
            {
                _btsWcfClient = _btsWcfClient ?? new WcfClient<IBTSService>(GetHostAddRMap(trayModel.CellID));
                var testSetting = GetTestSettingByModel(trayModel);
                var serviceRes = _btsWcfClient.UseService(t => t.TestReady(testSetting));
                CORE.Instance.AddLog(showMsg, testSetting.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                if (serviceRes.Code != ResultCode.Success)
                {
                    CORE.Instance.AddLog($"{showMsg}失败", serviceRes.Message, BatMes.Client.Enums.LogType.Network);
                    CORE.Instance.OnMessage($"{showMsg}失败:{serviceRes.Message}", BatMes.Client.Enums.SysEventLevel.Error);
                }
            }
            catch (Exception ex)
            {
                var warningMsg = $"{showMsg}失败:{ex.Message}";
                CORE.Instance.AddLog($"{showMsg}失败", warningMsg, BatMes.Client.Enums.LogType.Network);
                CORE.Instance.OnMessage(warningMsg, BatMes.Client.Enums.SysEventLevel.Error);
            }
        }

        /// <summary>
        /// 获取库位信息
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public decimal GetEquipmentsAvgTemp(string equipmentId)
        {
            var failValue = 0.00M;
            try
            {
                var serviceRes = _btsWcfClient.UseService(t => t.CellsInfo());
                if (serviceRes?.Code != ResultCode.Success)
                    return failValue;
                if (serviceRes.Data.ContainsKey(equipmentId))
                    return failValue;
                return Math.Round(serviceRes.Data[equipmentId].Temp, 2);
            }
            catch (Exception ex)
            {
                CORE.Instance.AddLog(FunMap["CellsInfo"], ex.Message, BatMes.Client.Enums.LogType.Network);
                return failValue;
            }
        }

        /// <summary>
        /// 向BTS发送心跳包
        /// </summary>
        public void Heartbeat()
        {
            try
            {
                _btsWcfClient.UseService(t => t.Heartbeat(1000));
            }
            catch (Exception ex)
            {
                var warningMsg = $"{FunMap["Heartbeat"]}失败:{ex.Message}";
            }
        }
        #endregion

        #region 其他函数
        /// <summary>
        /// 根据托盘模型获取下发指令
        /// </summary>
        /// <param name="trayModel"></param>
        /// <returns></returns>
        private TestSetting GetTestSettingByModel(TrayModel trayModel)
        {
            if (trayModel.IsEmpty() || trayModel.TrayCode.IsEmpty())
                return new TestSetting();
            TestSetting testSetting = new TestSetting
            {
                Proc = string.Empty,
                Ops = trayModel.OpsValue.ToStringEx(),
                Batch = string.Empty,
                BatteryType = string.Empty,
                Cell = trayModel.CellID.ToStringEx(),
                TrayCode = trayModel.TrayCode,
                TrayType = ChnageTypeToBts(trayModel.TrayAttr),
                BatteryCodes = trayModel.BatteryCodes,
                Channels = ChangeChannelsStatus(trayModel.Channels)
            };
            return testSetting;
        }

        /// <summary>
        /// 转换托盘类型给BTS
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private TrayType ChnageTypeToBts(BatMes.Enums.TrayAttr trayAttr)
        {
            var opRes = TrayType.Normal;
            switch (trayAttr)
            {
                case BatMes.Enums.TrayAttr.Battery:
                    opRes = TrayType.Normal;
                    break;
                case BatMes.Enums.TrayAttr.FcCalibration:
                    opRes = TrayType.Calibration;
                    break;
            }
            return opRes;
        }

        /// <summary>
        /// 转换托盘类型给BTS
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public BatMes.Enums.TrayAttr ChnageTypeToMES(TrayType trayAttr)
        {
            var opRes = BatMes.Enums.TrayAttr.Battery;
            switch (trayAttr)
            {
                case TrayType.Normal:
                    opRes = BatMes.Enums.TrayAttr.Battery;
                    break;
                case TrayType.Calibration:
                    opRes = BatMes.Enums.TrayAttr.FcCalibration;
                    break;
            }
            return opRes;
        }

        /// <summary>
        /// 通道状态枚举转换
        /// </summary>
        /// <param name="channelStatus"></param>
        /// <returns></returns>
        private ChannelStatus ChangeStatusByChannel(BatMes.Enums.ChannelStatus channelStatus)
        {
            var opRes = ChannelStatus.OK;
            switch (channelStatus)
            {
                case BatMes.Enums.ChannelStatus.Normal:
                    opRes = ChannelStatus.OK;
                    break;
                case BatMes.Enums.ChannelStatus.Failure:
                    opRes = ChannelStatus.Failed;
                    break;
                case BatMes.Enums.ChannelStatus.LockAuto:
                case BatMes.Enums.ChannelStatus.LockManual:
                    opRes = ChannelStatus.Failed;
                    break;
            }
            return opRes;
        }

        /// <summary>
        /// 通道状态转换
        /// </summary>
        /// <param name="statusByMES"></param>
        /// <returns></returns>
        public Dictionary<int, ChannelStatus> ChangeChannelsStatus(Dictionary<int, BatMes.Enums.ChannelStatus> statusByMES) 
        {
            if (statusByMES == null)
                return default;
            var opRes = new Dictionary<int, ChannelStatus>();
            statusByMES.OrderBy(t => t.Key).ToList().ForEach(t =>
            {
                opRes.Add(t.Key, ChangeStatusByChannel(t.Value));
            });
            return opRes;
        }
        #endregion

        #region BTS地址映射
        /// <summary>
        /// 存在分容上位机一对多的情况，通过库位ID获取BTS的WCF地址
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        private string GetHostAddRMap(int cellID)
        {
            return BtsTcpHost;
        }
        #endregion
    }
}
