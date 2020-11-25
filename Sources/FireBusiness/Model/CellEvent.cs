using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    public class CellEvent
    {
        #region 分容
        /// <summary>
        /// 右键修改库位状态事件
        /// </summary>
        public EventHandler EditCellClick { get; set; }

        /// <summary>
        /// 右键刷新库位事件
        /// </summary>
        public EventHandler EditReflushClick { get; set; }

        /// <summary>
        /// 库位符文事件
        /// </summary>
        public EventHandler ResetClick { get; set; }

        /// <summary>
        /// 切换库位事件
        /// </summary>
        public EventHandler OutChangeClick { get; set; }

        /// <summary>
        /// 请求出盘事件
        /// </summary>
        public EventHandler OutUnloadClick { get; set; }

        /// <summary>
        /// 出盘完成事件
        /// </summary>
        public EventHandler OutFinishClick { get; set; }

        /// <summary>
        /// 进盘完成事件
        /// </summary>
        public EventHandler InFinishClick { get; set; }
        #endregion


        #region 消防
        /// <summary>
        /// 恢复正常状态事件
        /// </summary>
        public EventHandler BackNormalClick { get; set; }

        /// <summary>
        /// 取消喷淋事件
        /// </summary>
        public EventHandler CancelSprayClick { get; set; }

        /// <summary>
        /// 喷淋事件
        /// </summary>
        public EventHandler DoSprayClick { get; set; }

        /// <summary>
        /// 喷淋过程中停止喷淋
        /// </summary>
        public EventHandler StopSprayClick { get; set; }
        #endregion

    }
}
