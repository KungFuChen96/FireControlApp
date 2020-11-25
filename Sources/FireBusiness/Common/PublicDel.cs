using BatMes.Client.Entity.batmes_client;
using FireBusiness.Enums;
using System;

namespace FireBusiness
{
    /// <summary>
    /// 公共的委托
    /// </summary>
    public class PublicDel
    {
        #region 分容
        /// <summary>
        /// 库位状态变化
        /// </summary>
        public static Action<cell> OnUpdateCellStatus;

        /// <summary>
        /// 自动化流程中更新库位状态
        /// 第二个参数仅用于更新备注，默认请给NULL
        /// </summary>
        public static Action<cell, string> AutoUpdateCellStatus;

        /// <summary>
        /// 实时获取静置架的温度
        /// </summary>
        public static Action<cell, decimal> UptTempByStandby;

        /// <summary>
        /// 更新工步
        /// </summary>
        public static Action<int> OnReflushFile;

        /// <summary>
        /// 库位复位
        /// </summary>
        public static Action<int> OnResetCell;

        /// <summary>
        /// 手工取走托盘
        /// </summary>
        public static Action<int> OnOutFinish;

        /// <summary>
        /// 手工下发压合
        /// </summary>
        public static Action<int> OnInFinish;

        /// <summary>
        /// 请求换库位
        /// </summary>
        public static Action<int> OnOutChange;

        /// <summary>
        /// 请求出盘
        /// </summary>
        public static Action<int> OnOutUnload;
        #endregion

        #region 消防
        /// <summary>
        /// 恢复正常状态
        /// </summary>
        public static Action<int, string> BackNormal;

        /// <summary>
        /// 取消喷淋
        /// </summary>
        public static Action<int, string> CancelSpray;

        /// <summary>
        /// 喷淋
        /// </summary>
        public static Action<int, string, FirePost> DoSpray;

        /// <summary>
        /// 若喷淋过程发生了，可以将喷淋停止
        /// </summary>
        public static Action<int, string, FirePost> StopSpray;
        #endregion

        #region 温度
        /// <summary>
        /// 实时获取静置架的温度
        /// </summary>
        public static Action<int, decimal> AutoGetTempByStandby;
        #endregion
    }
}
