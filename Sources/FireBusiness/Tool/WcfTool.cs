using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.Tool
{
    public static class WcfTool
    {
        /// <summary>
        /// 创建WCF服务主机
        /// </summary>
        /// <param name="contractType">契约类型</param>
        /// <param name="implementationType">实现契约类型</param>
        /// <param name="tcpUrl">tcp服务地址</param>
        /// <param name="httpUrl">http服务地址</param>
        /// <returns></returns>
        public static ServiceHost CreateWcfHost(Type contractType, Type implementationType, string tcpUrl, string httpUrl = null)
        {
            Uri TcpAddress = new Uri(tcpUrl);
            ServiceHost _ServiceHost = new ServiceHost(implementationType, TcpAddress);
            if (!string.IsNullOrEmpty(httpUrl))
            {
                ServiceMetadataBehavior _ServiceMetadataBehavior = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    HttpGetUrl = new Uri(httpUrl)
                };
                _ServiceHost.Description.Behaviors.Add(_ServiceMetadataBehavior);
                BasicHttpBinding bhb = new BasicHttpBinding();
                bhb.Security.Mode = BasicHttpSecurityMode.None;
                _ServiceHost.AddServiceEndpoint(contractType, bhb, httpUrl);
                _ServiceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
            }

            NetTcpBinding tcpb = new NetTcpBinding();
            tcpb.MaxBufferSize = 2147483647;
            tcpb.MaxBufferPoolSize = 2147483647;
            tcpb.MaxReceivedMessageSize = 2147483647;
            tcpb.Security.Mode = SecurityMode.None;
            _ServiceHost.AddServiceEndpoint(contractType, tcpb, tcpUrl);

            return _ServiceHost;
        }

    }
}
