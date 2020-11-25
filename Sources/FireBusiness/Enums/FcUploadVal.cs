using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Enums
{
    /// <summary>
    /// 分容上传MES温度类型
    /// </summary>
    public enum FcUploadVal
    {
        /// <summary>
        /// 取最高温度
        /// </summary>
        Highest = 0,

        /// <summary>
        /// 取平均温度
        /// </summary>
        Avg = 1,

        /// <summary>
        /// 其他类型
        /// </summary>
        Other = 2
    }
}
