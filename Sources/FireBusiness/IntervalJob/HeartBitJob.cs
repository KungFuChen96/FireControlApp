using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.IntervalJob
{
    /// <summary>
    /// 心跳作业
    /// </summary>
    public class HeartBitJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                //onstandby objRes = (onstandby)context.MergedJobDataMap["CommHubBase"];
                //hub.SendHeartBit();
            });
        }
    }
}
