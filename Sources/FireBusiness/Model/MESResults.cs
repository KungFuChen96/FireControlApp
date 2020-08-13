using Neware.BTS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    /// <summary>
    /// 测试结果， 用于上传MES
    /// </summary>
    public class MESResults
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
        /// 第几次测试
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; } = 0;

        /// <summary>
        /// BTS响应的数据
        /// </summary>
        public TestResult TestResult { get; set; }

        /// <summary>
        /// 电芯测试数据
        /// </summary>
        public List<ComleteModel> ComleteModels { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
