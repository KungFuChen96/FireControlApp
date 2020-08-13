namespace BatMes.Client.Enums
{
    /// <summary>
    /// 温度单位
    /// </summary>
    public enum TempUnit
    {
        /// <summary>
        /// ℃
        /// </summary>
        C = 1
    }

    public static partial class Tools
    {
        /// <summary>
        /// 温度单位符号
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string TempUnit(int val)
        {
            switch (val)
            {
                case 1:
                    return "℃";
                default:
                    return "";
            }
        }
    }

}
