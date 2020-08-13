using System;
using System.Collections.Generic;
using System.ServiceModel;
using Neware.BTS.Service;
using Neware.BTS.Service.WXA123;
using Newtonsoft.Json;

namespace FireBusiness.OutSystem
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BtsService : IWCSService
    {
        public BtsService()
        {
        }

        #region 定义委托及变量
        /// <summary>
        /// 库位告警委托
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public delegate ServiceResult CellAlarmEventHandler(string cell, string code, string message);
        public event CellAlarmEventHandler CellAlarmEvent;

        /// <summary>
        /// 消防告警
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public delegate ServiceResult CellFireDel(string cell, string code, string message);
        public event CellFireDel CellFireEvent;

        /// <summary>
        /// 测试完成委托（需要）
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public delegate ServiceResult TestFinishEventHandler(TestResult testResult);
        public event TestFinishEventHandler TestFinishEvent;

        /// <summary>
        /// 出盘委托
        /// </summary>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public delegate ServiceResult OutReadyDel(string cell, TrayType trayType, string trayCode, string ops);
        public event OutReadyDel OutReadyEvent;

        /// <summary>
        /// 连接完成委托
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public delegate ServiceResult ContactFinishEventHandler(string cell, bool result, string message);
        public event ContactFinishEventHandler ContactFinishEvent;

        /// <summary>
        /// 验证托盘合理性
        /// </summary>
        /// <param name="trayCode"></param>
        /// <param name="ops"></param>
        /// <returns></returns>
        public delegate ServiceResult<bool> TrayValidWithBatmesDel(string trayCode, string ops);
        public event TrayValidWithBatmesDel TrayValidEvent;

        /// <summary>
        /// 获取托盘电芯委托（需要）
        /// </summary>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        public delegate ServiceResult<Dictionary<int, string>> TrayBatteriesWithBatmesDel(string trayCode);
        public event TrayBatteriesWithBatmesDel TrayBatteriesEvent;

        /// <summary>
        /// 获取工步文件（需要）
        /// </summary>
        /// <param name="ops"></param>
        /// <returns></returns>
        public delegate ServiceResult<FileSetting> TestFileWithBatmesDel(string ops);
        public event TestFileWithBatmesDel TestFileEvent;

        /// <summary>
        /// 获取通道委托 （需要）
        /// </summary>
        /// <param name="ops"></param>
        /// <returns></returns>
        public delegate ServiceResult<Dictionary<int, ChannelStatus>> ChannelStatusDel(string cell);
        public event ChannelStatusDel ChannelStatusEvent;

        public delegate ServiceResult<TrayInfo> TrayInfoDel(string cell);
        public event TrayInfoDel TrayInfoEvent;
        #endregion

        #region 启用的接口

        #region 通用
        /// <summary>
        /// 接收库位告警信息
        /// </summary>
        /// <remark>
        /// 应用项目：WXA123 F2F3，ATL MES
        /// </remark>
        /// <param name="cell">库位编号</param>
        /// <param name="code">告警代码</param>
        /// <param name="message">告警消息</param>
        /// <returns></returns>
        public ServiceResult CellAlarm(string cell, string code, string message)
        {
            return CellAlarmEvent?.Invoke(cell, code, message);
        }

        /// <summary>
        /// 接收库位消防信息
        /// </summary>
        /// <remarks>
        /// 应用项目：暂无
        /// </remarks>
        /// <param name="cell">库位编号</param>
        /// <param name="code">消防代码</param>
        /// <param name="message">消防信息</param>
        /// <returns></returns>
        public ServiceResult CellFire(string cell, string code, string message)
        {
            return CellFireEvent?.Invoke(cell, code, message);
        }

        /// <summary>
        /// 接收测试完成结果数据
        /// </summary>
        /// <remarks>
        /// 应用项目：WXA123 F2F3
        /// </remarks>
        /// <param name="testResult">测试结果数据</param>
        public ServiceResult TestFinish(TestResult testResult)
        {
            try
            {
                if (testResult.TrayCode.IsEmpty())
                    return JsonConvert.DeserializeObject<ServiceResult>(ComResultJson(false, "未将对象引用初始化"));
                return TestFinishEvent?.Invoke(testResult);
            }
            catch (Exception ex)
            {
                CORE.Instance.AddLog("BTS通讯异常", ex.Message, BatMes.Client.Enums.LogType.Network);
                CORE.Instance.OnMessage(ex.Message, BatMes.Client.Enums.SysEventLevel.Error);
                return JsonConvert.DeserializeObject<ServiceResult>(ComResultJson(false, ex.Message));
            }
        }

        /// <summary>
        /// 接收连接测试（预测）完成消息
        /// </summary>
        /// <remarks>
        /// 应用项目：WXA123 F2F3
        /// </remarks>
        /// <param name="cell">库位编号</param>
        /// <param name="result">连接测试结果</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public ServiceResult ContactFinish(string cell, bool result, string message)
        {
            return ContactFinishEvent?.Invoke(cell, result, message);
        }

        /// <summary>
        /// 接收出盘请求
        /// </summary>
        /// <remarks>
        /// 应用项目：NEWARE M2
        /// </remarks>
        /// <param name="cell">库位编号</param>
        /// <param name="trayType">托盘类型</param>
        /// <param name="trayCode">托盘条码</param>
        /// <param name="ops">工序编号</param>
        public ServiceResult OutReady(string cell, TrayType trayType, string trayCode, string ops)
        {
            return OutReadyEvent?.Invoke(cell, trayType, trayCode, ops);
        }
        #endregion

        #region MES
        public ServiceResult<bool> TrayValidWithBatmes(string trayCode, string ops)
        {
            return TrayValidEvent?.Invoke(trayCode, ops);
        }

        public ServiceResult<Dictionary<int, string>> TrayBatteriesWithBatmes(string trayCode)
        {
            return TrayBatteriesEvent?.Invoke(trayCode);
        }

        public ServiceResult<FileSetting> TestFileWithBatmes(string ops)
        {
            return TestFileEvent?.Invoke(ops);
        }

        public ServiceResult<Dictionary<int, ChannelStatus>> ChannelStatusWithBatmes(string cell)
        {
            return ChannelStatusEvent?.Invoke(cell);
        }

        public ServiceResult<TrayInfo> TrayInfo(string cell)
        {
            return TrayInfoEvent?.Invoke(cell);
        }
        #endregion

        #endregion

        #region 不启用的接口

        #region WXA专用
        /// <summary>
        /// WXA123接收校准完成结果数据
        /// </summary>
        /// <remarks>
        /// 应用项目：WXA123 F2F3
        /// </remarks>
        /// <param name="caliResult">校准结果数据</param>
        /// <returns></returns>
        public ServiceResult CaliFinishWithWXA123(CaliResult caliResult)
        {
            var strMsg = ComResultJson();
            return JsonConvert.DeserializeObject<ServiceResult>(strMsg);
        }
        #endregion

        #region ATL专用
        /// <summary>
        /// ATL接收测试完成结果数据
        /// </summary>
        /// <remarks>
        /// 应用项目：ATL MES
        /// </remarks>
        /// <param name="testResult">测试结果数据</param>
        /// <returns></returns>
        public ServiceResult TestFinishWithAtlMes(Neware.BTS.Service.ATL.TestResult testResult)
        {
            var strMsg = ComResultJson();
            return JsonConvert.DeserializeObject<ServiceResult>(strMsg);
        }

        /// <summary>
        /// ATL根据库位/夹具编号获取测试（文件）信息
        /// </summary>
        /// <remarks>
        /// 应用项目：ATL MES
        /// </remarks>
        /// <param name="cell">库位/夹具编号</param>
        /// <returns></returns>
        public ServiceResult<FileSetting> TestFileWithAtlMes(string cell)
        {
            var strMsg = ComResultJson();
            return JsonConvert.DeserializeObject<ServiceResult<FileSetting>>(strMsg);
        }
        #endregion

        #endregion

        #region 扩展方法
        /// <summary>
        /// CSCService通用的响应格式
        /// </summary>
        /// <returns></returns>
        private string ComResultJson(bool isSuccess = false, string strMsg = "未实现该接口逻辑")
        {
            var resObj = new
            {
                Code = isSuccess ? ResultCode.Success : ResultCode.Exception,
                Message = isSuccess ? "Success" : strMsg
            };
            return JsonConvert.SerializeObject(resObj);
        }
        #endregion
    }
}
