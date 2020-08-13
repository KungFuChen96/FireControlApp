using BatMes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    /// <summary>
    /// 用于持久化上传MES的数据是否成功
    /// </summary>
    public class UpLoadModel
    { 
        /// <summary>
        /// 托盘编码
        /// </summary>
        public string TrayCode { get; set; }

        /// <summary>
        /// 库位ID
        /// </summary>
        public int CellID { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public int OpsValue { get; set; }

        /// <summary>
        /// 托盘属性
        /// </summary>
        public TrayAttr trayAttr { get; set; }

        /// <summary>
        /// timed_task表的主键
        /// </summary>
        public string TaskID { get; set; }

        /// <summary>
        /// 上传模型（默认为空）
        /// </summary>
        public MESResults MESResults { get; set; } = null;

        /// <summary>
        /// 当前出盘的托盘是否存在NG的电芯
        /// </summary>
        public bool HasNg { get; set; } = false;

        /// <summary>
        /// 数据是否已上传MES
        /// </summary>
        public bool NoticeMES { get; set; } = false;
    }
}
