using System;
using System.ServiceModel;
using FireBusiness.Tool;
using FireBusiness.OutSystem;
using BatMes.Service.Device.Standby;
using IExtend.Interface;
using BatMes.Service.Device.Fc;
using Neware.BTS.Service;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FireBusiness.WCF
{
    public class WcfService
    {
        #region 开启/关闭WCF服务

        private static ServiceHost _wcfMESHost = null;
        public static void WcfServiceForMES(string tcpHost, string httpHost)
        {
            try
            {
                if (_wcfMESHost != null && _wcfMESHost.State != CommunicationState.Faulted)
                    _wcfMESHost.Close();
                MesService mesService = new MesService();
                _wcfMESHost = new ServiceHost(mesService, new Uri(httpHost));
                //配置终结点
                NetTcpBinding netTcpBinding = new NetTcpBinding()
                {
                    MaxBufferSize = int.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue,
                };
                netTcpBinding.Security.Mode = SecurityMode.None;
                netTcpBinding.TransferMode = TransferMode.Buffered;
                _wcfMESHost.AddServiceEndpoint(
                    typeof(IFcService),
                    netTcpBinding,
                    tcpHost);
                _wcfMESHost.Open();
                //将接口实现和契约挂钩
                mesService.InAuthEvent += MesService_InAuthEvent; 
                mesService.InFinishedEvent += MesService_InFinishedEvent; 
                mesService.OutAuthEvent += MesService_OutAuthEvent; 
                mesService.OutFinishedEvent += MesService_OutFinishedEvent; 
                CORE.Instance.OnMessage($"WCF服务开启成功。", BatMes.Client.Enums.SysEventLevel.Info);
            }
            catch (Exception ex)
            {
                CORE.Instance.OnMessage($"WCF_FOR_MES服务:{tcpHost}，开启失败: {ex.Message}", BatMes.Client.Enums.SysEventLevel.Error);
            }
        }

        public static bool InitWcfService(string tcpHost, string httpHost)
        {
            try
            {
                _wcfMESHost?.Close();
                _wcfMESHost = WcfTool.CreateWcfHost(typeof(IStandbyService), typeof(WcfService), tcpHost, httpHost);
                _wcfMESHost.Open();
                CORE.Instance.OnMessage($"WCF服务开启成功。", BatMes.Client.Enums.SysEventLevel.Info);
                return true;
            }
            catch (Exception ex)
            {
                CORE.Instance.OnMessage($"WCF服务:{tcpHost}，开启失败: {ex.Message}", BatMes.Client.Enums.SysEventLevel.Error);
                return false;
            }
        }

        public static void CloseWcfService()
        {
            try
            {
                if(_wcfMESHost != null && _wcfMESHost.State != CommunicationState.Faulted)
                    _wcfMESHost.Close();
                _wcfMESHost = null;
            }
            catch (Exception ex)
            {
                CORE.Instance.OnMessage($"WCF服务关闭失败: {ex.Message}", BatMes.Client.Enums.SysEventLevel.Error);
            }
        }
        #endregion 开启WCF服务

        #region 实现接口委托

        #region MES
        /// <summary>
        /// 处理出盘完成事件
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        private static bool MesService_OutFinishedEvent(int cellID, string trayCode)
        {
            return default;
        }

        /// <summary>
        /// 处理出盘授权事件
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        private static bool MesService_OutAuthEvent(int cellID)
        {
            return default;
        }

        /// <summary>
        /// 处理进盘完成事件
        /// </summary>
        /// <param name="trayAttr"></param>
        /// <param name="trayCode"></param>
        /// <param name="cellID"></param>
        /// <param name="opsID"></param>
        /// <returns></returns>
        private static bool MesService_InFinishedEvent(BatMes.Enums.TrayAttr trayAttr, string trayCode, int cellID, int opsID, int times)
        {
            return default;
        }

        /// <summary>
        /// 处理进盘授权事件
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        private static bool MesService_InAuthEvent(BatMes.Enums.TrayAttr trayAttr, int cellID)
        {
            return default;
        }
        #endregion

        #endregion

        #region 扩展方法
        /// <summary>
        /// CSCService通用的响应格式
        /// </summary>
        /// <returns></returns>
        private static ServiceResult ComResultJson(bool isSuccess = true, string strMsg = "")
        {
            return new ServiceResult
            {
                Code = isSuccess ? ResultCode.Success : ResultCode.Exception,
                Message = isSuccess ? string.Empty : strMsg
            };
        }
        #endregion
    }
}
