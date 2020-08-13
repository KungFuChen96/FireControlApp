using BatMes.Client.Entity.batmes_client;
using System;

namespace FireBusiness
{
    /// <summary>
    /// 公共的委托
    /// </summary>
    public class PublicDel
    {
        /// <summary>
        /// 库位状态变化
        /// </summary>
        public static Action<cell> OnUpdateCellStatus;

        /// <summary>
        /// 自动化流程中更新库位状态
        /// </summary>
        public static Action<cell, string> AutoUpdateCellStatus;

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
    }
}
