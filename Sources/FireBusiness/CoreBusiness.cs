using BatMes.Client;
using BatMes.Client.Entity.batmes_client;
using FireBusiness.Dev;
using FireBusiness.Enums;
using FireBusiness.Model;
using FireBusiness.OutSystem;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FireBusiness
{
    public class CoreBusiness
    {
        #region 实例
        private static readonly object syncLock = new object();

        public static CoreBusiness Hub { get; private set; }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static CoreBusiness GreatHub()
        {
            lock (syncLock)
            {
                if (Hub == null)
                    Hub = new CoreBusiness();
            }
            return Hub;
        }

        CoreBusiness()
        {
        }
        #endregion

        #region 公共
        /// <summary>
        /// 初始化操作
        /// </summary>
        public void StartTaskWhenInit()
        {
            //订阅事件
            PublicDel.AutoGetTempByStandby += AutoGetTempByStandby;

            InitDeviceStatus();
            CellIdMapToBTS();
            InitFireCheck();
        }

        /// <summary>
        /// 实时获取静置架的温度
        /// </summary>
        /// <param name="cellNo"></param>
        /// <param name="tempVal"></param>
        public void AutoGetTempByStandby(int cellNo, decimal tempVal)
        {
            try
            {
                //OnlyUptCellTemp(cellNo, tempVal);
                var postType = GetPostByCell(cellNo);
                if (postType == FirePost.Unknown)
                    return;
                var (tempAction, tempWarn) = KvTempPost[postType];
                if (tempVal >= tempWarn)
                {
                    var actionVal = BatMes.Enums.DeviceFireMode.None;
                    var newCol = new FireControlModel
                    {
                        CellNo = cellNo,
                        FirePost = postType,
                        DeviceStatus = DeviceStatus.TempAnomaly,
                        FireAction = ChnageActionType(actionVal),
                        WithoutByHand = Configs.AutoMinByStandby,
                        DevTemp = tempVal
                    };
                    AddOrUpdateCol(newCol.CellNo, newCol);
                    //放到最后通知MES，以提升响应性能
                    if (postType == FirePost.Fc)
                        MesManager.Instance.CallFireTempByFc(cellNo, tempVal);
                    else
                        MesManager.Instance.CallFireTempByStandby(cellNo, tempVal);
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
            }
        }

        #endregion

        #region 定义变量
        /// <summary>
        /// 本地调试模式
        /// </summary>
        public bool IsDebug => CORE.Instance.SysPara<bool>("IsDebug");

        /// <summary>
        /// 开启调式模式下，是否要开启模拟喷淋，若有火警信号
        /// </summary>
        public bool IsSimSpray => CORE.Instance.SysPara<bool>("IsSimSpray");

        /// <summary>
        /// 运行标志
        /// </summary>
        private bool IsRunning { get; set; } = true; 

        /// <summary>
        /// 是否需要初始化库位
        /// </summary>
        private bool NeedInitCell => CORE.Instance.SysPara<bool>("NeedInitCell");

        /// <summary>
        /// 若发生火警，无人工多久自动执行消防
        /// </summary>
        private int WithoutByHand => CORE.Instance.SysPara<int>("WithoutByHand");

        /// <summary>
        /// 库位ID映射库位位置
        /// </summary>
        private ConcurrentDictionary<int, cell> KvCellMap { get; set; } = new ConcurrentDictionary<int, cell>();

        /// <summary>
        /// MES上配置的温度阈值 KEY:位置 VALUE:（执行温度, 告警温度）
        /// </summary>
        private ConcurrentDictionary<FirePost, (decimal FireActTemp, decimal FireWarnTemp)> KvTempPost;

        /// <summary>
        /// 消防执行队列
        /// </summary>
        private ConcurrentDictionary<int, FireControlModel> KvHasControl { get; set; } = new ConcurrentDictionary<int, FireControlModel>();

        /// <summary>
        /// 状态描述映射
        /// </summary>
        private readonly Dictionary<DeviceStatus, string> StatusMap = new Dictionary<DeviceStatus, string>
        {
            {DeviceStatus.Normal, "正常"},
            {DeviceStatus.TempAnomaly, "温度异常"},
            {DeviceStatus.FireAlarm, "烟雾告警"},
            {DeviceStatus.Fault, "设备故障"},
            {DeviceStatus.OtherEx, "其他异常"}
        };

        /// <summary>
        /// 执行动作描述
        /// </summary>
        private readonly Dictionary<FireAction, string> ActionDesMap = new Dictionary<FireAction, string>
        {
            {FireAction.Nothing, "无"},
            {FireAction.Spray, "喷淋"},
            {FireAction.WaterSpray, "喷水"},
            {FireAction.Other, "其他"},
        };
        #endregion

        #region 操作变量
        /// <summary>
        /// 新增或更新消防控制键值对
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="fireControl"></param>
        private void AddOrUpdateCol(int cellID, FireControlModel fireCol)
        {
            var hasCol = KvHasControl.ContainsKey(cellID) ? KvHasControl[cellID] : default;
            if (hasCol == null)
            {
                KvHasControl.TryAdd(cellID, fireCol);
            }
            else
            {
                hasCol.FireAction = fireCol.FireAction;
                hasCol.DeviceStatus = fireCol.DeviceStatus;
                hasCol.LastUpdateTime = fireCol.CreateTime;
                if(fireCol.TooHotCount > 0)
                {
                    hasCol.TooHotCount = fireCol.TooHotCount;
                }

                if (fireCol.CellHasSmoke)
                {
                    hasCol.CellHasSmoke = fireCol.CellHasSmoke;
                }

                if(fireCol.FirePost == FirePost.Fc && hasCol.TooHotCount > 0)
                {
                    hasCol.FireAction = ((hasCol.CellHasSmoke && hasCol.TooHotCount >= Configs.FcHasFireTooHighCount) || hasCol.TooHotCount >= Configs.FcTempTooHighCount) ? FireAction.Spray : FireAction.Nothing;
                }
                //KvHasControl.AddOrUpdate(cellID, hasCol, (oldVal, newVal) => hasCol);
            }
            RemarkAndStatusToUI(cellID, fireCol.DeviceStatus, hasCol.FireAction, fireCol.DevTemp, "库位有告警");
            LogManager.Info(GetLogMsg(fireCol));

            //定制化需求，反馈行列层给PLC
            if (fireCol.DeviceStatus == DeviceStatus.FireAlarm)
            {
                var hasCell = KvCellMap.ContainsKey(cellID) ? KvCellMap[cellID] : default;
                if (hasCell != null)
                    DEV.Instance.FeedbackRcl(fireCol.FirePost, false, (ushort)hasCell.row.Value, (ushort)hasCell.col.Value, (ushort)hasCell.lay.Value);
            }
            //定制化需求：打开喇叭
            if (fireCol.DeviceStatus == DeviceStatus.FireAlarm || fireCol.DeviceStatus == DeviceStatus.TempAnomaly)
            {
                DEV.Instance.OpenSpeaker(fireCol.FirePost);

                //定制化需求，压床有火警则弹开
                if (fireCol.FirePost == FirePost.Fc)
                {
                    DEV.Instance.FcCellOffOrProtect(cellID, SignalType.W_DoBrakeUp);
                }
            }
        }

        /// <summary>
        /// 移除控制模型
        /// </summary>
        /// <param name="cellID"></param>
        private FireControlModel RemoveCol(int cellID)
        {
            KvHasControl.TryRemove(cellID, out FireControlModel outCol);
            return outCol;
        }

        /// <summary>
        /// 初始化库位ID映射库位位置
        /// </summary>
        private void InitPostMap()
        {
            KvCellMap = KvCellMap ?? new ConcurrentDictionary<int, cell>();
            var cellList = Business.Instance.GetCellsAsync()?.Result;
            cellList?.ForEach(t =>
            {
                KvCellMap.TryAdd(t.cell_id, t);
            });
        }
        #endregion

        #region 初始化库位映射

        /// <summary>
        /// 程序运行开始时，打印设备初始化状态
        /// </summary>
        private void InitDeviceStatus()
        {
            var deviceStatus = DEV.Instance.GetDeviceStatusByInit();
            foreach (var status in deviceStatus)
            {
                CORE.Instance.OnMessage(status.Value.Message, status.Value.IsOpen ? BatMes.Client.Enums.SysEventLevel.Info : BatMes.Client.Enums.SysEventLevel.Error);
            }
            if(deviceStatus.Where(t => !t.Value.IsOpen).Any())
            {
                CORE.Instance.OnMessage($"有PLC连接失败，请根据日志检查。", BatMes.Client.Enums.SysEventLevel.Error);
            }
        }

        /// <summary>
        /// MES 库位ID => （BTS\PLC）本地库位库位ID
        /// </summary>
        private Dictionary<int, int> ToLocalCellMap { get; set; } = new Dictionary<int, int>();

        /// <summary>
        /// MES 库位ID => （BTS\PLC）本地库位库位ID
        /// </summary>
        private void CellIdMapToBTS()
        {
            try
            {
                string hasPath = AppDomain.CurrentDomain.BaseDirectory + "CellMap\\CellMapToBTS.json";
                string hasValue = System.IO.File.ReadAllText(hasPath);
                ToLocalCellMap = hasValue.JsonDecode<Dictionary<int, int>>();
            }
            catch (Exception)
            {
                var showMsg = $"获取库位映射关系失败，找不到CellMapToBTS.json文件。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
            }
        }

        /// <summary>
        /// 初始化库位
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Cells"></param>
        /// <param name="postType"></param>
        private void InitCell<T>(IList<T> Cells, int postType)
        {
            if (Cells == null || !NeedInitCell) 
                return;
            if (typeof(T) == typeof(BatMes.Service.Model.Standby.CellInfo))
            {
                var myCell = Cells as IList<BatMes.Service.Model.Standby.CellInfo>;
                myCell.AsParallel().ForAll(t => 
                {
                    var newCell = new cell
                    {
                        cell_id = t.CellID,
                        cell_status = (int)DeviceStatus.Normal,
                        last_update_time = DateTime.Now,
                        type = postType,
                        row = t.PhyRow,
                        col = t.PhyCol,
                        lay = t.PhyLayer,
                        extend_field1 = t.FireCode
                    };
                    Business.Instance.AddCell(newCell);
                });
            }
            else if (typeof(T) == typeof(BatMes.Service.Model.Fc.CellInfo))
            {
                var myCell = Cells as IList<BatMes.Service.Model.Fc.CellInfo>;
                myCell.AsParallel().ForAll(t =>
                {
                    var newCell = new cell
                    {
                        cell_id = t.CellID,
                        cell_status = (int)DeviceStatus.Normal,
                        last_update_time = DateTime.Now,
                        type = postType,
                        row = t.PhyRow,
                        col = t.PhyCol,
                        lay = t.PhyLayer,
                        extend_field1 = t.MakerCode
                    };
                    Business.Instance.AddCell(newCell);
                });
            }
        }
        #endregion

        #region 消防
        /// <summary>
        /// 初始化消防检查
        /// </summary>
        private void InitFireCheck()
        {
            IninKvByFile();
            //方法一：用Quartz定时任务
            //QuartzHelper.AddJob<FcStandbyJob>(Configs.FcStandyJob, Configs.StartTimeSeconds, Configs.IntervalInseconds);
            //QuartzHelper.AddJob<FcJob>(Configs.FcJob, Configs.StartTimeSeconds, Configs.IntervalInseconds);
            //QuartzHelper.AddJob<HotStandbyJob>(Configs.HotStandyJob, Configs.StartTimeSeconds, Configs.IntervalInseconds);
            //QuartzHelper.AddJob<FireJob>(Configs.FireJob, Configs.StartTimeSeconds, Configs.IntervalInseconds);

            //方法二：用Task做多线程任务
            Task.Run(() =>
            {
                while (true)
                {
                    if (IsRunning) FireHnadle();
                    Thread.Sleep(Configs.IntervalInMm);
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    if (IsRunning) FcStandbyHandle();
                    Thread.Sleep(Configs.IntervalInMm);
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    if (IsRunning) FcHandle();
                    Thread.Sleep(Configs.IntervalInMm);
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    if (IsRunning) FcTempHandle();
                    Thread.Sleep(Configs.IntervalInMm);
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    if (IsRunning) HotStandbyHandle();
                    Thread.Sleep(Configs.IntervalInMm);
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    if (IsRunning)
                    {
                        ModbusHasFire();
                        ModbusTempHandle();
                    }
                    Thread.Sleep(Configs.IntervalInMm);
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    if (IsRunning) HreatBit();
                    Thread.Sleep(1000);
                }
            });
        }

        /// <summary>
        /// 初始化键值对、初始化温度阈值
        /// 新需求：温度条件不再读取MES配置值，在本地配置
        /// </summary>
        [Obsolete]
        private void IninKvByMES()
        {
            //初始化键值对
            KvHasControl = KvHasControl ?? new ConcurrentDictionary<int, FireControlModel>();
            InitPostMap();

            //初始化温度阈值键值对
            var noNeed = 9999M;
            KvTempPost = new ConcurrentDictionary<FirePost, (decimal FireActTemp, decimal FireWarnTemp)>();
            KvTempPost.AddOrUpdate(FirePost.FcStandby, (noNeed, noNeed), (oldkkey, oldVal) => (noNeed, noNeed));
            KvTempPost.AddOrUpdate(FirePost.Fc, (noNeed, noNeed), (oldkkey, oldVal) => (noNeed, noNeed));
            KvTempPost.AddOrUpdate(FirePost.HotStandby, (noNeed, noNeed), (oldkkey, oldVal) => (noNeed, noNeed));

            //获取常温静置的温度阈值
            var opFcStandby = MesManager.Instance.StoreListByStandby(Configs.FcStandbyRgvId);
            if (opFcStandby != null && opFcStandby.Any())
            {
                var fcStandbyVal = (opFcStandby.First().FireActTemp, opFcStandby.First().FireWarnTemp);
                if(fcStandbyVal.FireWarnTemp > 0)
                    KvTempPost.AddOrUpdate(FirePost.FcStandby, fcStandbyVal, (oldkkey, oldVal) => fcStandbyVal);
                foreach(var hasCell in opFcStandby.Where(t => !t.Name.IsEmpty() && t.Name.Contains("静置架")))
                {
                    InitCell(hasCell.Cells, (int)FirePost.FcStandby);
                }
            }

            //获取分容压床的温度阈值
            var opFc = MesManager.Instance.HostListByFc(Configs.FcRgvId);
            if (opFc != null && opFc.Any())
            {
                var fcVal = (opFc.First().FireActTemp, opFc.First().FireWarnTemp);
                if(fcVal.FireWarnTemp > 0)
                    KvTempPost.AddOrUpdate(FirePost.Fc, fcVal, (oldkkey, oldVal) => fcVal);
                //InitCell(opFc.First().Cells, (int)FirePost.Fc); --因MES的编码规则无效，故分容库位只能手工配置
            }

            //获取高温静置的温度阈值
            var opHotStandby = MesManager.Instance.StoreListByStandby(Configs.HotStandbyRgvId);
            if (opHotStandby != null && opHotStandby.Any())
            {
                var hotVal = (opHotStandby.First().FireActTemp, opHotStandby.First().FireWarnTemp);
                if (hotVal.FireWarnTemp > 0)
                    KvTempPost.AddOrUpdate(FirePost.HotStandby, hotVal, (oldkkey, oldVal) => hotVal);
                foreach (var hasCell in opHotStandby.Where(t => !t.Name.IsEmpty() && t.Name.Contains("静置架")))
                {
                    InitCell(hasCell.Cells, (int)FirePost.HotStandby);
                }
            }
            CORE.Instance.OnMessage("PLC初始化完成、获取设定温度完成，消防判断开始启动。", BatMes.Client.Enums.SysEventLevel.Info);
        }

        /// <summary>
        /// 新需求：温度条件不再读取MES配置值，在本地配置
        /// </summary>
        private void IninKvByFile()
        {
            //初始化键值对
            KvHasControl = KvHasControl ?? new ConcurrentDictionary<int, FireControlModel>();
            InitPostMap();

            //初始化温度阈值键值对
            var noNeed = 9999M;
            KvTempPost = new ConcurrentDictionary<FirePost, (decimal FireActTemp, decimal FireWarnTemp)>();
            KvTempPost.AddOrUpdate(FirePost.FcStandby, (noNeed, noNeed), (oldkkey, oldVal) => (noNeed, noNeed));
            KvTempPost.AddOrUpdate(FirePost.Fc, (noNeed, noNeed), (oldkkey, oldVal) => (noNeed, noNeed));
            KvTempPost.AddOrUpdate(FirePost.HotStandby, (noNeed, noNeed), (oldkkey, oldVal) => (noNeed, noNeed));

            Configs.FcStandbyHotTemp = Configs.FcStandbyHotTemp > 0 ? Configs.FcStandbyHotTemp : 70M;
            Configs.FcHotTemp = Configs.FcHotTemp > 0 ? Configs.FcHotTemp : 70M;
            Configs.HighStandbyHotTemp = Configs.HighStandbyHotTemp > 0 ? Configs.HighStandbyHotTemp : 70M;
            if (Configs.FcStandbyHotTemp > 0)
                KvTempPost.AddOrUpdate(FirePost.FcStandby, (Configs.FcStandbyHotTemp, Configs.FcStandbyHotTemp), (oldkkey, oldVal) => (Configs.FcStandbyHotTemp, Configs.FcStandbyHotTemp));
            if(Configs.FcHotTemp > 0)
                KvTempPost.AddOrUpdate(FirePost.Fc, (Configs.FcHotTemp, Configs.FcHotTemp), (oldkkey, oldVal) => (Configs.FcHotTemp, Configs.FcHotTemp));
            if(Configs.HighStandbyHotTemp > 0)
                KvTempPost.AddOrUpdate(FirePost.HotStandby, (Configs.HighStandbyHotTemp, Configs.HighStandbyHotTemp), (oldkkey, oldVal) => (Configs.HighStandbyHotTemp, Configs.HighStandbyHotTemp));

            //静置区最低温度判断阈值
            Configs.MinTempForStandby = Configs.FcStandbyHotTemp <= Configs.HighStandbyHotTemp ? Configs.FcStandbyHotTemp : Configs.HighStandbyHotTemp;

            CORE.Instance.OnMessage($"常温静置架、分容压床、高温静置架的温度设置值分别为：{Configs.FcStandbyHotTemp}°C，{Configs.FcHotTemp}°C，{Configs.HighStandbyHotTemp}°C", BatMes.Client.Enums.SysEventLevel.Info);
            CORE.Instance.OnMessage("PLC初始化完成、获取设定温度完成，消防判断开始启动。", BatMes.Client.Enums.SysEventLevel.Info);
        }

        #region 定时任务
        /// <summary>
        /// 消防执行动作
        /// </summary>
        public void FireHnadle()
        {
            try
            {
                if (KvHasControl.Any())
                {
                    KvHasControl.Values.AsParallel().ForAll(v =>
                    {
                        if(v.Enabled)
                        {
                            DoOutFire(v);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
                CORE.Instance.OnMessage($"{ex.Message}，详情请查询日志。", BatMes.Client.Enums.SysEventLevel.Warn);
            }
        }

        /// <summary>
        /// 常温静置烟雾判断任务
        /// </summary>
        public void FcStandbyHandle()
        {
            try
            {
                var hasMap = DEV.Instance.NoBulkGetVal<bool>(FirePost.FcStandby, SignalType.R_FcStandby);
                if (!hasMap.IsEmpty())
                {
                    hasMap.AsParallel().ForAll(kv =>
                    {
                        if (kv.Value)
                        {
                            //var actionVal = MesManager.Instance.CallFireSmokeByStandby(kv.Key);
                            var actionVal = BatMes.Enums.DeviceFireMode.None;
                            var newCol = new FireControlModel
                            {
                                CellNo = kv.Key,
                                FirePost = FirePost.FcStandby,
                                DeviceStatus = DeviceStatus.FireAlarm,
                                FireAction = ChnageActionType(actionVal),
                                WithoutByHand = Configs.AutoMinByStandby
                            };
                            AddOrUpdateCol(newCol.CellNo, newCol);
                            actionVal = MesManager.Instance.CallFireSmokeByStandby(kv.Key);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
                //CORE.Instance.OnMessage($"{ex.Message}，详情请查询日志。", BatMes.Client.Enums.SysEventLevel.Warn);
            }
        }

        /// <summary>
        /// 分容压床烟雾判断任务
        /// </summary>
        public void FcHandle()
        {
            try
            {
                var hasMap = DEV.Instance.NoBulkGetVal<bool>(FirePost.Fc, SignalType.R_Fc);
                if (!hasMap.IsEmpty())
                {
                    hasMap.AsParallel().ForAll(kv =>
                    {
                        if (kv.Value)
                        {
                            //var actionVal = MesManager.Instance.CallFireSmokeByFc(kv.Key);
                            var actionVal = BatMes.Enums.DeviceFireMode.None;
                            var newCol = new FireControlModel
                            {
                                CellNo = kv.Key,
                                FirePost = FirePost.Fc,
                                DeviceStatus = DeviceStatus.FireAlarm,
                                FireAction = ChnageActionType(actionVal),
                                CellHasSmoke = true,
                                WithoutByHand = Configs.AutoMinByFc 
                            };
                            AddOrUpdateCol(newCol.CellNo, newCol);
                            actionVal = MesManager.Instance.CallFireSmokeByFc(kv.Key);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
                //CORE.Instance.OnMessage($"{ex.Message}，详情请查询日志。", BatMes.Client.Enums.SysEventLevel.Warn);
            }
        }

        /// <summary>
        /// 分容压床温度判断任务
        /// 获取温度值改成超温点数 By 20201028
        /// </summary>
        public void FcTempHandle()
        {
            try
            {
                var hasMap = DEV.Instance.BulkGetVal<decimal>(FirePost.Fc, SignalType.R_Fc_Temp);
                if (!hasMap.IsEmpty())
                {
                    hasMap.AsParallel().ForAll(kv =>
                    {
                        //var actionVal = MesManager.Instance.CallFireTempByFc(kv.Key, kv.Value); //放到最后通知MES，以提升响应性能
                        var actionVal = BatMes.Enums.DeviceFireMode.None;
                        var newCol = new FireControlModel
                        {
                            CellNo = kv.Key,
                            FirePost = FirePost.Fc,
                            DeviceStatus = DeviceStatus.TempAnomaly,
                            FireAction = ChnageActionType(actionVal),
                            WithoutByHand = Configs.AutoMinByFc, //分容库位比较特殊，需求改了比较多，做成灵活可配置
                            TooHotCount = kv.Value.To<int>(),
                            DevTemp = kv.Value
                        };
                        AddOrUpdateCol(newCol.CellNo, newCol);
                        actionVal = MesManager.Instance.CallFireTempByFc(kv.Key, kv.Value);
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
                //CORE.Instance.OnMessage($"{ex.Message}，详情请查询日志。", BatMes.Client.Enums.SysEventLevel.Warn);
            }
        }

        /// <summary>
        /// 高温静置烟雾判断任务
        /// </summary>
        public void HotStandbyHandle()
        {
            try
            {
                var hasMap = DEV.Instance.BulkGetVal<bool>(FirePost.HotStandby, SignalType.R_HotStandby);
                if (!hasMap.IsEmpty())
                {
                    hasMap.AsParallel().ForAll(kv =>
                    {
                        if (kv.Value)
                        {
                            //var actionVal = MesManager.Instance.CallFireSmokeByStandby(kv.Key); //放到最后通知MES，以提升响应性能
                            var actionVal = BatMes.Enums.DeviceFireMode.None;
                            var newCol = new FireControlModel
                            {
                                CellNo = kv.Key,
                                FirePost = FirePost.HotStandby,
                                DeviceStatus = DeviceStatus.FireAlarm,
                                FireAction = ChnageActionType(actionVal),
                                WithoutByHand = Configs.AutoMinByStandby
                            };
                            AddOrUpdateCol(newCol.CellNo, newCol);
                            actionVal = MesManager.Instance.CallFireSmokeByStandby(kv.Key);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
                //CORE.Instance.OnMessage($"{ex.Message}，详情请查询日志。", BatMes.Client.Enums.SysEventLevel.Warn);
            }
        }

        /// <summary>
        /// 读取Modbus的温度
        /// </summary>
        public void ModbusTempHandle()
        {
            try
            {
                var hasMap = DEV.Instance.NoBulkGetVal<decimal>(FirePost.ModBus, SignalType.R_Modbus);
                if (!hasMap.IsEmpty())
                {
                    hasMap.AsParallel().ForAll(kv =>
                    {
                        var postType = GetPostByCell(kv.Key);
                        if (postType == FirePost.Unknown)
                            return;
                        var (tempAction, tempWarn) = KvTempPost[postType];
                        if (kv.Value >= tempWarn)
                        {
                            var actionVal = BatMes.Enums.DeviceFireMode.None;
                            var newCol = new FireControlModel
                            {
                                CellNo = kv.Key,
                                FirePost = postType,
                                DeviceStatus = DeviceStatus.TempAnomaly,
                                FireAction = ChnageActionType(actionVal),
                                WithoutByHand = Configs.AutoMinByStandby,
                                DevTemp = kv.Value
                            };
                            AddOrUpdateCol(newCol.CellNo, newCol);
                            //放到最后通知MES，以提升响应性能
                            if (postType == FirePost.Fc)
                                actionVal = MesManager.Instance.CallFireTempByFc(kv.Key, kv.Value);
                            else
                                actionVal = MesManager.Instance.CallFireTempByStandby(kv.Key, kv.Value);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
                //CORE.Instance.OnMessage($"{ex.Message}，详情请查询日志。", BatMes.Client.Enums.SysEventLevel.Warn);
            }
        }

        /// <summary>
        /// 感温光纤是否有报警点
        /// </summary>
        private void ModbusHasFire()
        {
            try
            {
                if (!Configs.OpenSpeakerByModbus)
                    return;
                var hasVal = DEV.Instance.GetModbusHasWarning();
                if(hasVal != null && hasVal.Any())
                {
                    hasVal.ForEach(t =>
                    {
                        if(t.hasFire)
                            DEV.Instance.OpenSpeaker(t.firePost);
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message);
            }
        }

        /// <summary>
        /// 执行消防动作
        /// </summary>
        /// <param name="fireCol"></param>
        private void DoOutFire(FireControlModel fireCol)
        {
            var postVal = fireCol.FirePost;
            var actionVal = fireCol.FireAction;
            var hasCellNo = fireCol.CellNo;
            var opVal = DEV.Instance.DoOutFire(postVal, actionVal, hasCellNo);
            if (opVal)
            {
                fireCol.IsAction = true;
                fireCol.LastUpdateTime = DateTime.Now;
                RemoveCol(fireCol.CellNo);
            }
            //定制化需求，反馈行列层给PLC
            var hasCell = KvCellMap.ContainsKey(hasCellNo) ? KvCellMap[hasCellNo] : default;
            if (hasCell != null)
                DEV.Instance.FeedbackRcl(postVal, true, (ushort)hasCell.row.Value, (ushort)hasCell.col.Value, (ushort)hasCell.lay.Value);

            CORE.Instance.OnMessage($"库位ID：{hasCellNo}，已通知设备执行喷淋。", BatMes.Client.Enums.SysEventLevel.Info);
        }

        /// <summary>
        /// 手工喷淋或停止喷淋
        /// </summary>
        /// <param name="firePost"></param>
        /// <param name="fireAction"></param>
        /// <param name="cellNo"></param>
        /// <param name="isReset"></param>
        private void DoOutFireByHand(int cellNo, FirePost firePost, FireAction fireAction = FireAction.Spray, bool isSpray = true)
        {
            var opVal = DEV.Instance.DoOutFire(firePost, fireAction, cellNo, isSpray);
            if (opVal)
            {
                RemoveCol(cellNo);
            }
            if (isSpray && opVal)
            {
                //定制化需求，反馈行列层给PLC
                var hasCell = KvCellMap.ContainsKey(cellNo) ? KvCellMap[cellNo] : default;
                if (hasCell != null)
                    DEV.Instance.FeedbackRcl(firePost, true, (ushort)hasCell.row.Value, (ushort)hasCell.col.Value, (ushort)hasCell.lay.Value);
            }

            //记录人工操作日志，便于后期追溯
            var typeMsg = isSpray ? "执行喷淋" : "停止喷淋";
            var recordMsg = $"库位ID：{cellNo}，手工{typeMsg}，记录到日志。";
            LogManager.Info(recordMsg);
        }

        /// <summary>
        /// 执行心跳任务
        /// </summary>
        private void HreatBit()
        {
            DEV.Instance.DoHeartBit();
        }
        #endregion
        #endregion

        #region 扩展方法
        /// <summary>
        /// 通过库位ID获取当前库位属于哪个位置
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        private FirePost GetPostByCell(int cellID)
        {
            if (KvCellMap.ContainsKey(cellID))
                return (FirePost)KvCellMap[cellID].type;
            else
            {
                var logMsg = $"库位【{cellID}】找不到对应的位置，请检查库位表是否正确。";
                LogManager.Warn(logMsg);
                CORE.Instance.OnMessage(logMsg, BatMes.Client.Enums.SysEventLevel.Warn);
                return FirePost.Unknown;
            }
        }

        /// <summary>
        /// 切换执行类型
        /// </summary>
        /// <param name="deviceFireMode"></param>
        /// <returns></returns>
        private FireAction ChnageActionType(BatMes.Enums.DeviceFireMode deviceFireMode)
        {
            FireAction opVal = FireAction.Nothing;
            switch (deviceFireMode)
            {
                case BatMes.Enums.DeviceFireMode.None:
                    opVal = FireAction.Nothing;
                    break;
                case BatMes.Enums.DeviceFireMode.Spray:
                    opVal = FireAction.Spray;
                    break;
            }
            return opVal;
        }

        /// <summary>
        /// 获取日志模型
        /// </summary>
        /// <param name="fireControl"></param>
        /// <returns></returns>
        private string GetLogMsg(FireControlModel fireControl)
        {
            if (fireControl == null) 
                return string.Empty;
            StringBuilder sbVal = new StringBuilder();
            sbVal.Append($"库位ID：{fireControl.CellNo}，");
            sbVal.Append($"库位类型：{fireControl.FirePost}，");
            sbVal.Append($"库位状态：{fireControl.DeviceStatus}，");
            sbVal.Append($"库位温度：{fireControl.DevTemp}，");
            sbVal.Append($"超温点数：{fireControl.TooHotCount}，");
            sbVal.Append($"执行动作：{fireControl.FireAction}");
            return sbVal.ToString();
        }
        #endregion

        #region 为界面提供业务逻辑
        /// <summary>
        /// 异步获取所有库位信息
        /// </summary>
        /// <returns></returns>
        public List<cell> GetCellsAsync()
        {
            return Business.Instance.GetCellsAsync()?.Result;
        }

        /// <summary>
        /// 界面手工修改库位状态
        /// </summary>
        /// <param name="uptCell"></param>
        public (bool isOk, string hasMsg) UptCellStatusByHand(cell uptCell)
        {
            var uptFlag = Business.Instance.UpdateCellStatus(uptCell);
            if (uptFlag)
                Business.Instance.UptStatusMapByCell(uptCell.cell_id, uptCell.cell_status);
            return uptFlag ? (true, "保存成功") : (false, "库位不存在，修改失败。");
        }

        /// <summary>
        /// 更新库位状态且通知界面
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="execStatus"></param>
        /// <param name="isAdd"></param>
        private void RemarkAndStatusToUI(int cellID, DeviceStatus execStatus, FireAction? fireAction = null, decimal? devTemp = null, string remark = null)
        {
            var hasCell = KvCellMap.ContainsKey(cellID) ? KvCellMap[cellID] : default;
            if (hasCell == null)
                return;
            hasCell.cell_status = (int)execStatus;
            hasCell.last_update_time = DateTime.Now;
            hasCell.remark = remark;
            hasCell.extend_field2 = fireAction != null ? ActionDesMap[fireAction.Value] : null;
            if(devTemp != null && devTemp > 0)
            {
                hasCell.remark += hasCell.IsEmpty() ? $"库位温度（或分容超温数）：{devTemp}" : $"；库位温度（或分容超温数）：{devTemp}";
            }

            if(fireAction != null && fireAction == FireAction.Spray)
            {
                hasCell.cell_status = (int)DeviceStatus.SprayByHand;
            }

            //var uptFlag = Business.Instance.AddOrUpdateCell(hasCell);
            //if (uptFlag)
            PublicDel.AutoUpdateCellStatus?.Invoke(hasCell, default);
        }

        /// <summary>
        /// 仅更新备注信息
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="remarkCode"></param>
        private void OnlyUptRemark(int cellID, string remarkCode)
        {
            var hasCell = KvCellMap.ContainsKey(cellID) ? KvCellMap[cellID] : default;
            if (hasCell == null)
                return;
            PublicDel.AutoUpdateCellStatus?.Invoke(hasCell, remarkCode);
        }

        /// <summary>
        /// 界面手工恢复正常状态
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public void OnBackNormal(int cellID)
        {
            Task.Run(() =>
            {
                RemoveCol(cellID);
                RemarkAndStatusToUI(cellID, DeviceStatus.Normal, null, null, "手工恢复正常状态");
            });
        }

        /// <summary>
        /// 界面手工喷淋
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public void OnDoSpray(int cellID, FirePost firePost)
        {
            Task.Run(() =>
            {
                DoOutFireByHand(cellID, firePost);
                //OnlyUptRemark(cellID, "手工执行喷淋");
                RemarkAndStatusToUI(cellID, DeviceStatus.SprayByHand, FireAction.Spray, null, "手工喷淋");
                CORE.Instance.OnMessage($"库位【{cellID}】通知设备执行喷淋...", BatMes.Client.Enums.SysEventLevel.Info);
            });
        }

        /// <summary>
        /// 界面手工取消喷淋
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public void OnCancelSpray(int cellID)
        {
            Task.Run(() =>
            {
                var haCol = KvHasControl.ContainsKey(cellID) ? KvHasControl[cellID] : default;
                if (haCol != null)
                {
                    haCol.IsCancel = true;
                    haCol.ByHand = false;
                    OnlyUptRemark(cellID, "手工取消喷淋");
                    CORE.Instance.OnMessage($"库位【{cellID}】已取消喷淋...", BatMes.Client.Enums.SysEventLevel.Info);
                }
            });
        }

        /// <summary>
        /// 界面上点击停止喷淋，将喷淋动作停止
        /// </summary>
        /// <param name="cellID"></param>
        public void OnStopSpray(int cellID, FirePost firePost)
        {
            Task.Run(() =>
            {
                DoOutFireByHand(cellID, firePost, FireAction.Spray, false);
                //OnlyUptRemark(cellID, "手工停止喷淋");
                RemarkAndStatusToUI(cellID, DeviceStatus.Normal, null, null, "手工停止喷淋");
                CORE.Instance.OnMessage($"库位【{cellID}】通知设备停止喷淋...", BatMes.Client.Enums.SysEventLevel.Info);
            });
        }
        #endregion
    }
}
