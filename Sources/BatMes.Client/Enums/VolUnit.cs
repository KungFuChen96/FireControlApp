namespace BatMes.Client.Enums
{
    /// <summary>
    /// 电压单位
    /// </summary>
    public enum VolUnit
    {
        /// <summary>
        /// mV
        /// </summary>
        MV = 1,

        /// <summary>
        /// V
        /// </summary>
        V = 2
    }

    public static partial class Tools
    {
        /// <summary>
        /// 电压单位符号
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string VolUnit(int val)
        {
            switch (val)
            {
                case 1:
                    return "mV";
                case 2:
                    return "V";
                default:
                    return "";
            }
        }
    }
}
