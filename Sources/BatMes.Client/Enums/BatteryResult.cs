namespace BatMes.Client.Enums
{
    /// <summary>
    /// 电池测试结果
    /// </summary>
    public enum BatteryResult
    {
        OK = 1,

        NG = 2
    }

    public static partial class Tools
    {
        /// <summary>
        /// 电池测试结果
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string BatteryResult(int val)
        {
            switch (val)
            {
                case 1:
                    return "OK";
                case 2:
                    return "NG";
                default:
                    return "";
            }
        }
    }

}
