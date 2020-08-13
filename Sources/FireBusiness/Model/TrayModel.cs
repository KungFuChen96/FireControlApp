using Neware.BTS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    /// <summary>
    /// 托盘模型
    /// </summary>
    public class TrayModel
    {
        /// <summary>
        /// 托盘编码
        /// </summary>
        public string TrayCode { get; set; }

        /// <summary>
        /// 托盘属性
        /// </summary>
        public BatMes.Enums.TrayAttr TrayAttr { get; set; }

        /// <summary>
        /// 库位Id
        /// </summary>
        public int CellID { get; set; }

        /// <summary>
        /// 工序Id
        /// </summary>
        public int OpsValue { get; set; }

        /// <summary>
        /// 电芯条码（从1开始的序号与电芯条码列表）
        /// </summary>
        public Dictionary<int, string> BatteryCodes { get; set; }

        /// <summary>
        /// 通道状态
        /// </summary>
        public Dictionary<int, BatMes.Enums.ChannelStatus> Channels { get; set; }

        /// <summary>
        /// 工步文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件存储类型
        /// </summary>
        public FilePathType FilePathType { get; set; } = FilePathType.Local;

        /// <summary>
        /// 工步逻辑
        /// </summary>
        public FileSetting FileSetting { get; set; } = default;

        /// <summary>
        /// 测试次数（0:无效,1:常规测试，2:复测）
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 其他消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 用户Id 非必填
        /// </summary>
        public int UserId { get; set; }
    }
}
