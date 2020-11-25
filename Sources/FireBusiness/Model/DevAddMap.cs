using FireBusiness.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    /// <summary>
    /// 设备地址映射模型
    /// </summary>
    public class DevAddMap
    {
        /// <summary>
        /// PLC实例: 用于读取烟雾消防信号的PLC
        /// 由于信号点很多，允许存在一个或多个PLC端口，用英文逗号分隔，提高读取速度
        /// </summary>
        public List<BatMes.Device.PLC.IPlc> ReadPlc { get; set; }

        /// <summary>
        /// 用于执行消防动作的PLC
        /// </summary>
        public BatMes.Device.PLC.IPlc ActionPlc { get; set; }

        /// <summary>
        /// 读信号映射，一对一
        /// </summary>
        public Dictionary<SignalType, Dictionary<int, BatMes.Device.PLC.IAddress>> ReadMap { get; set; } = new Dictionary<SignalType, Dictionary<int, BatMes.Device.PLC.IAddress>>();

        /// <summary>
        /// 仅用于读
        /// 定义该变量用于处理多端口多PLC对象，将信号点分片给不同PLC处理，提升读取速度（一对一）
        /// </summary>
        public ConcurrentDictionary<BatMes.Device.PLC.IPlc, IEnumerable<KeyValuePair<int, BatMes.Device.PLC.IAddress>>> ReadMapByIns { get; set; } = new ConcurrentDictionary<BatMes.Device.PLC.IPlc, IEnumerable<KeyValuePair<int, BatMes.Device.PLC.IAddress>>>();

        /// <summary>
        /// 读信号映射，一对多
        /// </summary>
        public Dictionary<SignalType, Dictionary<int, List<BatMes.Device.PLC.IAddress>>> ReadMapBulk { get; set; } = new Dictionary<SignalType, Dictionary<int, List<BatMes.Device.PLC.IAddress>>>();

        /// <summary>
        /// 仅用于读
        /// 定义该变量用于处理多端口多PLC对象，将信号点分片给不同PLC处理，提升读取速度（一对多）
        /// </summary>
        public ConcurrentDictionary<BatMes.Device.PLC.IPlc, IEnumerable<KeyValuePair<int, List<BatMes.Device.PLC.IAddress>>>> ReadMapBulkByIns { get; set; } = new ConcurrentDictionary<BatMes.Device.PLC.IPlc, IEnumerable<KeyValuePair<int, List<BatMes.Device.PLC.IAddress>>>>();

        /// <summary>
        /// 写信号映射（用于执行消防操作）
        /// </summary>
        public Dictionary<FireAction, Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)>> ActionMap { get; set; } = new Dictionary<FireAction, Dictionary<int, (BatMes.Device.PLC.IAddress AddR, ushort writeVal)>>();

        /// <summary>
        /// 单点读写布尔值(暂定用于心跳)
        /// </summary>
        public Dictionary<SignalType, (BatMes.Device.PLC.IAddress AddR, bool wVal)> OneMap { get; set; } = new Dictionary<SignalType, (BatMes.Device.PLC.IAddress AddR, bool wVal)>();

        /// <summary>
        /// （定制化需求）反馈给PLC，发生烟雾报警是哪个行列层
        /// </summary>
        public (BatMes.Device.PLC.IAddress rowAddR, BatMes.Device.PLC.IAddress colAddR, BatMes.Device.PLC.IAddress layAddR) FireRclAddR { get; set; } = default;

        /// <summary>
        /// 定制化需求 反馈给PLC喷淋是哪个行列层
        /// </summary>
        public (BatMes.Device.PLC.IAddress rowAddR, BatMes.Device.PLC.IAddress colAddR, BatMes.Device.PLC.IAddress layAddR) DoRclAddR { get; set; } = default;

        /// <summary>
        /// 因需求变化多端，故定义这个变量处理一些定制化需求，非通用化的需求
        /// </summary>
        public ConcurrentDictionary<SignalType, Dictionary<int, BatMes.Device.PLC.IAddress>> DiyMap { get; set; } = new ConcurrentDictionary<SignalType, Dictionary<int, BatMes.Device.PLC.IAddress>>();
    }
}
