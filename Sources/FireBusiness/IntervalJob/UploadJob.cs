using FireBusiness.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBusiness.IntervalJob
{
    /// <summary>
    /// 持久化上传MES的任务
    /// </summary>
    public class UploadJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
            });
        }
    }
}
