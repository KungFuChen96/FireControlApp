using System.ServiceModel;

namespace IExtend.Interface
{
    /// <summary>
    /// 调度系统必须实现的接口
    /// </summary>
    [ServiceContract]
    public interface IStandyService
    {
        /// <summary>
        /// 进盘完成通知
        /// </summary>
        /// <param name="trayNotify"></param>
        /// <returns></returns>
        [OperationContract]
        CallResult<bool> IntoTrayFinish(TrayNotify trayNotify);
    }
}
