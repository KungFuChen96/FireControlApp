using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IExtend.Enums
{
    /// <summary>
    /// 静置任务状态
    /// </summary>
    public enum StandTaskStatus
    {
        /// <summary>
        /// 运行中
        /// </summary>
        Running = 0,

        /// <summary>
        /// 完成
        /// </summary>
        Finish = 1,

        /// <summary>
        /// 异常
        /// </summary>
        Exception = 2,
    }
}
