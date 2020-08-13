using System.Drawing;

namespace BatMes.Client
{
    /// <summary>
    /// 自定义颜色
    /// </summary>
    public static class CustomColor
    {
        /// <summary>
        /// 主蓝（主色调，用于头、尾、按钮背景等）
        /// </summary>
        public static readonly Color Blue = Color.FromArgb(0, 125, 184);

        /// <summary>
        /// 天蓝（颜色淡于主蓝，用于表头）
        /// </summary>
        public static readonly Color BlueSky = Color.FromArgb(160, 215, 233);

        /// <summary>
        /// 浅蓝（颜色淡于天蓝，用于文字背景）
        /// </summary>
        public static readonly Color BlueLight = Color.FromArgb(236, 247, 247);

        /// <summary>
        /// 深红（用于警示文字、按钮等）
        /// </summary>
        public static readonly Color Red = Color.FromArgb(202, 23, 44);

        /// <summary>
        /// 深绿（用于状态文字）
        /// </summary>
        public static readonly Color Green = Color.FromArgb(25, 132, 0);

        /// <summary>
        /// 草绿（颜色淡于深绿，用于状态背景）
        /// </summary>
        public static readonly Color GreenGrass = Color.FromArgb(110, 162, 4);

        /// <summary>
        /// 亮黄（用于数据列表交替背景）
        /// </summary>
        public static readonly Color YellowLight = Color.FromArgb(255, 255, 238);

        /// <summary>
        /// 浅灰（用于次要文本）
        /// </summary>
        public static readonly Color GrayLight = Color.FromArgb(153, 153, 153);
    }
}
