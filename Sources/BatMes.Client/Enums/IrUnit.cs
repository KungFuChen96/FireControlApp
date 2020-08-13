namespace BatMes.Client.Enums
{
    /// <summary>
    /// 电阻单位
    /// </summary>
    public enum IrUnit
    {
        /// <summary>
        /// mΩ
        /// </summary>
        MO = 1,

        /// <summary>
        /// Ω
        /// </summary>
        O = 2
    }

    public static partial class Tools
    {
        /// <summary>
        /// 电阻单位符号
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string IrUnit(int val)
        {
            switch (val)
            {
                case 1:
                    return "mΩ";
                case 2:
                    return "Ω";
                default:
                    return "";
            }
        }
    }
    
}
