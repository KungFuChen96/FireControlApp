using CCWin.SkinClass;
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
            { DeviceStatus.FireAlarm, Color.Red },
            { DeviceStatus.Fault, Color.Snow },
            { DeviceStatus.OtherEx, Color.Snow },
        };

        /// <summary>
        /// 库位状态描述
        /// </summary>
        public static readonly Dictionary<DeviceStatus, string> Descriptions = new Dictionary<DeviceStatus, string>()
        {
            {DeviceStatus.Normal, "正常"},
            {DeviceStatus.TempAnomaly, "温度异常"},
            {DeviceStatus.FireAlarm, "烟雾警告"},
            {DeviceStatus.Fault, "设备故障"},
            {DeviceStatus.OtherEx, "其他异常"}
        };
        #endregion

        #region 产线描述
        /// <summary>
        /// 第一行产线有多少列（包括title）
        /// </summary>
        public static int ColCountLine_1 => ConfigurationManager.AppSettings["ColCountLine_1"].ToInt32();

        /// <summary>
        /// 第一行产线有多少行（层）
        /// </summary>
        public static int RowCountLine_1 => ConfigurationManager.AppSettings["RowCountLine_1"].ToInt32();

        /// <summary>
        /// 第二行产线有多少列（包括title）
        /// </summary>
        public static int ColCountLine_2 => ConfigurationManager.AppSettings["ColCountLine_2"].ToInt32();

        /// <summary>
        /// 第二行产线有多少行（层）
        /// </summary>
        public static int RowCountLine_2 => ConfigurationManager.AppSettings["RowCountLine_2"].ToInt32();
        #endregion

        #region 其他
        /// <summary>
        /// 行列层格式化
        /// </summary>
        public static string RclKey => "{0}-{1}-{2}";

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
        #endregion
    }
}
