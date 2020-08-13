using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Enums
{
    /// <summary>
    /// Quartz的任务类型
    /// </summary>
    public enum QuartzDataType
    {
        /// <summary>
        /// 静置任务
        /// </summary>
        standy = 0,

        /// <summary>
        /// 心跳任务
        /// </summary>
        hreatbeat = 1,

        /// <summary>
        /// 不包含对象，什么都没有的任务
        /// </summary>
        nothing = 2,

        /// <summary>
        /// 其他任务（带扩展）
        /// </summary>
        other = 10,
    }
}
