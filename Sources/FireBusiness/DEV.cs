using BatMes.Client;
using FireBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FireBusiness.Dev
{
    /// <summary>
    /// 设备硬件
    /// </summary>
    public class DEV
    {
        #region 单例
        private static volatile DEV mInstance = null;
        private static readonly object syncLock = new object();

        private DEV()
        {
            try
            {
                #region 连接PLC
                this._plc = new BatMes.Device.PLC.Panasonic.Mewtocol.PlcNet(Business.Instance.SysPara<string>("PlcTcpIpHost"), Business.Instance.SysPara<int>("PlcTcpIpPost"));

                if (Configs.IsOpenSmoke)
                    this._plcSpray = new BatMes.Device.PLC.Panasonic.Mewtocol.PlcNet(Configs.PlcAddIpForSpray, Configs.PlcAddPortForSpray);
                #endregion

                #region 连接消防通过ModBus
                var busIp = Business.Instance.SysPara<string>("FireIpAddrModbus");
                var busPort = Business.Instance.SysPara<int>("FirePortAddrModbus");
                if (Configs.IsOpenMBus)
                    _plcByModBus = new BatMes.Device.PLC.Modbus.PlcNet(busIp, busPort);

                ///解析ModBus地址
                _readModBus = MyAddressAnalysis(Configs.slaveAddR, Configs.addrByModBus);
                #endregion

                #region 初始化PLC信号点
                //初始化写的PLC地址，并建立映射关系
                InitializeAddR(Configs.inFinishStart, InFinishMap);
                InitializeAddR(Configs.inType, TypeMap, true);
                InitializeAddR(Configs.resetStart, ResetMap);
                InitializeAddR(Configs.outFinishStart, OutFinshMap);
                InitializeAddR(Configs.confirmFireFlag, FireConfirmMap);
                OperationMap.Add(SignalType.W_InFinish, InFinishMap);
                OperationMap.Add(SignalType.W_InType, TypeMap);
                OperationMap.Add(SignalType.W_FalutReset, ResetMap);
                OperationMap.Add(SignalType.W_OutFinish, OutFinshMap);
                OperationMap.Add(SignalType.W_ConfirmFire, FireConfirmMap);

                //初始化（批量读），并建立映射关系
                _requestPress = MyAddressAnalysis(Configs.requestPressingStart);
                _runningFlag = MyAddressAnalysis(Configs.runningFlagStart);
                _faultFlag = MyAddressAnalysis(Configs.faultFlagStart);
                _requestOut = MyAddressAnalysis(Configs.resquestOut);
                _openFire = MyAddressAnalysis(Configs.openFireFlag);
                BulkReadMap.Add(SignalType.R_RequestPress, _requestPress);
                BulkReadMap.Add(SignalType.R_RunningFlag, _runningFlag);
                BulkReadMap.Add(SignalType.R_FaultFlag, _faultFlag);
                BulkReadMap.Add(SignalType.R_RequestOut, _requestOut);
                BulkReadMap.Add(SignalType.R_OpenFire, _openFire);

                //初始化（单个读），并建立映射关系
                InitializeAddR(Configs.requestPressingStart.Substring(0, Configs.requestPressingStart.Length - 1), RequestPressMap);
                InitializeAddR(Configs.runningFlagStart.Substring(0, Configs.runningFlagStart.Length - 1), RunningFlagMap);
                InitializeAddR(Configs.faultFlagStart, FaultFlagMap, true);
                InitializeAddR(Configs.resquestOut.Substring(0, Configs.resquestOut.Length - 1), RequestOutMap);
                InitializeAddR(Configs.openFireFlag.Substring(0, Configs.openFireFlag.Length - 1), OpenFireMap);
                OperationMap.Add(SignalType.OR_RequestPress, RequestPressMap);
                OperationMap.Add(SignalType.OR_RunningFlag, RunningFlagMap);
                OperationMap.Add(SignalType.OR_FaultFlag, FaultFlagMap);
                OperationMap.Add(SignalType.OR_RequestOut, RequestOutMap);
                OperationMap.Add(SignalType.OR_OpenFire, OpenFireMap);
                #endregion

                #region 烟雾感应/喷淋
                InitSmokeAddR(Configs.smokeStartAt, SmokeMap);
                OperationMap.Add(SignalType.R_HasSmoke, SmokeMap);
                SprayMap = InitSprayAddR();
                #endregion
            }
            catch (Exception err)
            {
                Business.Instance.SysEventAdd("地址解析发生异常", err.ToString(), BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.OnMessage("地址解析发生异常,详情请看日志", BatMes.Client.Enums.SysEventLevel.Error);
            }
        }

        public static DEV Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (syncLock)
                    {
                        if (mInstance == null)
                            mInstance = new DEV();
                    }
                }
                return mInstance;
            }
        }
        #endregion 单例

        #region 附加操作
        /// <summary>
        /// 扩展AddressAnalysis方法，省去重复代码
        /// </summary>
        /// <param name="_myAddress"></param>
        /// <returns></returns>
        private BatMes.Device.PLC.Panasonic.Address MyAddressAnalysis(string _myAddress)
        {
            var addressRes = BatMes.Device.PLC.Panasonic.Mewtocol.Tools.AddressAnalysis(_myAddress, out string plcMsg);
            if (addressRes == null)
            {
                Business.Instance.SysEventAdd($"PLC地址【{_myAddress}】解析失败", plcMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.OnMessage($"PLC地址【{_myAddress}】解析失败", BatMes.Client.Enums.SysEventLevel.Error);
            }
            return addressRes;
        }

        /// <summary>
        /// 解析ModBus地址
        /// </summary>
        /// <returns></returns>
        private BatMes.Device.PLC.Modbus.AddressModbus MyAddressAnalysis(byte slaveAddr, string _myAddress)
        {
            var addressRes = BatMes.Device.PLC.Modbus.Tools.AddressAnalysis(slaveAddr, _myAddress, out string plcMsg);
            if (addressRes == null || !plcMsg.IsEmpty())
            {
                Business.Instance.SysEventAdd($"ModBus地址【{_myAddress}】解析失败", plcMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.OnMessage($"ModBus地址【{_myAddress}】解析失败：{plcMsg}", BatMes.Client.Enums.SysEventLevel.Error);
            }
            return addressRes;
        }

        /// <summary>
        /// 初始化地址,并建立映射关系
        /// </summary>
        /// <param name="startAt"></param>
        /// <param name="addMap"></param>
        private void InitializeAddR(string startAt, Dictionary<int, BatMes.Device.PLC.IAddress> addMap, bool isDt = false)
        {
            var trunPoint = startAt;
            for(int i = 0; i < Configs.resquestLen; i++)
            {
                if(i >= Configs.turningPoint && trunPoint == startAt && !isDt)
                {
                    var endChar = startAt[startAt.Length - 1].ToString().ToInt32() + 1;
                    if(endChar > 9)
                        startAt = startAt.Substring(0, startAt.Length - 2) + endChar.ToString();
                    else
                        startAt = startAt.Substring(0, startAt.Length - 1) + endChar.ToString();
                }
                string addValue;
                if (!isDt)
                    addValue = (i >= Configs.turningPoint ? i - Configs.turningPoint : i).ToString("X");
                else
                {
                    var lastTwo = startAt.Substring(startAt.Length - 2);
                    addValue = startAt.Substring(0, startAt.Length - 2) + (lastTwo.ToInt32() + i).ToString().PadLeft(2, '0');
                    //addValue = startAt.Replace(lastTwo, (lastTwo.ToInt32() + i).ToString());
                }
                var insValue = isDt ? addValue : startAt + addValue;
                addMap.Add(i + 1, MyAddressAnalysis(insValue));
            }
        }

        /// <summary>
        /// 初始化烟雾报警
        /// </summary>
        /// <param name="startAddR"></param>
        /// <param name="addMap"></param>
        private void InitSmokeAddR(string startAddR, Dictionary<int, BatMes.Device.PLC.IAddress> addMap)
        {
            string nowAddR = startAddR;
            string startAt= nowAddR.Substring(0, 2);
            for(int i = 0; i < Configs.smokeCount; i++)
            {
                addMap[i + 1] = MyAddressAnalysis(nowAddR);
                var subAddR = nowAddR.Substring(nowAddR.Length - 3).ToInt32();
                var addVal = (subAddR + 20).ToString().PadLeft(3, '0');
                nowAddR = startAt + addVal;
            }
        }

        /// <summary>
        /// 初始化喷淋信号点
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)> InitSprayAddR()
        {
            var opRes = new Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)>();
            var addCount = 1;
            for(int i = 1; i <= Configs.sprayCount; i++)
            {
                var addRStr = Configs.doSpray + i.ToString();
                var addR = MyAddressAnalysis(addRStr);
                for(ushort j = 1; j <= 4; j++)
                {
                    opRes[addCount] = (addR, j);
                    addCount++;
                }
            }
            return opRes;
        }
        #endregion

        #region 定义变量
        /// <summary>
        /// 信号操作类型映射
        /// </summary>
        private Dictionary<SignalType, Dictionary<int, BatMes.Device.PLC.IAddress>> OperationMap { get; } = new Dictionary<SignalType, Dictionary<int, BatMes.Device.PLC.IAddress>>();

        /// <summary>
        /// 批量读映射
        /// </summary>
        private Dictionary<SignalType, BatMes.Device.PLC.IAddress> BulkReadMap { get; } = new Dictionary<SignalType, BatMes.Device.PLC.IAddress>();

        /// <summary>
        /// 松下MEWTOCOL-COM协议（相关的PLC通信说明书有附件）		
        /// </summary>
        private BatMes.Device.PLC.IPlc _plc;

        /// <summary>
        /// 喷淋PLC
        /// </summary>
        private BatMes.Device.PLC.IPlc _plcSpray;

        #region 写映射
        /// <summary>
        /// 调度完成信号 TO PLC。 key => 编号（层数/库位编号） value => 地址
        /// </summary>
        private Dictionary<int, BatMes.Device.PLC.IAddress> InFinishMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();

        /// <summary>
        /// 入盘规格
        /// </summary>
        private Dictionary<int, BatMes.Device.PLC.IAddress> TypeMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();

        /// <summary>
        /// 故障复位
        /// </summary>
        private Dictionary<int, BatMes.Device.PLC.IAddress> ResetMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();

        /// <summary>
        /// 出盘完成
        /// </summary>
        private Dictionary<int, BatMes.Device.PLC.IAddress> OutFinshMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();

        /// <summary>
        /// 消防允许确认信号
        /// </summary>
        private Dictionary<int, BatMes.Device.PLC.IAddress> FireConfirmMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
        #endregion

        #region 批量读（批量读PLC地址用到）
        /// <summary>
        /// 请求压合开始信号
        /// </summary>
        private BatMes.Device.PLC.IAddress _requestPress;

        /// <summary>
        /// 正常运行开始信号
        /// </summary>
        private BatMes.Device.PLC.IAddress _runningFlag;

        /// <summary>
        /// 故障开始信号
        /// </summary>
        private BatMes.Device.PLC.IAddress _faultFlag;

        /// <summary>
        /// 请求取盘开始信号
        /// </summary>
        private BatMes.Device.PLC.IAddress _requestOut;

        /// <summary>
        /// 火警消防启动允许开始信号
        /// </summary>
        private BatMes.Device.PLC.IAddress _openFire;
        #endregion

        #region 单个地址读映射(单个地址读用到)
        private Dictionary<int, BatMes.Device.PLC.IAddress> RequestPressMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
        private Dictionary<int, BatMes.Device.PLC.IAddress> RunningFlagMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
        private Dictionary<int, BatMes.Device.PLC.IAddress> FaultFlagMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
        private Dictionary<int, BatMes.Device.PLC.IAddress> RequestOutMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
        private Dictionary<int, BatMes.Device.PLC.IAddress> OpenFireMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
        private Dictionary<int, BatMes.Device.PLC.IAddress> SmokeMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
        private Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)> SprayMap;
        #endregion

        #endregion

        #region 读写PLC

        /// <summary>
        /// 公共写入
        /// </summary>
        /// <param name="signalType"></param>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public bool WriteBool(SignalType signalType, int cellID, bool writeValue = true)
        {
            var hasAddR = GetAddressByMap(signalType, cellID);
            if (hasAddR == null)
                return false;
            var opRes = default(BatMes.Device.OperateResult);
            for(int i = 0; i < 3; i++)
            {
                opRes = _plc.WriteBool(hasAddR, writeValue);
                if (opRes?.Code == BatMes.Device.ResultCode.Success)
                    break;
            }
            if(opRes?.Code != BatMes.Device.ResultCode.Success)
            {
                var showMsg = $"PLC地址【{hasAddR.AddressAscii}】写入失败：{opRes?.Message ?? "返回值为空"}。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
            return opRes != null && opRes.Code == BatMes.Device.ResultCode.Success;
        }

        /// <summary>
        /// 等待返回指定值
        /// </summary>
        /// <param name="signalType"></param>
        /// <param name="cellID"></param>
        /// <param name="writeValue"></param>
        /// <returns></returns>
        public bool WaitBool(SignalType signalType, int cellID, bool writeValue = true)
        {
            var hasMap = OperationMap.ContainsKey(signalType) ? OperationMap[signalType] : null;
            var hasAddR = hasMap != null ? (hasMap.ContainsKey(cellID) ? hasMap[cellID] : null) : null;
            if (hasAddR == null)
            {
                var showMsg = $"找不到库位ID【{cellID}】对应的PLC地址，读取不会成功。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.AddSysEventLog("找不到PLC地址", showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
            var opRes = _plc.WaitBool(hasAddR, writeValue, 10, 500);
            if (opRes?.Code != BatMes.Device.ResultCode.Success)
                return false;
            return opRes.Data;
        }

        /// <summary>
        /// 公共写入
        /// </summary>
        /// <param name="signalType"></param>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public bool WriteUShort(SignalType signalType, int cellID, ushort writeValue)
        {
            var hasAddR = GetAddressByMap(signalType, cellID);
            if (hasAddR == null)
                return false;
            var opRes = default(BatMes.Device.OperateResult);
            for (int i = 0; i < 3; i++)
            {
                opRes = _plc.WriteUShort(hasAddR, writeValue);
                if (opRes?.Code == BatMes.Device.ResultCode.Success)
                    break;
            }
            if (opRes?.Code != BatMes.Device.ResultCode.Success)
            {
                var showMsg = $"PLC地址【{hasAddR.AddressAscii}】写入失败：{opRes?.Message ?? "返回值为空"}。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
            return opRes != null && opRes.Code == BatMes.Device.ResultCode.Success;
        }

        /// <summary>
        /// 公共读（批量）
        /// </summary>
        /// <param name="signalType"></param>
        /// <returns></returns>
        public byte[] CommonReadByBulk(SignalType signalType)
        {
            var hasAddR = BulkReadMap.ContainsKey(signalType) ? BulkReadMap[signalType] : null;
            if (hasAddR == null)
            {
                var showMsg = $"批量读取PLC地址失败：{signalType}。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.AddSysEventLog(showMsg, showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
            var opRes = _plc.Read(hasAddR, (ushort)Configs.resquestLen);
            if (opRes?.Code != BatMes.Device.ResultCode.Success)
            {
                var showMsg = $"PLC地址【{hasAddR.AddressAscii}】读取失败：{opRes?.Message ?? "返回值为空"}。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.AddSysEventLog("PLC地址读取失败", showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
            return opRes.Data;
        }

        /// <summary>
        /// 单个读
        /// </summary>
        /// <param name="signalType"></param>
        /// <returns></returns>
        public bool CommonReadByOne(SignalType signalType, int cellID)
        {
            var hasMap = OperationMap.ContainsKey(signalType) ? OperationMap[signalType] : null;
            var hasAddR = hasMap != null ? (hasMap.ContainsKey(cellID) ? hasMap[cellID] : null) : null;
            if (hasAddR == null)
            {
                var showMsg = $"找不到库位ID【{cellID}】对应的PLC地址，读取不会成功。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.AddSysEventLog("找不到PLC地址", showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
            var opRes = _plc.ReadBool(hasAddR);
            if (opRes?.Code != BatMes.Device.ResultCode.Success)
            {
                var showMsg = $"PLC地址【{hasAddR.AddressAscii}】读取失败：{opRes?.Message ?? "返回值为空"}。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.AddSysEventLog("PLC地址读取失败", showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
            return opRes.Data;
        }

        /// <summary>
        /// 判断是否存在烟雾报警
        /// </summary>
        /// <param name="hasCell"></param>
        /// <returns></returns>
        public Dictionary<int, bool> HasSmoke(int? hasCell = null)
        {
            var opVal = new Dictionary<int, bool>();
            if(hasCell != null)
            {
                if (!SmokeMap.ContainsKey(hasCell.Value))
                    return opVal;
                var readOne = _plc?.ReadBool(SmokeMap[hasCell.Value]);
                if (readOne?.Code == BatMes.Device.ResultCode.Success && readOne.Data == true)
                    opVal[hasCell.Value] = readOne.Data;
            }
            else
            {
                SmokeMap.ToList().ForEach(t =>
                {
                    var readOne = _plc?.ReadBool(t.Value);
                    if (readOne?.Code == BatMes.Device.ResultCode.Success && readOne.Data == true)
                        opVal[t.Key] = readOne.Data;
                });
            }
            return opVal;
        }

        /// <summary>
        /// 喷淋
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public bool NoticeSpray(int cellID)
        {
            if (!SprayMap.ContainsKey(cellID))
            {
                var showMsg = $"找不到库位ID【{cellID}】对应的喷淋PLC地址，读取不会成功。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.AddSysEventLog("找不到喷淋PLC地址", showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
            var (AddR, writeVal) = SprayMap[cellID];
            var opVal = default(BatMes.Device.OperateResult);
            for (int i = 0; i < 3; i++)
            {
                opVal = _plcSpray?.WriteUShort(AddR, writeVal);
                if (opVal?.Code == BatMes.Device.ResultCode.Success)
                    break;
            }
            if (opVal?.Code != BatMes.Device.ResultCode.Success)
            {
                var showMsg = $"PLC地址【{AddR.AddressAscii}】读取失败：{opVal?.Message ?? "返回值为空"}。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.AddSysEventLog("PLC地址写入失败", showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region 读写ModBus
        /// <summary>
        /// 温度的ModBus
        /// </summary>
        private BatMes.Device.PLC.IPlc _plcByModBus;

        /// <summary>
        /// 读
        /// </summary>
        private BatMes.Device.PLC.IAddress _readModBus;

        /// <summary>
        /// 批量获取温度信号
        /// </summary>
        /// <returns></returns>
        public byte[] BulkGetByModBus()
        {
            var byteRes = _plcByModBus.Read(_readModBus, (ushort)Configs.addrCountByBus);
            if (byteRes?.Code == BatMes.Device.ResultCode.Failure)
            {
                CORE.Instance.OnMessage($"批量读取ModBus信号失败:{byteRes?.Message ?? "返回值为空"}", BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
            return byteRes.Data;
        }
        #endregion

        #region 公用
        /// <summary>
        /// 获取设备的连接状态
        /// </summary>
        /// <returns></returns>
        public Dictionary<DeviceType, ConnetStatus> GetDeviceStatusByInit()
        {
            var resultStatus = new Dictionary<DeviceType, ConnetStatus>();
            var plcIsOpen = this._plc.IsOpen;
            resultStatus.Add(DeviceType.PLC, new ConnetStatus
            {
                IsOpen = plcIsOpen,
                Message = plcIsOpen ? "压床PLC已连接" : "压床PLC未连接"
            });

            if (Configs.IsOpenSmoke)
            {
                var sprayIsOpen = this._plcSpray?.IsOpen;
                resultStatus.Add(DeviceType.Other, new ConnetStatus
                {
                    IsOpen = sprayIsOpen ?? false,
                    Message = (sprayIsOpen ?? false) ? "喷淋PLC已连接" : "喷淋PLC未连接"
                });
            }

            if (Configs.IsOpenMBus)
            {
                var busIsOpen = this._plcByModBus?.IsOpen;
                resultStatus.Add(DeviceType.ModBus, new ConnetStatus
                {
                    IsOpen = busIsOpen ?? false,
                    Message = (busIsOpen ?? false) ? "感温光纤已连接" : "感温光纤未连接"
                });
            }
            return resultStatus;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public async Task CloseDevice()
        {
            await Task.Run(() =>
            {
                this._plc?.Close();
                this._plcSpray?.Close();
                this._plcByModBus?.Close();
            });
        }

        /// <summary>
        /// 开启设备
        /// </summary>
        public async Task OpenDevice()
        {
            await Task.Run(() => 
            {
                this._plc?.Open();
                this._plcSpray?.Open();
                this._plcByModBus?.Open();
            });
        }

        /// <summary>
        /// 通过映射获取地址
        /// </summary>
        /// <param name="signalType"></param>
        /// <param name="cellID"></param>
        /// <returns></returns>
        private BatMes.Device.PLC.IAddress GetAddressByMap(SignalType signalType, int cellID)
        {
            var hasMap = OperationMap.ContainsKey(signalType) ? OperationMap[signalType] : null;
            var hasAddR = hasMap != null ? (hasMap.ContainsKey(cellID) ? hasMap[cellID] : null) : null;
            if (hasAddR == null)
            {
                var showMsg = $"找不到库位ID【{cellID}】对应的PLC地址，读取不会成功。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                CORE.Instance.AddSysEventLog("找不到PLC地址", showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return default;
            }
            return hasAddR;
        }
        #endregion
    }
}