using System.Runtime.Serialization;

namespace IExtend.Enums
{
    /// <summary>
    /// 服务调用结果代码
    /// </summary>
    [DataContract]
    public enum ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [EnumMember]
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        [EnumMember]
        Failure = 1,

        /// <summary>
        /// 异常
        /// </summary>
        [EnumMember]
        Exception = 2
    }
}
