using BatMes.Enums;
using BatMes.Service.Model;
using BatMes.Service.Model.Common;
using BatMes.Service.Model.Fc;
using BatMes.Service.Model.Standby;
using BatMes.Service.SDK.Common;
using BatMes.Service.SDK.Custom;
using BatMes.Service.SDK.Fc;
using BatMes.Service.SDK.Standby;
using FireBusiness.Model;
using Neware.BTS.Service;
using System;
using System.Collections.Generic;

namespace FireBusiness.OutSystem
{
    /// <summary>
    /// 该类用于处理和MES对接
    /// </summary>
    class MesManager
    {
        #region 单例
        private static volatile MesManager mInstance = null;
        private static readonly object syncLock = new object();
        private MesManager() { }
        public static MesManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (syncLock)
                    {
                        if (mInstance == null)
                            mInstance = new MesManager();
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
        /// 定制化
        /// </summary>
        private CustomRestful _customRestFul = default(CustomRestful);
        public CustomRestful CustomRestful
        {
            get
            {
                _customRestFul = _customRestFul ?? new CustomRestful();
                return _customRestFul;
            }
        }

        /// <summary>
        /// 分容HTTP获取变量
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
        /// 静置HTTP获取变量
        /// </summary>
        private StandbyRestful _standbyRestFul = default;
        public StandbyRestful StandbyRestful
        {
            get
            {
                _standbyRestFul = _standbyRestFul ?? new StandbyRestful();
                return _standbyRestFul;
            }
        }

        /// <summary>
        /// 分容及公共函数描述映射
        /// </summary>
        private readonly Dictionary<string, string> FcFunMap = new Dictionary<string, string>
        {
            { "OutReady", "【{0}】向MES请求出盘【OutReady】" },
            { "OutUnload", "【{0}】请求无条件搬运到出盘工位【OutUnload】" },
            { "HostList", "【{0}】获取指定RGV关联的电源上位机与工位信息【HostList】"},
            { "FireSmoke", "【{0}】向MES发送消防烟雾报警【FireSmoke】（分容）" },
            { "FireTemp", "【{0}】向MES发送消防温度报警【FireTemp】（分容）" },
            { "Warning", "【{0}】库位告警【Warning】（分容）"},
        };

        /// <summary>
        /// 静置函数映射
        /// </summary>
        private readonly Dictionary<string, string> StandbyFunMap = new Dictionary<string, string>
        {
            { "StoreList", "【{0}】获取指定RGV关联的静置设备与工位信息【StoreList】"},
            { "FireSmoke", "【{0}】向MES发送消防烟雾报警【FireSmoke】（静置架）" },
            { "FireTemp", "【{0}】向MES发送消防温度报警【FireTemp】（静置架）" },
            { "Warning", "【{0}】库位告警【Warning】（静置架）"}
        };

        /// <summary>
        /// 定制化函数映射
        /// </summary>
        private readonly Dictionary<string, string> CustomFunMap = new Dictionary<string, string>
        {
            { "WeiLanFireWarning", "库位【{0}】消防告警【WeiLanFireWarning】"},
            { "WeiLanFireSpray", "库位【{0}】消防喷淋【WeiLanFireSpray】" }
        };
        #endregion

        #region 业务逻辑(分容)

        /// <summary>
        /// 获取指定RGV关联的电源上位机与工位信息
        /// </summary>
        /// <param name="procID"></param>
        /// <returns></returns>
        public List<HostInfo> HostListByFc(int rgvId)
        {
            try
            {
                var opRes = FcRestful.HostList(rgvId);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FcFunMap["HostList"], rgvId)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                }
                CORE.Instance.AddLog($"{string.Format(FcFunMap["HostList"], rgvId)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes?.Code == ServiceResultCode.Success ? opRes.Data : default;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FcFunMap["HostList"], rgvId)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// 消防烟雾报警
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public DeviceFireMode CallFireSmokeByFc(int cellID, bool hasCustom = true)
        {
            //有定制化需求
            if (hasCustom)
            {
                WeiLanFireWarning(DeviceType.Fc, cellID);
                return DeviceFireMode.None;
            }

            //非定制化需求
            if (CoreBusiness.Hub.IsDebug)
            {
                if (CoreBusiness.Hub.IsSimSpray)
                    return DeviceFireMode.Spray;
                else
                    return DeviceFireMode.None;
            }
            try
            {
                var opRes = FcRestful.FireSmoke(cellID);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FcFunMap["FireSmoke"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(FcFunMap["FireSmoke"], cellID)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes?.Code == ServiceResultCode.Success ? opRes.Data : DeviceFireMode.None;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FcFunMap["FireSmoke"], cellID)}失败：{ex.Message}";
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
        public DeviceFireMode CallFireTempByFc(int cellID, decimal temp, bool hasCustom = true)
        {
            //有定制化需求
            if (hasCustom)
            {
                WeiLanFireWarning(DeviceType.Fc, cellID);
                return DeviceFireMode.None;
            }

            if (CoreBusiness.Hub.IsDebug)
            {
                if (CoreBusiness.Hub.IsSimSpray)
                    return DeviceFireMode.Spray;
                else
                    return DeviceFireMode.None;
            }
            try
            {
                var opRes = FcRestful.FireTemp(cellID, temp);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FcFunMap["FireTemp"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(FcFunMap["FireTemp"], cellID)}结果", opRes.JsonEncode() + $" 参数：{cellID}-{temp}", BatMes.Client.Enums.LogType.Network);
                return opRes?.Code == ServiceResultCode.Success ? opRes.Data : DeviceFireMode.None;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FcFunMap["FireTemp"], cellID)}失败：{ex.Message}";
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
        public bool CellWarningByFc(int cellID, bool isDisable, CellWarningCode warningCode, string warnMsg)
        {
            try
            {
                var opRes = FcRestful.Warning(cellID, isDisable, warningCode, warnMsg);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(FcFunMap["Warning"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(FcFunMap["Warning"], cellID)}结果", opRes.JsonEncode() + $" 参数：{cellID}-{isDisable}-{warningCode}", BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(FcFunMap["Warning"], cellID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }
        #endregion

        #region 业务逻辑(静置架)
        /// <summary>
        /// 获取指定RGV关联的静置设备与工位信息
        /// </summary>
        /// <param name="procID"></param>
        /// <returns></returns>
        public List<StoreInfo> StoreListByStandby(int rgvId)
        {
            try
            {
                var opRes = StandbyRestful.StoreList(rgvId);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(StandbyFunMap["StoreList"], rgvId)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                }
                CORE.Instance.AddLog($"{string.Format(StandbyFunMap["StoreList"], rgvId)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes?.Code == ServiceResultCode.Success ? opRes.Data : default;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(StandbyFunMap["StoreList"], rgvId)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// 消防烟雾报警
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public DeviceFireMode CallFireSmokeByStandby(int cellID, bool hasCustom = true)
        {
            //有定制化需求
            if (hasCustom)
            {
                WeiLanFireWarning(DeviceType.Standby, cellID);
                return DeviceFireMode.None;
            }

            if (CoreBusiness.Hub.IsDebug)
            {
                if (CoreBusiness.Hub.IsSimSpray)
                    return DeviceFireMode.Spray;
                else
                    return DeviceFireMode.None;
            }
            try
            {
                var opRes = StandbyRestful.FireSmoke(cellID);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(StandbyFunMap["FireSmoke"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(StandbyFunMap["FireSmoke"], cellID)}结果", opRes.JsonEncode(), BatMes.Client.Enums.LogType.Network);
                return opRes?.Code == ServiceResultCode.Success ? opRes.Data : DeviceFireMode.None;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(StandbyFunMap["FireSmoke"], cellID)}失败：{ex.Message}";
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
        public DeviceFireMode CallFireTempByStandby(int cellID, decimal temp, bool hasCustom = true)
        {
            //有定制化需求
            if (hasCustom)
            {
                WeiLanFireWarning(DeviceType.Standby, cellID);
                return DeviceFireMode.None;
            }

            if (CoreBusiness.Hub.IsDebug)
            {
                if (CoreBusiness.Hub.IsSimSpray)
                    return DeviceFireMode.Spray;
                else
                    return DeviceFireMode.None;
            }
            try
            {
                var opRes = StandbyRestful.FireTemp(cellID, temp);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(StandbyFunMap["FireTemp"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(StandbyFunMap["FireTemp"], cellID)}结果", opRes.JsonEncode() + $" 参数：{cellID}-{temp}", BatMes.Client.Enums.LogType.Network);
                return opRes?.Code == ServiceResultCode.Success ? opRes.Data : DeviceFireMode.None;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(StandbyFunMap["FireTemp"], cellID)}失败：{ex.Message}";
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
        public bool CellWarningByStandby(int cellID, bool isDisable, CellWarningCode warningCode, string warnMsg)
        {
            try
            {
                var opRes = StandbyRestful.Warning(cellID, isDisable, warningCode, warnMsg);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(StandbyFunMap["Warning"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(StandbyFunMap["Warning"], cellID)}结果", opRes.JsonEncode() + $" 参数：{cellID}-{isDisable}-{warningCode}", BatMes.Client.Enums.LogType.Network);
                return opRes.Data;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(StandbyFunMap["Warning"], cellID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// 卫蓝消防告警
        /// </summary>
        /// <param name="deviceType">设备类型</param>
        /// <param name="cellID">工位ID</param>
        /// <returns></returns>
        public bool WeiLanFireWarning(BatMes.Enums.DeviceType deviceType, int cellID)
        {
            if (CoreBusiness.Hub.IsDebug)
                return default;
            try
            {
                var opRes = CustomRestful.WeiLanFireWarning(deviceType, cellID);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(CustomFunMap["WeiLanFireWarning"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(CustomFunMap["WeiLanFireWarning"], cellID)}结果", opRes.JsonEncode() + $" 参数：{deviceType}，{cellID}", BatMes.Client.Enums.LogType.Network);
                return opRes?.Code == ServiceResultCode.Success ? opRes.Data : default;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(CustomFunMap["WeiLanFireWarning"], cellID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }

        /// <summary>
        /// 卫蓝消防喷淋
        /// </summary>
        /// <param name="deviceType">设备类型</param>
        /// <param name="cellID">工位ID</param>
        /// <returns></returns>
        public bool WeiLanFireSpray(BatMes.Enums.DeviceType deviceType, int cellID)
        {
            if (CoreBusiness.Hub.IsDebug)
                return default;
            try
            {
                var opRes = CustomRestful.WeiLanFireSpray(deviceType, cellID);
                if (opRes?.Code != ServiceResultCode.Success)
                {
                    var showMsg = $"{string.Format(CustomFunMap["WeiLanFireSpray"], cellID)}失败：{opRes?.Message ?? "返回值为空"}";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                }
                CORE.Instance.AddLog($"{string.Format(CustomFunMap["WeiLanFireSpray"], cellID)}结果", opRes.JsonEncode() + $" 参数：{deviceType}，{cellID}", BatMes.Client.Enums.LogType.Network);
                return opRes?.Code == ServiceResultCode.Success ? opRes.Data : default;
            }
            catch (Exception ex)
            {
                var errorMsg = $"{string.Format(CustomFunMap["WeiLanFireSpray"], cellID)}失败：{ex.Message}";
                CORE.Instance.OnMessage(errorMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
        }
        #endregion

        #region 其他

        #endregion
    }
}
