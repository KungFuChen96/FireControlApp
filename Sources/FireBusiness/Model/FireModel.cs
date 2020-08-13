using FireBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    /// <summary>
    /// 消防信号点模型
    /// </summary>
    public class FireModel
    {
        /// <summary>
        /// 第几行
        /// </summary>
        public int RowValue { get; set; }

        /// <summary>
        /// 第几列
        /// </summary>
        public int ColValue { get; set; }

        /// <summary>
        /// 第几层
        /// </summary>
        public int LayerValue { get; set; }

        /// <summary>
        /// 信号点
        /// </summary>
        public string SignalPoints { get; set; }

        /// <summary>
        /// 是否发生火警
        /// </summary>
        public bool IsFire { get; set; } = false;

        /// <summary>
        /// 对应上传MES的库位Id
        /// </summary>
        public int CellId { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public FireType FireType { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public decimal Temp { get; set; }

        /// <summary>
        /// 其他消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enabled { get; set; } = true;
    }
}
