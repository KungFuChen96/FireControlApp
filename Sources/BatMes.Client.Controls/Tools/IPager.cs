using System.Collections;
using System.Collections.Generic;

namespace Tools
{
    public interface IPager : IEnumerable
    {
        /// <summary>
        /// 页索引
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 页尺码
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 页总数
        /// </summary>
        int PageTotal { get; }

        /// <summary>
        /// 记录总数
        /// </summary>
        int RecordTotal { get; set; }
    }

    public interface IPager<T> : IEnumerable<T>, IPager
    {
    }
}