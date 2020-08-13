using BatMes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    /// <summary>
    /// 上传MES接口【CompleteTesting】的模型
    /// </summary>
    public class ComleteModel
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BatteryCode { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public int OpsID { get; set; }

        /// <summary>
        /// 测试次数（1：正常测试，2：复测）
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 结果数据
        /// </summary>
        //public Dictionary<BatteryFcResultParameter, decimal> Result { get; set; } = new Dictionary<BatteryFcResultParameter, decimal>();

        /// <summary>
        /// 结果路径
        /// </summary>
        public string ResultFilePath { get; set; } = string.Empty;

        /// <summary>
        /// MES用户（可为0）
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 恒流充电时间（秒）
        /// </summary>
        public int ChargeCurrentTime { get; set; }

        /// <summary>
        /// 恒压充电时间（秒）
        /// </summary>
        public int ChargeVolTime { get; set; }

        /// <summary>
        /// 充电时间（秒）
        /// </summary>
        public int ChargeTime { get; set; }

        /// <summary>
        /// 放电时间（秒）
        /// </summary>
        public int DischargeTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
