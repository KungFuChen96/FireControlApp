using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Enums
{
    /// <summary>
    /// PLC信号类型
    /// </summary>
    public enum SignalType
    {
        #region 写
        /// <summary>
        /// 放盘完成
        /// </summary>
        W_InFinish = 1,

        /// <summary>
        /// 入盘规格
        /// </summary>
        W_InType = 2,

        /// <summary>
        /// 故障复位
        /// </summary>
        W_FalutReset = 3,

        /// <summary>
        /// 出盘完成
        /// </summary>
        W_OutFinish = 4,

        /// <summary>
        /// 确认开启消防
        /// </summary>
        W_ConfirmFire = 5,
        #endregion

        #region 批量读
        /// <summary>
        /// 压床请求放盘信号
        /// </summary>
        R_RequestPress = 6,

        /// <summary>
        /// 正常运行信号
        /// </summary>
        R_RunningFlag = 7,

        /// <summary>
        /// 故障代码
        /// </summary>
        R_FaultFlag = 8,

        /// <summary>
        /// 请求出盘
        /// </summary>
        R_RequestOut = 9,

        /// <summary>
        /// 请求打开消防
        /// </summary>
        R_OpenFire = 10,
        #endregion

        #region 单个读
        /// <summary>
        /// 压床请求放盘信号
        /// </summary>
        OR_RequestPress = 11,

        /// <summary>
        /// 正常运行信号
        /// </summary>
        OR_RunningFlag = 12,

        /// <summary>
        /// 故障代码
        /// </summary>
        OR_FaultFlag = 13,

        /// <summary>
        /// 请求出盘
        /// </summary>
        OR_RequestOut = 14,

        /// <summary>
        /// 请求打开消防
        /// </summary>
        OR_OpenFire = 15,
        #endregion

        #region 烟雾报警
        /// <summary>
        /// 有烟雾报警
        /// </summary>
        R_HasSmoke = 16
        #endregion
    }
}
