namespace BatMes.Client.Enums
{
    /// <summary>
    /// 系统事件级别
    /// </summary>
    public enum SysEventLevel
    {
        /// <summary>
        /// 信息
        /// </summary>
        Info = 1,

        /// <summary>
        /// 警告
        /// </summary>
        Warn = 2,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 3
    }

    public static partial class Tools
    {
        /// <summary>
        /// 系统事件级别
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string SysEventLevel(int val)
        {
            switch (val)
            {
                case 1:
                    return "信息";
                case 2:
                    return "警告";
                case 3:
                    return "错误";
                default:
                    return "";
            }
        }
    }

}
