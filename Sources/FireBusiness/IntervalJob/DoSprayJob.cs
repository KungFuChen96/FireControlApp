using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.IntervalJob
{
    /// <summary>
    /// 喷淋作业
    /// </summary>
    public class DoSprayJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                //CoreBusiness.Hub.DoFalutHandle();
            });
        }
    }
}
