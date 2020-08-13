namespace BatMes.Client.Enums
{
    /// <summary>
    /// 工位类型
    /// </summary>
    public enum StationType
    {
        /// <summary>
        /// 上料
        /// </summary>
        Load = 1,

        /// <summary>
        /// 下料
        /// </summary>
        Unload = 2,

        /// <summary>
        /// 热压化成
        /// </summary>
        HotFormation = 3,

        /// <summary>
        /// 热压分容
        /// </summary>
        HotCapacity = 4,

        /// <summary>
        /// 冷压
        /// </summary>
        Cold = 5
    }
}
