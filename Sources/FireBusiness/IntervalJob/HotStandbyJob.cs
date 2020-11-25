using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.IntervalJob
{
    public class HotStandbyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                CoreBusiness.Hub.HotStandbyHandle();
            });
        }
    }
}
