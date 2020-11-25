using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    /// <summary>
    /// 库位模型接口
    /// </summary>
    public class ICellEntity
    {
        /// <summary>
        /// 库位编号
        /// </summary>
        public int CellNo { get; set; }

        /// <summary>
        /// 行
        /// </summary>
        public int RowVal { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public int ColVal { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        public int LayVal { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Massage { get; set; }
    }
}
