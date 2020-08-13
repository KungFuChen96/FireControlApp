using System.Runtime.Serialization;
using IExtend.Enums;

namespace IExtend.Interface
{
    /// <summary>
    /// 服务调用结果
    /// </summary>
    [DataContract]
    public class CallResult
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        [DataMember]
        public ResultCode Code { get; set; }

        /// <summary>
        /// 结果消息
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }

    /// <summary>
    /// 服务调用结果
    /// </summary>
    [DataContract]
    public class CallResult<T> : CallResult
    {
        /// <summary>
        /// 结果数据
        /// </summary>
        [DataMember]
        public T Data { get; set; }
    }
}
