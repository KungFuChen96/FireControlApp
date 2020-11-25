using FireBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Model
{
    /// <summary>
    /// 库位消防模型
    /// </summary>
    public class FireControlModel: ICellEntity
    {
        /// <summary>
        /// 消防位置
        /// </summary>
        public FirePost FirePost { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public DeviceStatus DeviceStatus { get; set; }

        private FireAction _fireAction = FireAction.Nothing;
        /// <summary>
        /// 若发生火警，调度通知的执行动作
        /// </summary>
        public FireAction FireAction
        {
            get
            {
                return _fireAction;
            }
            set
            {
                if (_fireAction == FireAction.Nothing && value == FireAction.Spray)
                {
                    AllowTime = DateTime.Now;
                    _fireAction = value;
                }
                //else if (_fireAction == FireAction.Spray && value == FireAction.Nothing)
                //{
                //   // _fireAction = FireAction.Spray;
                //}  
            }
        }

        /// <summary>
        /// 设备温度
        /// </summary>
        public decimal? DevTemp { get; set; }

        /// <summary>
        /// 最早创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 手工执行消防
        /// </summary>
        public bool ByHand { get; set; } = false;

        /// <summary>
        /// 是否已经执行消防动作
        /// </summary>
        public bool IsAction { get; set; } = false;

        /// <summary>
        /// 取消执行动作
        /// </summary>
        public bool IsCancel { get; set; } = false;

        /// <summary>
        /// 运行执行消防时间
        /// </summary>
        public DateTime AllowTime { get; set; } = DateTime.Now.AddDays(5);

        /// <summary>
        /// 若发生火警，无人工多久自动执行消防
        /// </summary>
        public int WithoutByHand { get; set; }

        /// <summary>
        /// 针对分容库位，该库位是否有烟雾报警。因为需求变化比较多，只能加这个字段来区分压床库位是否有发生过烟雾报警。
        /// </summary>
        public bool CellHasSmoke { get; set; }

        /// <summary>
        /// 针对分容库位，超温的点又多少个，一个库位有12个点
        /// </summary>
        public int TooHotCount { get; set; }

        /// <summary>
        /// 是否可以执行消防动作
        /// </summary>
        public bool Enabled 
        {
            get 
            {
                //新需求：手工的优先级最高 By 20201008
                /*
                 * 新需求：By 20201028
                 * 静置架不自动喷淋，只有手工喷淋 
                 * 分容库位满足喷淋条件时，可以自动喷淋也可以手工喷淋
                 * 判断条件不在获取MES，而是在本地判断
                */

                /*
                 * 这个字段目前仅用于分容库位的自动喷淋，因静置架已改成手工喷淋，不会自动喷淋。
                 */

                //var required = FirePost == FirePost.Fc && FireAction != FireAction.Nothing && !IsCancel;
                //return ByHand || (required && ((DateTime.Now - AllowTime).TotalSeconds > WithoutByHand));

                var mustVal = FirePost == FirePost.Fc
                              && TooHotCount > 0
                              && FireAction == FireAction.Spray
                              && !IsCancel;
                return mustVal && ((DateTime.Now - AllowTime).TotalSeconds > WithoutByHand);
            }
        }
    }
}
