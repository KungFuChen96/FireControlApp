using BatMes.Enums;
using BatMes.Service.Model;
using BatMes.Service.Model.Common;
using BatMes.Service.SDK.Common;
using BatMes.Service.SDK.Fc;
using FireBusiness.Model;
using Neware.BTS.Service;
using System;
using System.Collections.Generic;

namespace FireBusiness.OutSystem
{
    /// <summary>
    /// 该类用于处理和MES对接
    /// </summary>
    class MesHelper
    {
        #region 单例
        private static volatile MesHelper mInstance = null;
        private static readonly object syncLock = new object();
        private MesHelper() { }
        public static MesHelper Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (syncLock)
                    {
                        if (mInstance == null)
                            mInstance = new MesHelper();
                    }
                }
                return mInstance;
            }
        }
        #endregion

        #region 变量定义
        /// <summary>
        /// 公共HTTP获取变量
        /// </summary>
        private CommonRestful _comRestFul = default;
        public CommonRestful CommonRestful
        {
            get
            {
                _comRestFul = _comRestFul ?? new CommonRestful();
                return _comRestFul;
            }
        }

        /// <summary>
        /// 分选HTTP获取变量
        /// </summary>
        private FcRestful _fcRestFul = default;
        public FcRestful FcRestful
        {
            get
            {
                _fcRestFul = _fcRestFul ?? new FcRestful();
                return _fcRestFul;
            }
        }

        /// <summary>
        /// 函数描述映射
        /// </summary>
        private readonly Dictionary<string, string> FunMap = new Dictionary<string, string>
        {
            { "TrayValid",  "【{0}】向MES校验托盘"},
            { "TrayBatteryList", "【{0}】向MES获取电芯条码【TrayBatteryList】"},
            { "ChannelStatus", "【{0}】向MES获取工位的通道状态【ChannelStatus】"},
            { "ProcessFile", "向MES获取工步路径【ProcessFile】"},
            { "CompleteTesting", "【{0}】向MES上传电芯测试结果【CompleteTesting】"},
            { "OutReady", "【{0}】向MES请求出盘【OutReady】" },
            { "OutUnload", "【{0}】请求无条件搬运到出盘工位【OutUnload】" },
            { "ProcInfo", "【{0}】向MES获取工艺信息【ProcInfo】"},
            { "FireSmoke", "【{0}】向MES发送消防烟雾报警【FireSmoke】" },
            { "FireTemp", "【{0}】向MES发送消防温度报警【FireTemp】" },
            { "Submit", "【{0}】上传MES电芯测试数据【Submit】"},
            { "Warning", "【{0}】库位告警"},
            { "CompleteByBattery", "完成电芯数据提交【已过站】"},
            { "OutChange", "【{0}】请求切换库位【OutChange】"}
        };
        #endregion

        #region 业务逻辑(调用)
        /// <summary>
        /// 检验托盘
        /// </summary>
        /// <param name="opsValue"></param>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        public bool TrayValid(string trayCode, int opsValue)
        {
            try
            {
                var opRes = CommonRestful.TrayValid(trayCode, opsValue);
                CORE.Instance.AddLog($"{string.Format(FunMap["TrayValid"], trayCode)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["TrayValid"], trayCode)}-【{opsValue}】失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// 获取托盘里面的电芯条码
        /// </summary>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        public Dictionary<int, string> TrayBatteryList(string trayCode)
        {
            try
            {
                var opRes = CommonRestful.TrayBatteryList(trayCode);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["TrayBatteryList"], trayCode)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(FunMap["TrayBatteryList"], trayCode)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes?.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["TrayBatteryList"], trayCode)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// 获取通道状态
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public Dictionary<int, BatMes.Enums.ChannelStatus> ChannelStatus(int cellID)
        {
            try
            {
                var opRes = FcRestful.ChannelStatusWithCellID(cellID);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["ChannelStatus"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(FunMap["ChannelStatus"], cellID)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes?.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["ChannelStatus"], cellID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// 新需求变更(无需转换BTS的数据，直接转发给MES)
        /// </summary>
        /// <param name="testResult"></param>
        /// <param name="opsID"></param>
        /// <param name="times">暂时填0，让MES自动识别</param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Dictionary<string, string> FcSubmit(MESResults mESResults)
        {
            try
            {
                if (mESResults.IsEmpty())
                    return default;
                var opRes = default(BatMes.Service.Model.ServiceResult<Dictionary<string, string>>);
                int retryCount = 1;
                while (retryCount <= 3)
                {
                    opRes = FcRestful.Submit(mESResults.TestResult, mESResults.OpsValue, 0, mESResults.TrayCode, mESResults.UserID);
                    if (opRes?.Code == ServiceResultCode.Success)
                        break;
                    retryCount++;
                }
                CORE.Instance.AddLog($"{string.Format(FunMap["Submit"], mESResults.TrayCode)}结果", opRes.JsonEncode() + $"，工序【{mESResults.OpsValue}】-次数【{mESResults.Times}】", BatMes.Client.Enums.LogType.Network);
                return opRes?.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["Submit"], mESResults.TrayCode)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// OK的电芯已过站
        /// </summary>
        /// <param name="opsID"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public Dictionary<string, string> CompleteByBattery(string[] batCodes, int opsVal, string trayCode)
        {
            try
            {
                var opRes = FcRestful.Complete(batCodes, opsVal, trayCode);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{FunMap["CompleteByBattery"]}失败：{opRes?.Message ?? "返回值为空"}。";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{FunMap["CompleteByBattery"]}结果", opRes.JsonEncode() + $"； 参数：{batCodes.JsonEncode()} - {opsVal}", BatMes.Client.Enums.LogType.Network);
                return opRes?.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{FunMap["CompleteByBattery"]}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        public string ProcessFile(int opsID, int times)
        {
            try
            {
                var opRes = FcRestful.ProcessFile(opsID, times);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{FunMap["ProcessFile"]}失败：{opRes?.Message ?? "返回值为空"}。";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{FunMap["ProcessFile"]}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes?.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{FunMap["ProcessFile"]}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// 通知出盘
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="opsID"></param>
        /// <param name="trayAttr"></param>
        /// <param name="trayCodes"></param>
        /// <returns></returns>
        public bool OutReady(int cellID, int opsID, TrayAttr trayAttr, string trayCodes)
        {
            try
            {
                var opRes = FcRestful.OutReady(cellID, opsID, trayCodes);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["OutReady"], trayCodes)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                var preTitlt = string.Format(Configs.preTitle, trayCodes, opsID, cellID);
                CORE.Instance.AddLog($"{string.Format(FunMap["OutReady"], trayCodes)}结果", opRes.JsonEncode() + $" 参数{preTitlt}", BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["OutReady"], trayCodes)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// 请求无条件搬运到出盘工位
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="opsID"></param>
        /// <param name="trayAttr"></param>
        /// <param name="trayCodes"></param>
        /// <returns></returns>
        public bool OutUnload(int cellID, int opsID, TrayAttr trayAttr, string trayCodes)
        {
            try
            {
                var opRes = FcRestful.OutUnload(cellID, opsID, trayCodes);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["OutUnload"], trayCodes)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                var preTitlt = string.Format(Configs.preTitle, trayCodes, opsID, cellID);
                CORE.Instance.AddLog($"{string.Format(FunMap["OutUnload"], trayCodes)}结果", opRes.JsonEncode() + $" 参数{preTitlt}", BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["OutUnload"], trayCodes)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// 请求切换库位
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="opsID"></param>
        /// <param name="trayCodes"></param>
        /// <returns></returns>
        public bool OutChange(int cellID, int opsID, string trayCodes)
        {
            try
            {
                var opRes = FcRestful.OutChange(cellID, opsID, trayCodes);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["OutChange"], trayCodes)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                var preTitlt = string.Format(Configs.preTitle, trayCodes, opsID, cellID);
                CORE.Instance.AddLog($"{string.Format(FunMap["OutChange"], trayCodes)}结果", opRes.JsonEncode() + $" 参数{preTitlt}", BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["OutChange"], trayCodes)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// 获取工艺信息
        /// </summary>
        /// <param name="procID"></param>
        /// <returns></returns>
        public ProcInfo GetProcInfo(int procID)
        {
            try
            {
                var opRes = CommonRestful.ProcInfo(procID);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["ProcInfo"], procID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                }
                CORE.Instance.AddLog($"{string.Format(FunMap["ProcInfo"], procID)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes?.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["ProcInfo"], procID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// 消防烟雾报警
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public DeviceFireMode CallFireSmoke(int cellID)
        {
            try
            {
                var opRes = FcRestful.FireSmoke(cellID);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["FireSmoke"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(FunMap["FireSmoke"], cellID)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["FireSmoke"], cellID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return DeviceFireMode.None;
            }
        }

        /// <summary>
        /// 消防温度报警
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="temp"></param>
        /// <returns></returns>
        public DeviceFireMode CallFireTemp(int cellID, decimal temp)
        {
            try
            {
                var opRes = FcRestful.FireTemp(cellID, temp);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["FireTemp"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(FunMap["FireTemp"], cellID)}结果", opRes.JsonEncode() + $" 参数：{cellID}-{temp}", BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["FireTemp"], cellID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return DeviceFireMode.None;
            }
        }

        /// <summary>
        /// 库位告警
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="isDisable"></param>
        /// <param name="warningCode"></param>
        /// <param name="warnMsg"></param>
        /// <returns></returns>
        public bool CellWarning(int cellID, bool isDisable, CellWarningCode warningCode, string warnMsg)
        {
            try
            {
                var opRes = FcRestful.Warning(cellID, isDisable, warningCode, warnMsg);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FunMap["Warning"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(FunMap["Warning"], cellID)}结果", opRes.JsonEncode() + $" 参数：{cellID}-{isDisable}-{warningCode}", BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FunMap["Warning"], cellID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }
        #endregion

        #region 其他
        /// <summary>
        /// BTS的结果数据转成上传MES的模型
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public MESResults GetMESResults(TestResult testResult, TrayModel trayModel)
        {
            if (testResult.IsEmpty())
                return default;
            if (trayModel.IsEmpty())
            {
                var showMsg = $"在本地找不到托盘模型【{testResult.TrayCode}】";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                return default;
            }
            var trayCode = testResult.TrayCode;
            var testModel = new MESResults
            {
                TrayCode = trayCode,
                CellID = trayModel.CellID,
                OpsValue = trayModel.OpsValue,
                TrayAttr = trayModel.TrayAttr,       
                Times = trayModel.Times
            };
            return testModel;
        }
        #endregion
    }
}
