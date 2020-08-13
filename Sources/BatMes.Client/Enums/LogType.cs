namespace BatMes.Client.Enums
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 本地
        /// </summary>
        Local = 1,

        /// <summary>
        /// 网络
        /// </summary>
        Network = 2
    }

    public static partial class Tools
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string LogType(int val)
        {
            switch (val)
            {
                case 1:
                    return "本地";
                case 2:
                    return "网络";
                default:
                    return "";
            }
        }
    }

}
