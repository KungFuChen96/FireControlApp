using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IExtend.Interface
{
    /// <summary>
    /// 通用的托盘通知，可以灵活赋值不同字段
    /// </summary>
    public class TrayNotify
    {
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string TrayCode { get; set; }

        /// <summary>
        /// 工序Id 
        /// </summary>
        public int? OpsId { get; set; }

        /// <summary>
        /// 库位Id
        /// </summary>
        public int? CellId { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户Id， 非必填
        /// </summary>
        public int? UserId { get; set; }
    }
}
