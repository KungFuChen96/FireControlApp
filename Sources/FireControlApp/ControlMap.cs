using CCWin.SkinClass;
using FireBusiness;
using FireBusiness.Enums;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace FireControlApp
{
    public class ControlMap
    {
        #region 库位状态
        /// <summary>
        /// 库位颜色映射
        /// </summary>
        public static readonly Dictionary<DeviceStatus, Color> ColorMap = new Dictionary<DeviceStatus, Color>()
        {
            { DeviceStatus.Normal, Color.Snow },
            { DeviceStatus.TempAnomaly, Color.Yellow },
            { DeviceStatus.FireAlarm, Color.FromArgb(255, 192, 192) },
            { DeviceStatus.Fault, Color.Snow },
            { DeviceStatus.OtherEx, Color.Snow },
            { DeviceStatus.SprayByHand, Color.Red }
        };

        /// <summary>
        /// 库位状态描述
        /// </summary>
        public static readonly Dictionary<DeviceStatus, string> Descriptions = new Dictionary<DeviceStatus, string>()
        {
            {DeviceStatus.Normal, "正常"},
            {DeviceStatus.TempAnomaly, "温度异常"},
            {DeviceStatus.FireAlarm, "烟雾告警"},
            {DeviceStatus.Fault, "设备故障"},
            {DeviceStatus.OtherEx, "其他异常"},
            {DeviceStatus.SprayByHand, "喷淋"}
        };

        /// <summary>
        /// 位置描述
        /// </summary>
        public static readonly Dictionary<FirePost, string> PostDescMap = new Dictionary<FirePost, string>
        {
            {FirePost.FcStandby, "常温静置架"},
            {FirePost.Fc, "分容压床"},
            {FirePost.HotStandby, "高温静置架"},
        };

        /// <summary>
        /// 位置对应的行列映射。KEY：（位置，第几行） VALUE:（列数，行数）
        /// </summary>
        public static readonly Dictionary<(FirePost, int), (int colVal, int rowVal)> PostCountMap = new Dictionary<(FirePost, int), (int colVal, int rowVal)>
        {
            { (FirePost.FcStandby, 1), (9, 7)},
            { (FirePost.FcStandby, 2), (36, 7)},
            { (FirePost.Fc, 1), (7, 4)},
            { (FirePost.HotStandby, 1), (21, 6)},
            { (FirePost.HotStandby, 2), (21, 6)}
        };
        #endregion

        #region 产线描述
        /// <summary>
        /// 第一行产线有多少列（包括title）
        /// </summary>
        //public static int ColCountLine_1 => ConfigurationManager.AppSettings["ColCountLine_1"].ToInt32();

        /// <summary>
        /// 第一行产线有多少行（层）
        /// </summary>
        //public static int RowCountLine_1 => ConfigurationManager.AppSettings["RowCountLine_1"].ToInt32();

        /// <summary>
        /// 第二行产线有多少列（包括title）
        /// </summary>
        //public static int ColCountLine_2 => ConfigurationManager.AppSettings["ColCountLine_2"].ToInt32();

        /// <summary>
        /// 第二行产线有多少行（层）
        /// </summary>
        //public static int RowCountLine_2 => ConfigurationManager.AppSettings["RowCountLine_2"].ToInt32();
        #endregion

        #region 其他
        /// <summary>
        /// 类型-行列层格式化
        /// </summary>
        public static string RclKey => "{0}-{1}-{2}-{3}";

        /// <summary>
        /// 有料状态
        /// </summary>
        public static DeviceStatus[] HasOneExecStatuses = new DeviceStatus[] { DeviceStatus.Normal};
        #endregion

        #region 定义变量
        private static int pad => 1;
        public static int radius => 4;

        public static Color foreColor => Color.FromArgb(3, 111, 125);
        public static Color baseColor => Color.Snow;
        public static Color borderColor => Color.LightGray;
        public static Color innerBorderColor => baseColor;
        public static Padding padding = new Padding(pad);
        public static Font indicatorFont = new Font("微软雅黑", 9f, FontStyle.Bold);

        public static string NoOpenStr => "";
        public static string NoOpenVal => ConfigurationManager.AppSettings["NoOpen"].ToString();
        public static string NoCol => ConfigurationManager.AppSettings["NoCol"].ToString();
        /// <summary>
        /// 手工喷淋是否需要密码
        /// </summary>
        public static bool NeedPassWord = ConfigurationManager.AppSettings["NeedPassWord"].To<bool>();
        /// <summary>
        /// 喷淋密码
        /// </summary>
        public static string Password = ConfigurationManager.AppSettings["Password"];
        #endregion
    }
}
