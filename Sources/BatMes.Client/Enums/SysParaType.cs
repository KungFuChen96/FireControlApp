namespace BatMes.Client.Enums
{
    /// <summary>
    /// 系统参数类型
    /// </summary>
    public enum SysParaType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text = 1,

        /// <summary>
        /// 整数
        /// </summary>
        Integer = 2,

        /// <summary>
        /// 浮点数
        /// </summary>
        Decimals = 3,

        /// <summary>
        /// 布尔值
        /// </summary>
        Boolean = 4
    }

    public static partial class Tools
    {
        /// <summary>
        /// 系统参数类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string SysParaType(int val)
        {
            switch (val)
            {
                case 1:
                    return "文本";
                case 2:
                    return "整数";
                case 3:
                    return "浮点数";
                case 4:
                    return "布尔值";
                default:
                    return "";
            }
        }
    }

}
