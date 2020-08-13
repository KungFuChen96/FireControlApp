using BatMes.Client;
using BatMes.Client.Entity.batmes_client;
using FireBusiness.Dev;
using FireBusiness.Enums;
using FireBusiness.IntervalJob;
using FireBusiness.Model;
using FireBusiness.OutSystem;
using FireBusiness.Quartz;
using ExtFuncs;
using Neware.BTS.Service;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="socketAddress"></param>
        /// <param name="port"></param>
        /// <param name="isNotifyTemp"></param>
        /// <param name="totalCells"></param>
        /// <param name="waterCells"></param>
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
        /// 在初始化完成后，开始做
        /// </summary>
        public void StartTaskWhenInit()
        {
            InitDeviceStatus();
            CellIdMapToBTS();
            CellIdMapToMES();
            InitFireCheck();
            HasNoComplete();
        }

        #endregion

        #region 定义变量
        /// <summary>
        /// 本地调试模式
        /// </summary>
        public bool IsDebug => Business.Instance.SysPara<bool>("IsDebug");


        /// <summary>
        /// 工步文件存放类型
        /// </summary>
        public int OpsFileType => Business.Instance.SysPara<int>("OpsFileType");

        /// <summary>
        /// 当前工序
        /// </summary>
        public int MesOpsValue => Business.Instance.SysPara<int>("CurrentProcesses");

        /// <summary>
        /// 过程状态描述
        /// </summary>
        private readonly Dictionary<DeviceStatus, string> StatusMap = new Dictionary<DeviceStatus, string>
        {
            {DeviceStatus.Normal, "正常"},
            {DeviceStatus.TempAnomaly, "温度异常"},
            {DeviceStatus.FireAlarm, "烟雾警告"},
            {DeviceStatus.Fault, "设备故障"},
            {DeviceStatus.OtherEx, "其他异常"}
        };
        #endregion

        #region 业务逻辑

        #endregion

        #region 托盘模型
        /// <summary>
        /// 获取托盘模型
        /// </summary>
        public List<TrayModel> TrayModels { get; } = new List<TrayModel>();

        /// <summary>
        /// 新增一个托盘模型
        /// </summary>
        /// <param name="trayModel"></param>
        public void AddTrayModel(TrayModel trayModel)
        {
            if (trayModel.IsEmpty())
                return;
            TrayModels.RemoveAll(t => t.TrayCode == trayModel.TrayCode || t.CellID == trayModel.CellID);
            TrayModels.Add(trayModel);
            //持久化
            var insertMap = new tray_map
            {
                tray_code = trayModel.TrayCode,
                cell_id = trayModel.CellID,
                processes = trayModel.OpsValue,
                exec_status = (int)DeviceStatus.Normal,
                type = (int)trayModel.TrayAttr,
                last_update_time = DateTime.Now,
                mes_model = trayModel.JsonEncode()
            };
            Business.Instance.AddTrayMap(insertMap);
            UptCellNotityUi(trayModel.CellID, trayModel.TrayCode, DeviceStatus.Normal, trayModel.TrayAttr, true);
        }

        /// <summary>
        /// 删除指定模型(或不启用)
        /// </summary>
        /// <param name="trayModel"></param>
        /// <param name="isDelete"></param>
        public void RemoveTray(TrayModel trayModel, bool isDelete = true)
        {
            if (trayModel.IsEmpty())
                return;
            if(isDelete)
                TrayModels.RemoveAll(t => t.TrayCode == trayModel.TrayCode);
            else
            {
                var hasOne = TrayModels.Where(t => t.TrayCode == trayModel.TrayCode).FirstOrDefault();
                if (hasOne != null)
                    hasOne.Enabled = false;
            }
        }

        /// <summary>
        /// 获取指定的托盘模型
        /// </summary>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        public TrayModel GetTrayModel(string trayCode, int? CellID = null)
        {
            if (trayCode.IsEmpty())
                return default;
            var hasCache = TrayModels.Where(t => t.TrayCode == trayCode && t.Enabled).OrderByDescending(t => t.CreateTime).FirstOrDefault();
            if (hasCache != null)
                return hasCache;
            var hasAccess = Business.Instance.GetTrayMap(trayCode, CellID);
            return (hasAccess?.mes_model).JsonDecode<TrayModel>();
        }

        /// <summary>
        /// 根据库位ID获取托盘模型
        /// </summary>
        /// <param name="Cell"></param>
        /// <returns></returns>
        public TrayModel GetTrayModelByCell(int cellID)
        {
            var hasCache = TrayModels.Where(t => t.CellID == cellID && t.Enabled).OrderByDescending(t => t.CreateTime).FirstOrDefault();
            if (hasCache != null)
                return hasCache;
            var hasAccess = Business.Instance.GetTrayMapByCell(cellID);
            return (hasAccess?.mes_model).JsonDecode<TrayModel>();
        }
        #endregion

        #region 结果模型
        /// <summary>
        /// 获取托盘模型
        /// </summary>
        public List<MESResults> TestResults { get; } = new List<MESResults>();

        /// <summary>
        /// 新增结果模型
        /// </summary>
        /// <param name="testResults"></param>
        private void AddTestResult(MESResults testResults)
        {
            if (testResults.IsEmpty())
                return;
            TestResults.RemoveAll(t => t.TrayCode == testResults.TrayCode || t.CellID == testResults.CellID);
            TestResults.Add(testResults);
            //持久化
            var uptMap = new tray_map
            {
                tray_code = testResults.TrayCode,
                cell_id = testResults.CellID,
                processes = testResults.OpsValue,
                exec_status = (int)DeviceStatus.Normal,
                last_update_time = DateTime.Now,
                bts_result = testResults.JsonEncode()
            };
            Business.Instance.UpdateTrayMap(uptMap);
            UptCellNotityUi(testResults.CellID, testResults.TrayCode, DeviceStatus.Normal);
        }

        /// <summary>
        /// 获取测试结果
        /// </summary>
        /// <param name="trayCode"></param>
        /// <param name="cellID"></param>
        /// <returns></returns>
        private MESResults GetMESResults(string trayCode, int? cellID = null)
        {
            if (trayCode.IsEmpty())
                return default;
            var hasCache = TestResults.Where(t => t.TrayCode == trayCode).OrderByDescending(t => t.CreateTime).FirstOrDefault();
            if (hasCache != null)
                return hasCache;
            var hasAccess = Business.Instance.GetTrayMap(trayCode, cellID);
            return (hasAccess?.bts_result).JsonDecode<MESResults>();
        }
        #endregion

        #region 持久化任务
        /// <summary>
        /// 将未完成的静置任务，接着初始化执行
        /// </summary>
        private void HasNoComplete()
        {
            var opRes = Business.Instance.GetInitStandTaskAsync();
            var hasMany = opRes.Result;
            if (!hasMany.IsEmpty() && hasMany.Any())
            {
                Task.Run(() =>
                {
                    hasMany.AsParallel().ForAll(t =>
                    {
                        var hasTask = Business.Instance.GetTrayMap(t.tray_code, t.cell_id);
                        if (hasTask != null && hasTask.exec_status == (int)DeviceStatus.Normal)
                        {
                            var jobKey = string.Format(Configs.jobKey, t.tray_code, t.processes, t.cell_id);
                            var showMsg = string.Format(Configs.preTitle, t.tray_code, t.processes, t.cell_id) + $"重新加入到上传MES队列中...";
                            CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Info);
                        }
                    });
                });
            }
        }
        #endregion

        #region 校验

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
        }
        /// <summary>
        /// 本地库位ID => MES库位ID
        /// </summary>
        public Dictionary<string, int> ToMESCellMap { get; set; } = new Dictionary<string, int>();

        private void CellIdMapToMES()
        {
            try
            {
                string hasPath = AppDomain.CurrentDomain.BaseDirectory + "CellMap\\CellMapToMES.json";
                string hasValue = System.IO.File.ReadAllText(hasPath);
                ToMESCellMap = hasValue.JsonDecode<Dictionary<string, int>>();
            }
            catch (Exception)
            {
                var showMsg = $"获取库位映射关系失败，找不到CellMapToMES.json文件。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
            }
        }

        /// <summary>
        /// MES 库位ID => （BTS\PLC）本地库位库位ID
        /// </summary>
        public Dictionary<int, int> ToLocalCellMap { get; set; } = new Dictionary<int, int>();

        /// <summary>
        /// Plc => MES 库位映射
        /// </summary>
        public Dictionary<int, int> PlcToMes { get; set; } = new Dictionary<int, int>();

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
                PlcToMes = ToLocalCellMap.ToDictionary(kv => kv.Value, kv => kv.Key);
            }
            catch (Exception)
            {
                var showMsg = $"获取库位映射关系失败，找不到CellMapToBTS.json文件。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
            }
        }

        /// <summary>
        /// MES的库位ID => 本地库位ID
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        private int GetCellForPLC(int cellID)
        {
            if (!ToLocalCellMap.ContainsKey(cellID))
            {
                var showMsg = $"MES库位【{cellID}】，找不到对应本地的库位，请检查配置是否有误。";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Error);
                return -1;
            }
            return ToLocalCellMap[cellID];
        }

        /// <summary>
        /// PLC对应的库位ID => MES库位ID
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        private int PlcCellToMes(int cellID)
        {
            return PlcToMes.ContainsKey(cellID) ? PlcToMes[cellID] : -1;
        }
        #endregion

        #region 扩展方法
        
        #endregion

        #region 消防/故障
        /// <summary>
        /// 消防描述
        /// </summary>
        private Dictionary<ErrorType, string> faultMap = new Dictionary<ErrorType, string>
        {
            {ErrorType.Ok, "程序正常"},
            {ErrorType.FalutStop, "设备故障停机"},
            {ErrorType.Byhand, "压床处于手动状态"},
            {ErrorType.Scram, "压床急停中"},
            {ErrorType.Reverse, "压床料盘反向放置"},
            {ErrorType.ValveFailure, "阀门故障"},
            {ErrorType.ExcFailure, "电源工步执行失败"},
            {ErrorType.FireAlarm, "压床火警报警"},
            {ErrorType.YV1, "门YV1阀开关不到位故障"},
            {ErrorType.YV2, "模组YV2阀开关不到位故障"},
            {ErrorType.YV3, "定位YV3阀开关不到位故障"},
            {ErrorType.YV4, "探针YV4阀开关不到位故障"},
            {ErrorType.PreeWindFault, "压床风扇故障"},
            {ErrorType.SpeedFault, "风道提速风扇故障"},
            {ErrorType.WindLevelFault, "风道风扇故障"},
        }; 

        /// <summary>
        /// 初始化消防检查
        /// </summary>
        private void InitFireCheck()
        {
            GetFireSetByMes();
            QuartzHelper.CommonAddJob<FireJob>(Configs.fireName, Configs.startFire, Configs.intervalFire);
            QuartzHelper.CommonAddJob<FaultJob>(Configs.faultName, Configs.startFire, Configs.intervalFault);
        }

        /// <summary>
        /// 消防判断动作
        /// </summary>
        public void DoFireHnadle()
        {
            if(Configs.IsOpenSmoke)
                HasFireByLocal();
        }

        /// <summary>
        /// 判断感光纤维
        /// </summary>
        public void DoFalutHandle() 
        {
            if(Configs.IsOpenMBus)
                HasFireByModBus();
        }

        #region 故障
        /// <summary>
        /// 批量读取故障信号点
        /// </summary>
        /// <returns></returns>
        private void HasFaultAnomaly()
        {
            var busRes = DEV.Instance.CommonReadByBulk(SignalType.R_FaultFlag);
            if (busRes == null)
                return;
            var hasFault = GetFireModelsByFault(busRes);
            hasFault?.ForEach(t =>
            {
                var showMsg = $"库位【{t.CellId}】提示有故障：{t.Message}";
                CORE.Instance.OnMessage(showMsg, BatMes.Client.Enums.SysEventLevel.Warn);
            });
        }
        #endregion

        /// <summary>
        /// 判断本地是否存在烟雾报警
        /// </summary>
        private void HasFireByLocal()
        {
            var busRes = DEV.Instance.HasSmoke();
            if (!busRes.Any())
                return;
            var hasFire = GetFireModelsBySmoke(busRes);
            HasFireNotifyMES(hasFire);
        }

        /// <summary>
        /// 判断ModBus是否存在温度异常
        /// </summary>
        private void HasFireByModBus()
        {
            var busRes = DEV.Instance.BulkGetByModBus();
            if (busRes == null)
                return;
            var hasFire = GetFireModelsByMBus(busRes);
            HasFireNotifyMES(hasFire);
        }

        /// <summary>
        /// 判断是否存在故障
        /// </summary>
        /// <param name="sourceRes"></param>
        /// <returns></returns>
        private List<FireModel> GetFireModelsByFault(byte[] sourceRes)
        {
            if (sourceRes == null)
                return default;
            List<FireModel> fireModels = new List<FireModel>();
            var startCount = 1;
            for (int i = 0; i < sourceRes.Length; i += 2)
            {
                var falutVal = BitConverter.ToUInt16(sourceRes, i);
                if(falutVal > 0)
                {
                    var hasMap = (ErrorType)falutVal;
                    var fireModel = new FireModel
                    {
                        CellId = PlcCellToMes(startCount),
                        Message = faultMap.ContainsKey(hasMap) ? faultMap[hasMap] : "未知错误",
                    };
                    fireModels.Add(fireModel);
                }
                startCount++;
            }
            return fireModels;
        }

        /// <summary>
        /// 判断是否存在温度异常
        /// </summary>
        /// <param name="sourceRes"></param>
        /// <returns></returns>
        private List<FireModel> GetFireModelsByMBus(byte[] sourceRes)
        {
            if (sourceRes == null)
                return default;
            List<FireModel> fireModels = new List<FireModel>();
            var startCount = 1;
            for (int i = 0; i < sourceRes.Length; i += 2)
            {
                var falutVal = BitConverter.ToInt16(sourceRes, i);
                var uploadVal = decimal.Parse(falutVal.ToStringEx());
                if (uploadVal > 0 && Configs.FireWarnTemp > 0 && uploadVal >= Configs.FireWarnTemp)
                {
                    var fireModel = new FireModel
                    {
                        CellId = PlcCellToMes(startCount),
                        FireType = FireType.FireTemp,
                        Temp = uploadVal
                    };
                    fireModels.Add(fireModel);
                }
                startCount++;
            }
            return fireModels;
        }

        /// <summary>
        /// 判断是否存在烟雾报警
        /// </summary>
        /// <param name="sourceRes"></param>
        /// <returns></returns>
        private List<FireModel> GetFireModelsBySmoke(Dictionary<int, bool> sourceRes)
        {
            if (sourceRes == null)
                return default;
            List<FireModel> fireModels = new List<FireModel>();
            sourceRes.ForEach(t =>
            {
                if (!t.Value)
                    return;
                var newModel = new FireModel
                {
                    CellId = PlcCellToMes(t.Key),
                    FireType = FireType.Smoke
                };
                fireModels.Add(newModel);
            });
            return fireModels;
        }

        /// <summary>
        /// 获取MES上设置的消防温度阈值
        /// </summary>
        private void GetFireSetByMes()
        {
            var procInfo = MesHelper.Instance.GetProcInfo(Configs.procID);
            if (procInfo.IsEmpty())
                return;
            Configs.FireWarnTemp = procInfo.FireWarnTemp;
            Configs.FireActTemp = procInfo.FireActTemp;
        }

        /// <summary>
        /// 若有火警，则告知MES
        /// </summary>
        /// <param name="fireModels"></param>
        private void HasFireNotifyMES(List<FireModel> fireModels)
        {
            fireModels?.AsParallel().ForAll(t =>
            {
                if(t.FireType == FireType.Smoke)
                {
                    var opFlag = MesHelper.Instance.CallFireSmoke(t.CellId);
                    if (opFlag == BatMes.Enums.DeviceFireMode.Spray)
                        ComfirmOpenFire(t.CellId);
                } 
                else if(t.FireType == FireType.FireTemp)
                {
                    var opFlag = MesHelper.Instance.CallFireTemp(t.CellId, t.Temp);
                    if (opFlag == BatMes.Enums.DeviceFireMode.Spray)
                        ComfirmOpenFire(t.CellId);
                }   
            });
        }

        /// <summary>
        /// 判断是否打开消防
        /// </summary>
        /// <param name="cellID"></param>
        private void ComfirmOpenFire(int cellID)
        {
            DEV.Instance.WriteBool(SignalType.OR_OpenFire, GetCellForPLC(cellID));
            var waitVal = DEV.Instance.WaitBool(SignalType.W_ConfirmFire, GetCellForPLC(cellID));
            if (waitVal)
            {
                DEV.Instance.NoticeSpray(GetCellForPLC(cellID));
                CORE.Instance.OnMessage($"库位【{cellID}】发生火警，满足喷淋条件，已通知消防柜。", BatMes.Client.Enums.SysEventLevel.Info);
            }
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
        private void UptCellNotityUi(int cellID, string trayCode, DeviceStatus execStatus, BatMes.Enums.TrayAttr trayAttr = BatMes.Enums.TrayAttr.Battery, bool maybeAdd = false)
        {
            var hasCell = Business.Instance.GetCellByID(cellID);
            hasCell = hasCell ?? new cell { cell_id = cellID };
            hasCell.cell_status = (int)execStatus;
            hasCell.last_update_time = DateTime.Now;
            hasCell.type = maybeAdd ? (int)trayAttr : hasCell.type;
            var uptFlag = Business.Instance.AddOrUpdateCell(hasCell);
            if (uptFlag)
                PublicDel.AutoUpdateCellStatus?.Invoke(hasCell, trayCode);
        }

        /// <summary>
        /// 通知PLC复位库位
        /// </summary>
        /// <param name="cellNo"></param>
        /// <returns></returns>
        public (bool isSuccess, string strMsg) ResetCell(int cellNo)
        {
            Task.Run(() =>
            {
                var opVal = DEV.Instance.WriteBool(SignalType.W_FalutReset, GetCellForPLC(cellNo));
                var doRes = opVal ? $"库位【{cellNo}】已通知压床复位" : $"库位【{cellNo}】手工复位失败：写入PLC失败";
                CORE.Instance.OnMessage(doRes, opVal ? BatMes.Client.Enums.SysEventLevel.Info : BatMes.Client.Enums.SysEventLevel.Warn);
            });
            return default;
        }

        /// <summary>
        /// 更新工步文件
        /// </summary>
        /// <param name="cellNo"></param>
        /// <returns></returns>
        public (bool isSuccess, string strMsg) ReflushFileCell(int cellNo)
        {
            Task.Run(() =>
            {
                try
                {
                    var hasModle = GetTrayModelByCell(cellNo);
                    if(hasModle != null)
                    {
                        var opsFile = MesHelper.Instance.ProcessFile(hasModle.OpsValue, hasModle.Times);
                        hasModle.FilePath = opsFile;
                        var uptModel = new tray_map
                        {
                            cell_id = cellNo,
                            mes_model = hasModle.JsonEncode(),
                            last_update_time = DateTime.Now
                        };
                        Business.Instance.UpdateTrayModel(uptModel);
                        CORE.Instance.OnMessage($"库位【{cellNo}】更新工步文件成功。", BatMes.Client.Enums.SysEventLevel.Info);
                    }
                    else
                    {
                        CORE.Instance.OnMessage($"更新工步文件失败：库位【{cellNo}】找不到对应的托盘模型。", BatMes.Client.Enums.SysEventLevel.Error);
                    }
                }
                catch (Exception ex)
                {
                    CORE.Instance.OnMessage($"更新工步文件异常：{ex.Message}", BatMes.Client.Enums.SysEventLevel.Error);
                }
            });
            return default;
        }


        /// <summary>
        /// 手工请求切换库位
        /// </summary>
        /// <param name="cellNo"></param>
        /// <returns></returns>
        public (bool isSuceess, string strMsg) OutChangeByHand(int cellNo)
        {
            Task.Run(() =>
            {
                try
                {
                    var hasModle = GetTrayModelByCell(cellNo);
                    if (hasModle != null)
                    {
                        var opVal = MesHelper.Instance.OutChange(hasModle.CellID, hasModle.OpsValue, hasModle.TrayCode);
                        if(opVal)
                            CORE.Instance.OnMessage($"库位【{cellNo}】- 托盘【{hasModle.TrayCode}】已通知调度换库位。", BatMes.Client.Enums.SysEventLevel.Info);
                    }
                    else
                    {
                        CORE.Instance.OnMessage($"切换库位失败：库位【{cellNo}】找不到对应的托盘模型。", BatMes.Client.Enums.SysEventLevel.Error);
                    }
                }
                catch (Exception ex)
                {
                    CORE.Instance.OnMessage($"切换库位异常：{ex.Message}", BatMes.Client.Enums.SysEventLevel.Error);
                }
            });
            return default;
        }

        /// <summary>
        /// 手工请求出盘，当NG处理
        /// </summary>
        /// <param name="cellNo"></param>
        /// <returns></returns>
        public (bool isSuceess, string strMsg) OutUploadByHand(int cellNo)
        {
            Task.Run(() =>
            {
                try
                {
                    var hasModle = GetTrayModelByCell(cellNo);
                    if (hasModle != null)
                    {
                        var opVal = MesHelper.Instance.OutUnload(hasModle.CellID, hasModle.OpsValue, hasModle.TrayAttr, hasModle.TrayCode);
                        if (opVal)
                            CORE.Instance.OnMessage($"库位【{cellNo}】- 托盘【{hasModle.TrayCode}】已通知调度出盘。", BatMes.Client.Enums.SysEventLevel.Info);
                    }
                    else
                    {
                        CORE.Instance.OnMessage($"请求出盘失败：库位【{cellNo}】找不到对应的托盘模型。", BatMes.Client.Enums.SysEventLevel.Error);
                    }
                }
                catch (Exception ex)
                {
                    CORE.Instance.OnMessage($"请求出盘失败：{ex.Message}", BatMes.Client.Enums.SysEventLevel.Error);
                }
            });
            return default;
        }
        #endregion
    }
}
