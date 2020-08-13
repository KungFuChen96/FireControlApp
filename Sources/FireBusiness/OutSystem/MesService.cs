using BatMes.Enums;
using BatMes.Service.Device.Fc;
using System.ServiceModel;

namespace FireBusiness.OutSystem
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MesService : IFcService
    {
        public MesService()
        {
        }

        #region 接口委托
        /// <summary>
        /// 进盘授权通知
        /// </summary>
        /// <returns></returns>
        public delegate bool InAuthDel(TrayAttr trayAttr, int cellID);
        public event InAuthDel InAuthEvent;

        /// <summary>
        /// 进盘完成通知
        /// </summary>
        /// <returns></returns>
        public delegate bool InFinishedDel(TrayAttr trayAttr, string trayCode, int cellID, int opsID, int times);
        public event InFinishedDel InFinishedEvent;

        /// <summary>
        /// 出盘授权通知
        /// </summary>
        /// <returns></returns>
        public delegate bool OutAuthDel(int cellID);
        public event OutAuthDel OutAuthEvent;

        /// <summary>
        /// 进盘完成通知
        /// </summary>
        /// <returns></returns>
        public delegate bool OutFinishedDel(int cellID, string trayCode);
        public event OutFinishedDel OutFinishedEvent;

        /// <summary>
        /// MES主动报警
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="deviceFireMode"></param>
        /// <returns></returns>
        public delegate bool FireDel(int cellID, DeviceFireMode deviceFireMode);
        public event FireDel FireEvent;
        #endregion

        #region 实现接口
        public bool OutAuth(int cellID)
        {
            return OutAuthEvent.Invoke(cellID);
        }

        public bool OutFinished(int cellID, string trayCode)
        {
            return OutFinishedEvent.Invoke(cellID, trayCode);
        }

        public bool InAuth(TrayAttr trayAttr, int cellID)
        {
            return InAuthEvent.Invoke(trayAttr, cellID);
        }

        public bool InFinished(TrayAttr trayAttr, string trayCode, int cellID, int opsID, int times)
        {
            return InFinishedEvent.Invoke(trayAttr, trayCode, cellID, opsID, times);
        }

        public bool Fire(int cellID, DeviceFireMode deviceFireMode)
        {
            return FireEvent?.Invoke(cellID, deviceFireMode) ?? default;
        }
        #endregion
    }
}
