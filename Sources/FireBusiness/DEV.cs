using BatMes.Client;
using BatMes.Client.Entity.batmes_client;
using FireBusiness.Enums;
using FireBusiness.Model;
using FireBusiness.OutSystem;
using NLog.Filters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace FireBusiness.Dev
{
    /// <summary>
    /// 设备对接类
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
                CORE.Instance.OnMessage("初始化完成，正在连接PLC...", BatMes.Client.Enums.SysEventLevel.Info);
                KvAddMap = KvAddMap ?? new ConcurrentDictionary<FirePost, DevAddMap>();

                //获取库位表及库位配置详细信息
                var devMap = Business.Instance.GetCellsAsync()?.Result;

                #region 实例PLC
                var ipAddrForFcStandby = Business.Instance.SysPara<string>("IpAddrForFcStandby");
                var ipPortForFcStandby = Business.Instance.SysPara<string>("IpPortForFcStandby");
                var fcStandbyPlc = GetPlcInsByPorts(ipAddrForFcStandby, ipPortForFcStandby);

                var ipAddrForFc = Business.Instance.SysPara<string>("IpAddrForFc");
                var ipPortForFc = Business.Instance.SysPara<string>("IpPortForFc");
                var fcPlc = GetPlcInsByPorts(ipAddrForFc, ipPortForFc);

                var ipAddrForHotStandby = Business.Instance.SysPara<string>("IpAddrForHotStandby");
                var ipPortForHotStandby = Business.Instance.SysPara<string>("IpPortForHotStandby");
                var hotStandbyPlc = GetPlcInsByPorts(ipAddrForHotStandby, ipPortForHotStandby);

                var fireIpAddrModbus = Business.Instance.SysPara<string>("FireIpAddrModbus");
                var firePortAddrModbus = Business.Instance.SysPara<string>("FirePortAddrModbus");
                var modbusPlc = GetPlcInsByPorts(fireIpAddrModbus, firePortAddrModbus, true);
                #endregion

                #region 建立常温静置映射
                var fcSCells = devMap.Where(t => t.type == (int)FirePost.FcStandby);
                //var fcSPointMap = fcSCells.DistinctBy(t => t.extend_field3).ToDictionary(k => k.extend_field3.ToInt32(), v => v.cell_id);
                var fcStandbyMap = new DevAddMap
                {
                    ReadPlc = fcStandbyPlc,
                    ActionPlc = fcStandbyPlc.FirstOrDefault()
                };
                fcStandbyMap.ReadMap.Add(SignalType.R_FcStandby, InitSmokeByFcStandby(FirePost.FcStandby, fcSCells));
                fcStandbyMap.ActionMap.Add(FireAction.Spray, InitSprayByStandby(FirePost.FcStandby, fcSCells));
                fcStandbyMap.OneMap.Add(SignalType.W_FcStandbyBit, (MyAddressAnalysis("R5000"), true));
                fcStandbyMap.OneMap.Add(SignalType.W_Speaker, (MyAddressAnalysis("R5001"), true));
                fcStandbyMap.FireRclAddR = (MyAddressAnalysis("D10080"), MyAddressAnalysis("D10081"), MyAddressAnalysis("D10082"));
                fcStandbyMap.DoRclAddR = (MyAddressAnalysis("D10083"), MyAddressAnalysis("D10084"), MyAddressAnalysis("D10085"));
                KvAddMap.TryAdd(FirePost.FcStandby, fcStandbyMap);
                #endregion

                #region 建立分容压床映射
                var fcCells = devMap.Where(t => t.type == (int)FirePost.Fc);
                var fcPointMap = fcCells.DistinctBy(t => t.extend_field3).ToDictionary(k => k.extend_field3.ToInt32(), v => v.cell_id);
                var fcMap = new DevAddMap
                {
                    ReadPlc = fcPlc,
                    ActionPlc = fcStandbyPlc.FirstOrDefault() //注意：分容喷淋的PLC是由常温静置架PLC控制
                };

                fcMap.DiyMap.TryAdd(SignalType.W_NotifyFcSpray, InitNotifySprayByFc(fcCells));
                fcMap.DiyMap.TryAdd(SignalType.W_DoBrakeUp, InitDoBrakeUp(fcCells));
                //fcMap.ReadMap.Add(SignalType.W_ConfirmFire, InitializeAddR(Configs.ConfirmFireFlag, fcPointMap));
                //fcMap.ReadMap.Add(SignalType.OR_OpenFire, InitializeAddR(Configs.OpenFireFlag.Substring(0, Configs.OpenFireFlag.Length - 1), fcPointMap));

                fcMap.ReadMap.Add(SignalType.R_Fc, InitSmokeAddR(Configs.SmokeStartAt, fcPointMap));
                fcMap.ReadMapBulk.Add(SignalType.R_Fc_Temp, InitFcTemp(fcPointMap));
                fcMap.OneMap.Add(SignalType.W_Speaker, (MyAddressAnalysis("R5001"), true));
                fcMap.ActionMap.Add(FireAction.Spray, InitSprayByFc(fcPointMap, Configs.FcSpraryIsSanme));
                KvAddMap.TryAdd(FirePost.Fc, fcMap);
                #endregion

                #region 建立高温静置映射
                var fcHCells = devMap.Where(t => t.type == (int)FirePost.HotStandby);
                //var fcHPointMap = fcHCells.DistinctBy(t => t.extend_field3).ToDictionary(k => k.extend_field3.ToInt32(), v => v.cell_id);
                var hotMap = new DevAddMap
                {
                    ReadPlc = hotStandbyPlc,
                    ActionPlc = hotStandbyPlc.FirstOrDefault()
                };
                hotMap.ReadMapBulk.Add(SignalType.R_HotStandby, InitSmokeByHotStandby(FirePost.HotStandby, fcHCells));
                hotMap.ActionMap.Add(FireAction.Spray, InitSprayByStandby(FirePost.HotStandby, fcHCells));
                hotMap.OneMap.Add(SignalType.W_HotBit, (MyAddressAnalysis("R5000"), true));
                hotMap.OneMap.Add(SignalType.W_Speaker, (MyAddressAnalysis("R5001"), true));
                hotMap.FireRclAddR = (MyAddressAnalysis("D10080"), MyAddressAnalysis("D10081"), MyAddressAnalysis("D10082"));
                hotMap.DoRclAddR = (MyAddressAnalysis("D10083"), MyAddressAnalysis("D10084"), MyAddressAnalysis("D10085"));
                KvAddMap.TryAdd(FirePost.HotStandby, hotMap);
                #endregion

                #region 建立Modbus置映射
                var insMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
                var modbusMap = devMap.Where(t => Configs.ModbusRange.Contains(t.type));
                modbusMap.ForEach(t =>
                {
                    insMap[t.cell_id] = MyAddressAnalysis(Configs.SlaveAddR, "InputRegister" + t.extend_field1.PadLeft(4, '0'));
                });
                var busMap = new DevAddMap
                {
                    ReadPlc = modbusPlc,
                    ActionPlc = modbusPlc.FirstOrDefault()
                };
                busMap.ReadMap.Add(SignalType.R_Modbus, insMap);
                //busMap.DiyMap = GetModbusDiy(modbusMap); 
                KvAddMap.TryAdd(FirePost.ModBus, busMap);

                ModbusHasWarning = ModbusHasWarning ?? new List<(int parkVal, FirePost postVal, BatMes.Device.PLC.Modbus.AddressModbus addR)>();
                ModbusHasWarning.Add((1, FirePost.FcStandby, MyAddressAnalysis(Configs.SlaveAddR, "InputRegister0003")));
                ModbusHasWarning.Add((2, FirePost.FcStandby, MyAddressAnalysis(Configs.SlaveAddR, "InputRegister0004")));
                ModbusHasWarning.Add((3, FirePost.HotStandby, MyAddressAnalysis(Configs.SlaveAddR, "InputRegister0005")));
                ModbusHasWarning.Add((4, FirePost.HotStandby, MyAddressAnalysis(Configs.SlaveAddR, "InputRegister0006")));
                #endregion
            }
            catch (Exception err)
            {
                LogManager.Error($"解析地址发生错误：{err.ToString()}");
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
        /// 根据多个端口创建多个PLC实例
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <param name="ports"></param>
        /// <returns></returns>
        private List<BatMes.Device.PLC.IPlc> GetPlcInsByPorts(string ipAddr, string ports, bool isModbus = false)
        {
            var opVals = new List<BatMes.Device.PLC.IPlc>();
            var idIndex = 1;
            ports.Split(',').Where(t => !t.IsEmpty()).ForEach(port =>
            {
                if (isModbus)
                {
                    var newVal = new BatMes.Device.PLC.Modbus.PlcNet(ipAddr, port.ToInt32());
                    newVal.ID += $" && {idIndex}";
                    opVals.Add(newVal);
                }
                else
                {
                    var newVal = new BatMes.Device.PLC.Panasonic.Mewtocol.PlcNet(ipAddr, port.ToInt32());
                    newVal.ID += $" && {idIndex}";
                    opVals.Add(newVal);
                }
                idIndex++;
            });
            return opVals;
        }

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
        private Dictionary<int, BatMes.Device.PLC.IAddress> InitializeAddR(string startAt, Dictionary<int, int> cellMap, bool isDt = false)
        {
            var addMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            var trunPoint = startAt;
            for(int i = 0; i < Configs.ResquestLen; i++)
            {
                if(i >= Configs.TurningPoint && trunPoint == startAt && !isDt)
                {
                    var endChar = startAt[startAt.Length - 1].ToString().ToInt32() + 1;
                    if(endChar > 9)
                        startAt = startAt.Substring(0, startAt.Length - 2) + endChar.ToString();
                    else
                        startAt = startAt.Substring(0, startAt.Length - 1) + endChar.ToString();
                }
                string addValue;
                if (!isDt)
                    addValue = (i >= Configs.TurningPoint ? i - Configs.TurningPoint : i).ToString("X");
                else
                {
                    var lastTwo = startAt.Substring(startAt.Length - 2);
                    addValue = startAt.Substring(0, startAt.Length - 2) + (lastTwo.ToInt32() + i).ToString().PadLeft(2, '0');
                }
                var insValue = isDt ? addValue : startAt + addValue;
                var mesId = cellMap.ContainsKey(i + 1) ? cellMap[i + 1] : default;
                if(mesId > 0)
                    addMap[mesId] = MyAddressAnalysis(insValue);
            }
            return addMap;
        }

        /// <summary>
        /// 初始化烟雾报警
        /// </summary>
        /// <param name="startAddR"></param>
        /// <param name="addMap"></param>
        private Dictionary<int, BatMes.Device.PLC.IAddress> InitSmokeAddR(string startAddR, Dictionary<int, int> cellMap)
        {
            var addMap = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            string nowAddR = startAddR;
            string startAt= nowAddR.Substring(0, 2);
            for(int i = 0; i < Configs.SmokeCount; i++)
            {
                var mesId = cellMap.ContainsKey(i + 1) ? cellMap[i + 1] : default;
                if (mesId > 0)
                    addMap[mesId] = MyAddressAnalysis(nowAddR);
                var subAddR = nowAddR.Substring(nowAddR.Length - 3).ToInt32();
                var addVal = (subAddR + 20).ToString().PadLeft(3, '0');
                nowAddR = startAt + addVal;
            }
            return addMap;
        }

        /// <summary>
        /// 初始化喷淋信号点: 适用于分容压床
        /// 分容压床喷淋是由静置架PLC控制，和分容压床的PLC读取顺序相反，在这里做特殊处理
        /// </summary>
        /// <param name="cellMap">库位映射关系</param>
        /// <param name="isRev">和压床PLC顺序相反吗</param>
        /// <returns></returns>
        private Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)> InitSprayByFc(Dictionary<int, int> cellMap, bool isSame = true)
        {
            var opRes = new Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)>();
            if (!isSame)
            {
                var addCount = 28;
                for (int i = 1; i <= Configs.SprayCount; i++)
                {
                    var addRStr = Configs.DoSpray + i.ToString();
                    var addR = MyAddressAnalysis(addRStr);
                    for (ushort j = 4; j >= 1; j--)
                    {
                        var mesId = cellMap.ContainsKey(addCount) ? cellMap[addCount] : default;
                        if (mesId > 0)
                            opRes[mesId] = (addR, j);
                        //opRes[addCount] = (addR, j);
                        addCount--;
                    }
                }
            }
            else
            {
                var addCount = 1;
                for (int i = 1; i <= Configs.SprayCount; i++)
                {
                    var addRStr = Configs.DoSpray + i.ToString();
                    var addR = MyAddressAnalysis(addRStr);
                    for (ushort j = 1; j <= 4; j++)
                    {
                        var mesId = cellMap.ContainsKey(addCount) ? cellMap[addCount] : default;
                        if (mesId > 0)
                            opRes[mesId] = (addR, j);
                        //opRes[addCount] = (addR, j);
                        addCount++;
                    }
                }
            }
            return opRes;
        }

        /// <summary>
        /// 初始化通知压床断电信号点
        /// </summary>
        /// <param name="cellMap"></param>
        /// <returns></returns>
        private Dictionary<int, BatMes.Device.PLC.IAddress> InitNotifySprayByFc(IEnumerable<cell> cellMap)
        {
            var opRes = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            var index = 1;
            var addIndex = 1;
            cellMap.OrderBy(c => c.col).ThenBy(l => l.lay).ForEach(t =>
            {
                var addRStr = Configs.DoBlackout + addIndex.ToString();
                var addR = MyAddressAnalysis(addRStr);
                opRes[t.cell_id] = addR;
                index++;
                if (index > 4)
                {
                    index = 1;
                    addIndex++;
                }
            });
            return opRes;
        }

        /// <summary>
        /// 初始化弹开压床信号点
        /// </summary>
        /// <param name="cellMap"></param>
        /// <returns></returns>
        private Dictionary<int, BatMes.Device.PLC.IAddress> InitDoBrakeUp(IEnumerable<cell> cellMap)
        {
            var opRes = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            var index = 0;
            var addindex = 4;
            var indexAddR = Configs.OpenFireFlag.Substring(0, Configs.OpenFireFlag.Length - 2);
            cellMap.OrderBy(c => c.col).ThenBy(l => l.lay).ForEach(t =>
            {
                var addRStr = indexAddR + addindex.ToString() + index.ToString("X");
                var addR = MyAddressAnalysis(addRStr);
                opRes[t.cell_id] = addR;
                index++;
                if (index >= 16)
                {
                    index = 0;
                    addindex++;
                }
            });
            return opRes;
        }

        /// <summary>
        /// 初始化喷淋信号点: 适用于静置
        /// </summary>
        /// <param name="firePost"></param>
        /// <param name="cellMap"></param>
        /// <returns></returns>
        private Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)> InitSprayByStandby(FirePost firePost,  IEnumerable<cell> cellMap)
        {
            var opRes = new Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)>();
            var (colVal, layVal, startAdd) = Configs.SprayLenMap.ContainsKey((firePost, 1)) ? Configs.SprayLenMap[(firePost, 1)] : (default, default, default);
            if (!startAdd.IsEmpty())
                startAdd = startAdd.Substring(0, startAdd.Length - 2);
            var addVal = firePost == FirePost.HotStandby ? 1 : 0;
            for (int len = 1; len <= colVal; len++)
            {
                var addRStr = startAdd + (len - addVal).ToString().PadLeft(2, '0');
                cellMap.Where(t => t.row == 1 && t.col == len).ForEach(t =>
                {
                    var wVal = (firePost == FirePost.FcStandby || t.lay == null) ? 1 : (t.lay <= 3 ? 1 : 2);
                    opRes[t.cell_id] = (MyAddressAnalysis(addRStr), (ushort)wVal);
                });
            }

            var (colVal2, layVal2, startAdd2) = Configs.SprayLenMap.ContainsKey((firePost, 2)) ? Configs.SprayLenMap[(firePost, 2)] : (default, default, default);
            if (!startAdd2.IsEmpty())
                startAdd2 = startAdd2.Substring(0, startAdd2.Length - 2);
            for (int len = 1; len <= colVal2; len++)
            {
                var addRStr = startAdd2 + len.ToString().PadLeft(2, '0');
                cellMap.Where(t => t.row == 2 && t.col == len).ForEach(t =>
                {
                    var wVal = (firePost == FirePost.FcStandby || t.lay == null) ? 1 : (t.lay <= 3 ? 1 : 2);
                    opRes[t.cell_id] = (MyAddressAnalysis(addRStr), (ushort)wVal);
                });
            }
            return opRes;
        }

        /// <summary>
        /// 初始化静置烟雾感应信号点 (适用常温静置)
        /// </summary>
        /// <param name="firePost"></param>
        /// <param name="cellMap"></param>
        /// <returns></returns>
        private Dictionary<int, BatMes.Device.PLC.IAddress> InitSmokeByFcStandby(FirePost firePost, IEnumerable<cell> cellMap)
        {
            var opVal = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            var (colVal, layVal, startAdd) = Configs.SmokeLenMap.ContainsKey((firePost, 1)) ? Configs.SmokeLenMap[(firePost, 1)] : (default, default, default);
            if (!startAdd.IsEmpty())
                startAdd = startAdd.Substring(0, startAdd.Length - 3);
            for (int len = 1; len <= colVal; len++)
            {
                var addRStr = startAdd + (len * 1).ToString().PadLeft(2, '0');
                cellMap.Where(t => t.row == 1 && t.col == len).ForEach(t =>
                {
                    if (t.lay == null)
                        return;
                    var addLay = (t.lay.Value - 1).ToString(); ;
                    opVal[t.cell_id] = MyAddressAnalysis(addRStr + addLay);
                });
            }

            //初始化第二行
            var (colVal2, layVal2, startAdd2) = Configs.SmokeLenMap.ContainsKey((firePost, 2)) ? Configs.SmokeLenMap[(firePost, 2)] : (default, default, default);
            if (!startAdd2.IsEmpty())
                startAdd2 = startAdd2.Substring(0, startAdd2.Length - 3);
            for (int len = 1; len <= colVal2; len++)
            {
                var addRStr = startAdd2 + (len * 1).ToString().PadLeft(2, '0');
                cellMap.Where(t => t.row == 2 && t.col == len).ForEach(t =>
                {
                    if (t.lay == null)
                        return;
                    var addLay = (t.lay.Value - 1).ToString(); 
                    //常温静置架的第二行第三列、第四列有特殊情况，1-3层是没有的，在此做特殊处理
                    if((t.col == 3 || t.col == 4) && t.lay > 3)
                    {
                        addLay = (t.lay.Value - 4).ToString();
                    }
                    opVal[t.cell_id] = MyAddressAnalysis(addRStr + addLay);
                });
            }
            return opVal;
        }

        /// <summary>
        /// 初始化静置烟雾感应信号点 (适用高温静置) 一对多
        /// </summary>
        /// <param name="firePost"></param>
        /// <param name="cellMap"></param>
        /// <returns></returns>
        private Dictionary<int, List<BatMes.Device.PLC.IAddress>> InitSmokeByHotStandby(FirePost firePost, IEnumerable<cell> cellMap)
        {
            var opVal = new Dictionary<int, List<BatMes.Device.PLC.IAddress>>();
            var (colVal, layVal, startAdd) = Configs.SmokeLenMap.ContainsKey((firePost, 1)) ? Configs.SmokeLenMap[(firePost, 1)] : (default, default, default);
            if (!startAdd.IsEmpty())
                startAdd = startAdd.Substring(0, startAdd.Length - 3);
            var addVal = firePost == FirePost.HotStandby ? 1 : 0;
            var myLen = 1;
            for (int len = 2; len <= colVal; len++)
            {
                var lineOneCount = 0;
                var lineTwoCount = 0;
                cellMap.Where(t => t.row == 1 && t.col == len).ForEach(t =>
                {
                    if (t.lay == null || t.lay == 0)
                        return;
                    var addLay = (t.lay.Value - 1).ToString();
                    if (t.lay.Value <= 4)
                    {
                        var addRStr = startAdd + myLen.ToString().PadLeft(2, '0');
                        var addList = new List<BatMes.Device.PLC.IAddress>
                        {
                            MyAddressAnalysis(addRStr + lineOneCount.ToString()),
                            MyAddressAnalysis(addRStr + (lineOneCount + 1).ToString())
                         };
                        lineOneCount += 2;
                        opVal[t.cell_id] = addList;
                    }
                    else if (t.lay > 4)
                    {
                        var addRStr = startAdd + (myLen + 1).ToString().PadLeft(2, '0');
                        var addList = new List<BatMes.Device.PLC.IAddress>
                        {
                            MyAddressAnalysis(addRStr + lineTwoCount.ToString()),
                            MyAddressAnalysis(addRStr + (lineTwoCount + 1).ToString())
                         };
                        lineTwoCount += 2; 
                        opVal[t.cell_id] = addList;
                    }
                });
                myLen += 2;
            }

            //初始化第二行
            var (colVal2, layVal2, startAdd2) = Configs.SmokeLenMap.ContainsKey((firePost, 2)) ? Configs.SmokeLenMap[(firePost, 2)] : (default, default, default);
            if (!startAdd2.IsEmpty())
                startAdd2 = startAdd2.Substring(0, startAdd2.Length - 3);
            myLen = 1;
            for (int len = 1; len <= colVal2; len++)
            {
                var lineOneCount = 0;
                var lineTwoCount = 0;
                cellMap.Where(t => t.row == 2 && t.col == len).ForEach(t =>
                {
                    if (t.lay == null || t.lay == 0)
                        return;
                    var addLay = (t.lay.Value - 1).ToString();
                    if (t.lay.Value <= 4)
                    {
                        var addRStr = startAdd2 + myLen.ToString().PadLeft(2, '0');
                        var addList = new List<BatMes.Device.PLC.IAddress>
                        {
                            MyAddressAnalysis(addRStr + lineOneCount.ToString()),
                            MyAddressAnalysis(addRStr + (lineOneCount + 1).ToString())
                         };
                        lineOneCount += 2;
                        opVal[t.cell_id] = addList;
                    }
                    else if (t.lay > 4)
                    {
                        var addRStr = startAdd2 + (myLen + 1).ToString().PadLeft(2, '0');
                        var addList = new List<BatMes.Device.PLC.IAddress>
                        {
                            MyAddressAnalysis(addRStr + lineTwoCount.ToString()),
                            MyAddressAnalysis(addRStr + (lineTwoCount + 1).ToString())
                         };
                        lineTwoCount += 2;
                        opVal[t.cell_id] = addList;
                    }
                });
                myLen += 2;
            }
            return opVal;
        }

        /// <summary>
        /// 初始化分容压床温度
        /// </summary>
        /// <param name="cellMap"></param>
        /// <returns></returns>
        private Dictionary<int, List<BatMes.Device.PLC.IAddress>> InitFcTemp(Dictionary<int, int> cellMap)
        {
            var opVal = new Dictionary<int, List<BatMes.Device.PLC.IAddress>>();
            var (colVal, layVal, startAddR) = Configs.SmokeLenMap.ContainsKey((FirePost.Fc, 1)) ? Configs.SmokeLenMap[(FirePost.Fc, 1)] : (default, default, default);
            var addCount = 1;
            var sddR = startAddR.Substring(0, 2);
            for (int i = 1; i <= colVal; i++)
            {
                var colAddR = sddR + i.ToString().PadLeft(2, '0');
                var addList = new List<BatMes.Device.PLC.IAddress>();
                for (int j = 1; j <= layVal; j++)
                {
                    var addAnalysis = MyAddressAnalysis(colAddR + j.ToString().PadLeft(2, '0'));
                    addList.Add(addAnalysis);
                }
                var mesId = cellMap.ContainsKey(addCount) ? cellMap[addCount] : default;
                if (mesId > 0)
                    opVal[mesId] = addList;
                addCount++;
            }
            return opVal;
        }

        /// <summary>
        /// 通过静置架库位获取Modbus信号映射
        /// </summary>
        /// <param name="mapCell"></param>
        /// <returns></returns>
        private ConcurrentDictionary<SignalType, Dictionary<int, BatMes.Device.PLC.IAddress>> GetModbusDiy(IEnumerable<cell> mapCell)
        {
            var opVal = new ConcurrentDictionary<SignalType, Dictionary<int, BatMes.Device.PLC.IAddress>>();

            var packVal_1 = mapCell?.Where(t => t.type == (int)FirePost.FcStandby && t.row == 2);
            var packVal_2 = mapCell?.Where(t => t.type == (int)FirePost.FcStandby && t.row == 1);
            var packVal_3 = mapCell?.Where(t => t.type == (int)FirePost.HotStandby && t.row == 2);
            var packVal_4 = mapCell?.Where(t => t.type == (int)FirePost.HotStandby && t.row == 1);

            var parkDictiony1 = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            packVal_1.ForEach(t =>
            {
                parkDictiony1[t.cell_id] = MyAddressAnalysis(Configs.SlaveAddR, "InputRegister" + t.extend_field1.PadLeft(4, '0'));
            });

            var parkDictiony2 = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            packVal_2.ForEach(t =>
            {
                parkDictiony2[t.cell_id] = MyAddressAnalysis(Configs.SlaveAddR, "InputRegister" + t.extend_field1.PadLeft(4, '0'));
            });

            var parkDictiony3 = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            packVal_3.ForEach(t =>
            {
                parkDictiony3[t.cell_id] = MyAddressAnalysis(Configs.SlaveAddR, "InputRegister" + t.extend_field1.PadLeft(4, '0'));
            });

            var parkDictiony4 = new Dictionary<int, BatMes.Device.PLC.IAddress>();
            packVal_4.ForEach(t =>
            {
                parkDictiony4[t.cell_id] = MyAddressAnalysis(Configs.SlaveAddR, "InputRegister" + t.extend_field1.PadLeft(4, '0'));
            });

            opVal.TryAdd(SignalType.R_Modbus1, parkDictiony1);
            opVal.TryAdd(SignalType.R_Modbus2, parkDictiony2);
            opVal.TryAdd(SignalType.R_Modbus3, parkDictiony3);
            opVal.TryAdd(SignalType.R_Modbus4, parkDictiony4);
            return opVal;
        }
        #endregion

        #region 定义变量
        /// <summary>
        /// 位置 => 映射PLC地址
        /// </summary>
        private readonly ConcurrentDictionary<FirePost, DevAddMap> KvAddMap;

        /// <summary>
        /// 感温光纤4个通道是否含有告警信号点
        /// </summary>
        private readonly List<(int parkVal, FirePost postVal, BatMes.Device.PLC.Modbus.AddressModbus addR)> ModbusHasWarning; 

        /// <summary>
        /// 当前分容上传MES温度类型
        /// </summary>
        private FcUploadVal FcUploadVal
        {
            get
            {
                return (FcUploadVal)CORE.Instance.SysPara<int>("FcUploadVal");
            }
        }
        #endregion

        #region 读写PLC

        /// <summary>
        /// 发送心跳
        /// </summary>
        public void DoHeartBit()
        {
            KvAddMap?.Values.ForEach(map =>
            {
                map.OneMap.ForEach(bit =>
                {
                    if(bit.Key == SignalType.W_HotBit || bit.Key == SignalType.W_FcStandbyBit)
                    {
                        if (map.ActionPlc.IsOpen)
                            map.ActionPlc.WriteBool(bit.Value.AddR, bit.Value.wVal);
                    }
                });
            });
        }

        /// <summary>
        /// （定制化需求）反馈行列层
        /// </summary>
        public void FeedbackRcl(FirePost firePost, bool isSpray, ushort rowVal, ushort colVal, ushort layVal)
        {
            var hasMap = KvAddMap.ContainsKey(firePost) ? KvAddMap[firePost] : default;
            if (hasMap == null)
                return;
            var rclAddR = isSpray ? hasMap.DoRclAddR : hasMap.FireRclAddR;
            if (rclAddR == default)
                return;
            var plcIns = hasMap.ActionPlc;
            if (!plcIns.IsOpen)
                return;
            plcIns?.WriteUShort(rclAddR.rowAddR, rowVal);
            plcIns?.WriteUShort(rclAddR.colAddR, colVal);
            plcIns?.WriteUShort(rclAddR.layAddR, layVal);
        }

        /// <summary>
        /// 定制化需求（打开喇叭）
        /// </summary>
        /// <param name="firePost"></param>
        public void OpenSpeaker(FirePost firePost)
        {
            var hasMap = KvAddMap.ContainsKey(firePost) ? KvAddMap[firePost] : default;
            if (hasMap == null)
                return;
            var plcIns = hasMap.ActionPlc;
            if (!hasMap.OneMap.ContainsKey(SignalType.W_Speaker) || !plcIns.IsOpen)
                return;
            var (addR, wVal) = hasMap.OneMap[SignalType.W_Speaker];
            plcIns?.WriteBool(addR, wVal);
        }

        /// <summary>
        /// 执行消防动作
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public bool DoOutFire(FirePost firePost, FireAction fireAction, int cellID, bool isSpray = true)
        {
            var devMap = GetDevAddMap(firePost);
            if (devMap == null || !devMap.ActionMap.ContainsKey(fireAction))
                return default;
            var actionMap = devMap.ActionMap[fireAction];
            if(actionMap == null || !actionMap.ContainsKey(cellID))
            {
                var showMsg = $"库位ID【{cellID}】对应的PLC地址为空，通知消防失败。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                LogManager.Error(showMsg);
                return false;
            }

            var plcIns = devMap.ActionPlc;
            var addR = actionMap[cellID].AddR;
            var actionVal = actionMap[cellID].writeVal;
            var writeVal = isSpray ? actionMap[cellID].writeVal : (ushort)0;
            var opVal = default(BatMes.Device.OperateResult);
            if (!plcIns.IsOpen)
                return default;

            //定制化需求，分容库位在喷淋时候，要通知电源保护、压床弹开等一系列操作 By 20201029
            if (firePost == FirePost.Fc && isSpray)
            {
                FcCellOffOrProtect(cellID);
            }

            //高温静置架的喷淋信号点特殊处理，由于一列有两个电磁阀,且两个电磁阀有一个地址控制，所以这里先读出来处理再写入 By20201118
            if(firePost == FirePost.HotStandby)
            {
                var readVal = plcIns?.ReadUShort(addR);
                var currentVal = readVal.Data;
                var doVal = isSpray ? actionVal : (actionVal == 1 ? 14 : (actionVal == 2 ? 13 : 0));
                var readyVal = isSpray ? (currentVal | doVal) : (currentVal & doVal);
                opVal = plcIns?.WriteUShort(addR, (ushort)readyVal);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    opVal = plcIns?.WriteUShort(addR, writeVal);
                    if (opVal?.Code == BatMes.Device.ResultCode.Success)
                        break;
                }
            }
            
            if (opVal?.Code != BatMes.Device.ResultCode.Success)
            {
                var showMsg = $"PLC地址【{addR.AddressAscii}】读取失败：{opVal?.Message ?? "返回值为空"}。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                LogManager.Warn(showMsg);
                return false;
            }

            //若发生喷淋通知MES
            if (isSpray)
            {
                BatMes.Enums.DeviceType deviceType = firePost == FirePost.Fc ? BatMes.Enums.DeviceType.Fc : BatMes.Enums.DeviceType.Standby;
                MesManager.Instance.WeiLanFireSpray(deviceType, cellID);
            }
            return true;
        }

        /// <summary>
        /// 批量获取（一对一）: 暂时支持bool\ushort类型，其他类型用到时在扩展。KEY:库位ID  VALUE:想要获取的值
        /// <returns></returns>
        public ConcurrentDictionary<int, T> NoBulkGetVal<T>(FirePost firePost, SignalType signalType)
        {
            var devMap = GetDevAddMap(firePost);
            if (devMap == null || !devMap.ReadMap.ContainsKey(signalType))
                return default;
            var opRes = new ConcurrentDictionary<int, T>();
            var readPlcs = devMap.ReadPlc;
            //var readMap = devMap.ReadMap[signalType];
            var readMapByIns = devMap.ReadMapByIns;
            var hasVaildIns = readPlcs.Where(t => t.IsOpen);
            if(hasVaildIns.Any())
            {
                hasVaildIns.AsParallel().ForAll(plcIns =>
                {
                    var rangeMap = readMapByIns.ContainsKey(plcIns) ? readMapByIns[plcIns] : default;
                    if (rangeMap == null)
                        return;
                    if (typeof(T) == typeof(bool))
                    {
                        rangeMap.AsParallel().ForAll(kv =>
                        {
                            var opVal = plcIns?.ReadBool(kv.Value);
                            if (opVal?.Code == BatMes.Device.ResultCode.Failure)
                            {
                                var logMsg = $"【{kv.Value.AddressAscii}】: {opVal.Message}";
                                LogManager.Warn(logMsg);
                                return;
                            }
                            else if (opVal?.Code == BatMes.Device.ResultCode.Success && opVal.Data)
                            {
                                //若读出来是报警的，为了减少误报警，延迟后再次读一次
                                //Thread.Sleep(100);
                                //var opVal2 = plcIns?.ReadBool(kv.Value);
                                opRes[kv.Key] = opVal.Data.To<T>();
                                OpenSpeaker(firePost);
                            }   
                        });
                    }
                    else if (typeof(T) == typeof(ushort))
                    {
                        rangeMap.AsParallel().ForAll(kv =>
                        {
                            var opVal = plcIns?.ReadUShort(kv.Value);
                            if (opVal?.Code == BatMes.Device.ResultCode.Success)
                                opRes[kv.Key] = opVal.Data.To<T>();
                            else
                            {
                                var logMsg = $"【{kv.Value.AddressAscii}】: {opVal.Message}";
                                LogManager.Warn(logMsg);
                            }
                        });
                    }
                    else if (typeof(T) == typeof(decimal))
                    {
                        rangeMap.AsParallel().ForAll(kv =>
                        {
                            var opVal = plcIns?.ReadUShort(kv.Value);
                            if (opVal?.Code == BatMes.Device.ResultCode.Success)
                            {
                                //Modbus文档中，剩0.01得到最终的温度值
                                //感温光纤有个特殊的值为38221、38222，其对应为-273.15的float的值，这是没有意义的值。	
                                if (!Configs.SpecialVals.Contains(opVal.Data))
                                {
                                    var toFloat = Math.Round(opVal.Data * 0.01M, 2);
                                    //opRes[kv.Key] = toFloat.To<T>();
                                    if (toFloat >= Configs.MinTempForStandby)
                                        PublicDel.AutoGetTempByStandby?.Invoke(kv.Key, toFloat);
                                }
                            }
                            else
                            {
                                var logMsg = $"【{kv.Value.AddressAscii}】: {opVal.Message}";
                                LogManager.Warn(logMsg);
                            }
                        });
                    }
                });
            }
            return opRes;
        }

        /// <summary>
        /// 批量获取（存在一对多）: 暂时支持bool\ushort类型，其他类型用到时在扩展。KEY:库位ID  VALUE:想要获取的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="firePost"></param>
        /// <param name="signalType"></param>
        /// <returns></returns>
        public ConcurrentDictionary<int, T> BulkGetVal<T>(FirePost firePost, SignalType signalType)
        {
            var devMap = GetDevAddMap(firePost);
            if (devMap == null || !devMap.ReadMapBulk.ContainsKey(signalType))
                return default;
            var opRes = new ConcurrentDictionary<int, T>();
            var readPlcs = devMap.ReadPlc;
            //var readMap = devMap.ReadMapBulk[signalType];
            var readMapByIns = devMap.ReadMapBulkByIns;
            var hasVaildIns = readPlcs.Where(t => t.IsOpen);
            if (hasVaildIns.Any())
            {
                hasVaildIns.AsParallel().ForAll(plcIns => 
                {
                    var rangeMap = readMapByIns.ContainsKey(plcIns) ? readMapByIns[plcIns] : default;
                    if (rangeMap == null)
                        return;
                    if (typeof(T) == typeof(bool))
                    {
                        rangeMap.AsParallel().ForAll(kv =>
                        {
                            kv.Value.ForEach(addR =>
                            {
                                var opVal = plcIns?.ReadBool(addR);
                                if (opVal?.Code == BatMes.Device.ResultCode.Failure)
                                {
                                    var logMsg = $"【{addR.AddressAscii}】: {opVal.Message}";
                                    LogManager.Warn(logMsg);
                                    return;
                                }
                                else if (opVal?.Code == BatMes.Device.ResultCode.Success && opVal.Data)
                                {
                                    //若读出来是报警的，为了减少误报警，延迟后再次读一次
                                    //Thread.Sleep(100);
                                    //var opVal2 = plcIns?.ReadBool(addR);s
                                    opRes[kv.Key] = opVal.Data.To<T>();
                                    OpenSpeaker(firePost);
                                }
                            });
                        });
                    }
                    else if (typeof(T) == typeof(decimal))
                    {
                        rangeMap.AsParallel().ForAll(kv =>
                        {
                            if (!kv.Value.Any())
                                return;
                            var sAddR = kv.Value.FirstOrDefault();
                            var byteRes = plcIns?.Read(sAddR, (ushort)kv.Value.Count);
                            if (byteRes?.Code == BatMes.Device.ResultCode.Success && byteRes.Data != null)
                            {
                                var valList = new List<decimal>();
                                var arrayVal = byteRes.Data;
                                for (int i = 0; i < arrayVal.Length; i += 2)
                                {
                                    var tempVal = BitConverter.ToUInt16(arrayVal, i) * 0.1M; //分容压床只需剩0.1，Modbus剩0.01
                                    valList.Add(tempVal);
                                }
                                if (!valList.Any())
                                    return;
                                //if (FcUploadVal == FcUploadVal.Avg)
                                //    opRes[kv.Key] = Math.Round(valList.Average(), 2).To<T>();
                                //else
                                //    opRes[kv.Key] = valList.Max().To<T>();
                                //新需求：要判断超温的点有几个，且条件由本地判断，因此函数暂时仅用于压床读取，所以可以这里直接判断 By 20201028
                                var tooCount = valList.Count(t => t >= Configs.FcHotTemp);
                                if(tooCount > 0)
                                    opRes[kv.Key] = tooCount.To<T>();
                            }
                        });
                    }
                    else if (typeof(T) == typeof(ushort))
                    {
                        rangeMap.AsParallel().ForAll(kv =>
                        {
                            kv.Value.ForEach(addR =>
                            {
                                var opVal = plcIns?.ReadUShort(addR);
                                double maxVal = 0;
                                if (opVal?.Code == BatMes.Device.ResultCode.Success)
                                {
                                    var toFloat = Math.Round(opVal.Data * 0.01, 3); //modbus文档中，剩0.01得到最终的温度, 取最大的温度
                                    if (toFloat >= maxVal)
                                    {
                                        opRes[kv.Key] = toFloat.To<T>();
                                        maxVal = toFloat;
                                    }
                                }
                                else
                                {
                                    var logMsg = $"【{addR.AddressAscii}】: {opVal.Message}";
                                    LogManager.Warn(logMsg);
                                }
                            });
                        });
                    }
                });
            }  
            return opRes;
        }

        /// <summary>
        /// 获取modbus是否含有超温报警
        /// </summary>
        /// <returns></returns>
        public List<(int parkVal, FirePost firePost, bool hasFire)> GetModbusHasWarning()
        {
            var opVal = new List<(int parkVal, FirePost firePost, bool hasFire)>();
            var devMap = GetDevAddMap(FirePost.ModBus);
            if (devMap == null)
                return default;
            var readPlcs = devMap.ReadPlc;
            var hasVaildIns = readPlcs.Where(t => t.IsOpen);
            if (hasVaildIns.Any())
            {
                var hasPLC = hasVaildIns.First();
                ModbusHasWarning?.ForEach(t =>
                {
                    var readVal = hasPLC?.ReadUShort(t.addR);
                    if(readVal?.Code == BatMes.Device.ResultCode.Success)
                    {
                        if(readVal.Data == 1)
                            opVal.Add((t.parkVal, t.postVal, true));
                    }
                    else
                    {
                        var logMsg = $"【{t.addR.AddressAscii}】: {readVal.Message}";
                        LogManager.Warn(logMsg);
                    }
                });
            }
            return opVal;
        }
        #region 分容定制化需求
        /// <summary>
        /// 分容库位断电或保护，或者其他待扩展需求
        /// </summary>
        /// <param name="cellNo"></param>
        /// <param name="doType"></param>
        /// <returns></returns>
        public bool FcCellOffOrProtect(int cellNo, SignalType doType = SignalType.W_NotifyFcSpray)
        {
            var devMap = GetDevAddMap(FirePost.Fc);
            if (devMap == null || !devMap.DiyMap.ContainsKey(doType))
                return default;
            var plcIns = devMap.ReadPlc.FirstOrDefault();
            var readMap = devMap.DiyMap[doType];
            if (!readMap.ContainsKey(cellNo) || !plcIns.IsOpen)
                return default;
            var addR = readMap[cellNo];
            var opVal = plcIns.WriteBool(addR, true);
            if (opVal?.Code != BatMes.Device.ResultCode.Success)
            {
                var showMsg = $"压床PLC地址【{addR.AddressAscii}】写入失败：{opVal?.Message ?? "返回值为空"}。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                LogManager.Warn(showMsg);
                return false;
            }
            return opVal?.Code == BatMes.Device.ResultCode.Success;
        }
        #endregion

        #endregion

        #region 公用
        /// <summary>
        /// 获取设备的连接状态
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<string, ConnetStatus> GetDeviceStatusByInit()
        {
            var openVal = new ConcurrentDictionary<string, ConnetStatus>();
            KvAddMap.AsParallel().ForAll(kv => 
            {
                kv.Value.ReadPlc.AsParallel().ForAll(read =>
                {
                    openVal[read.ID] = new ConnetStatus
                    {
                        IsOpen = read.IsOpen,
                        Message = GetMsgByInit(kv.Key, read.IsOpen, read.ID)
                    };
                });
            });
            return openVal;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public async Task CloseDevice()
        {
            await Task.Run(() =>
            {
                KvAddMap.Values.ForEach(t =>
                {
                    t.ReadPlc.ForEach(read =>
                    {
                       read.Close();
                    });
                    t.ActionPlc?.Close();
                });
            });
        }

        /// <summary>
        /// 开启设备
        /// </summary>
        public async Task OpenDevice()
        {
            await Task.Run(() => 
            {
                KvAddMap.Values.AsParallel().ForAll(t =>
                {
                    t.ReadPlc.AsParallel().ForAll(read =>
                    {
                        read?.Open();
                    });
                    //t.ActionPlc?.Open();  ActionPlc已经包含在ReadPlc中，无需重复打开
                });

                //为了提升读取速度，将建立多个PLC对象，根据不同的端口。
                //如PLC对象打开成功，则将读取的信号点分片给不同的PLC，这样可以提高读取速度
                //修改自：ByCGF 2020/10/08
                KvAddMap.Values.ForEach(devMap =>
                {
                    var readyOpen = devMap.ReadPlc.Where(t => t.IsOpen);
                    if (!readyOpen.Any())
                        return;
                    var readyCount = readyOpen.Count();
                    var startIndex = 0;
                    if (devMap.ReadMap.Any())
                    {
                        var targetMap = devMap.ReadMap.First().Value;
                        var pageSize = targetMap.Count() / readyCount; //最后一个PLC要接受剩余的信号点
                        readyOpen.ForEach(o =>
                        {
                            if (readyCount == 1 || readyCount > targetMap.Count())
                                devMap.ReadMapByIns[o] = targetMap.AsEnumerable();
                            else
                                devMap.ReadMapByIns[o] = GetRangeExByKv(startIndex, pageSize, targetMap, (startIndex + 1) == readyCount);
                            startIndex++;
                        });
                    }
                    startIndex = 0;
                    if (devMap.ReadMapBulk.Any())
                    {
                        var targetMap = devMap.ReadMapBulk.First().Value;
                        var pageSize = targetMap.Count() / readyCount; //最后一个PLC要接受剩余的信号点
                        readyOpen.ForEach(o =>
                        {
                            if (readyCount == 1 || readyCount > targetMap.Count())
                                devMap.ReadMapBulkByIns[o] = targetMap.AsEnumerable();
                            else
                                devMap.ReadMapBulkByIns[o] = GetRangeExByKv(startIndex, pageSize, targetMap, (startIndex + 1) == readyCount);
                            startIndex++;
                        });
                    }
                });
            });
        }


        /// <summary>
        /// 将集合分片处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sourceVal"></param>
        /// <param name="noTake">是否需要take，最后一次的元素，一次取完不再留</param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<int, T>> GetRangeExByKv<T>(int pageIndex, int pageSize, Dictionary<int, T> sourceVal, bool noTake = false)
        {
            return sourceVal.GetRangeEx(pageIndex, pageSize, noTake);
        }

        /// <summary>
        /// 每隔60秒打开设备
        /// </summary>
        public void AlwaysOnDev()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    KvAddMap?.Values.AsParallel().ForAll(t =>
                    {
                        t.ReadPlc.AsParallel().ForAll(read =>
                        {
                            if (!read.IsOpen)
                                read.Open();
                        });
                        if(!t.ActionPlc.IsOpen)
                            t.ActionPlc.Open();
                    });
                    Thread.Sleep(1000 * 60);
                }
            });
        }

        /// <summary>
        /// 通过映射获取地址
        /// </summary>
        /// <param name="firePost">位置</param>
        /// <param name="signalType">操作类型</param>
        /// <param name="cellID">库位id，当库位ID为空时默认取第一个</param>
        /// <returns></returns>
        private (List<BatMes.Device.PLC.IPlc> plcIns, BatMes.Device.PLC.IAddress pointAdd) GetAddressByMap(FirePost firePost, SignalType signalType, int? cellID = null)
        {
            if(cellID == null)
            {
                var hasMap = KvAddMap.ContainsKey(firePost) ? KvAddMap[firePost] : null;
                var hasReadMap = hasMap != null ? (hasMap.ReadMap.ContainsKey(signalType) ? hasMap.ReadMap[signalType] : null) : null;
                var hasAddR = hasReadMap != null ? hasReadMap.First().Value : null;
                if (hasAddR == null)
                {
                    var showMsg = $"位置【{firePost}】类型【{signalType}】对应的PLC地址，读取不会成功。";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                    LogManager.Error(showMsg);
                    return default;
                }
                return (hasMap?.ReadPlc, hasAddR);
            }
            else
            {
                var hasMap = KvAddMap.ContainsKey(firePost) ? KvAddMap[firePost] : null;
                var hasReadMap = hasMap != null ? (hasMap.ReadMap.ContainsKey(signalType) ? hasMap.ReadMap[signalType] : null) : null;
                var hasAddR = hasReadMap != null ? (hasReadMap.ContainsKey(cellID.Value) ? hasReadMap[cellID.Value] : default) : null;
                if (hasAddR == null)
                {
                    var showMsg = $"位置【{firePost}】类型【{signalType}】找不到库位【{cellID}】对应的PLC地址，读取不会成功。";
                    CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                    LogManager.Error(showMsg);
                    return default;
                }
                return (hasMap?.ReadPlc, hasAddR);
            }
        }

        /// <summary>
        /// 获取位置 => 映射地址
        /// </summary>
        /// <param name="firePost"></param>
        /// <returns></returns>
        private DevAddMap GetDevAddMap(FirePost firePost)
        {
            return KvAddMap.ContainsKey(firePost) ? KvAddMap[firePost] : default;
        }

        /// <summary>
        /// 获取连接描述
        /// </summary>
        /// <param name="firePost"></param>
        /// <param name="openFlag"></param>
        /// <returns></returns>
        private string GetMsgByInit(FirePost firePost, bool openFlag, string ipAddR)
        {
            string opMsg = default;
            string conMsg = openFlag ? "连接成功" : "连接失败";
            switch (firePost)
            {
                case FirePost.Fc:
                    opMsg = $"分容压床PLC指定端口{conMsg}：{ipAddR}";
                    break;
                case FirePost.FcStandby:
                    opMsg = $"常温静置架PLC指定端口{conMsg}：{ipAddR}";
                    break;
                case FirePost.HotStandby:
                    opMsg = $"高温静置架PLC指定端口{conMsg}：{ipAddR}";
                    break;
                case FirePost.ModBus:
                    opMsg = $"感温光纤Modbus指定端口{conMsg}：{ipAddR}";
                    break;
            }
            return opMsg;
        }
        #endregion
    }
}