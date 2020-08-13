using Quartz;
using Quartz.Impl;
using FireBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FireBusiness.Quartz
{
    public class QuartzHelper
    {
        static readonly IScheduler _scheduler;
        static QuartzHelper()
        {
            var properties = new NameValueCollection();
            // 设置线程池
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            //设置线程池的最大线程数量
            properties["quartz.threadPool.threadCount"] = "5";
            //设置作业中每个线程的优先级
            properties["quartz.threadPool.threadPriority"] = ThreadPriority.Normal.ToString();
            // 远程输出配置
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "666";  //配置端口号
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp"; //协议类型
            //创建一个工厂
            var schedulerFactory = new StdSchedulerFactory(properties);
            //启动
            _scheduler = schedulerFactory.GetScheduler().Result;
            //1、开启调度
            _scheduler.Start();
        }

        #region 公共
        /// <summary>
        /// 将任务加入队列,通用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValue">任务唯一编号</param>
        /// <param name="startAtSeconds">创建任务后多久开始执行（单位：秒）</param>
        /// <param name="intervalInSeconds">每个多久再次执行（单位：秒）</param>
        /// <param name="objData">传值的对象</param>
        public static void CommonAddJob<T>(string keyValue, int startAtSeconds, int intervalInSeconds, object objData = null) where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>().WithIdentity(keyValue).Build();
            if (objData != null)
                job.JobDataMap.Put(Configs.paramName, objData);

            ITrigger trigger = TriggerBuilder.Create()
                .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(startAtSeconds)))
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(intervalInSeconds)
                .RepeatForever())
                .Build();
            _scheduler.ScheduleJob(job, trigger);
        }
        #endregion

        #region 静置类型
        ///// <summary>
        ///// 将任务加入队列，静置任务使用
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="keyValue">任务唯一编号</param>
        ///// <param name="startAtSeconds">创建任务后多久开始执行（单位：秒）</param>
        ///// <param name="intervalInMinutes">每个多久再次执行（单位：分钟）</param>
        ///// <param name="objData">传值的对象</param>
        ///// <param name="taskStatus">任务类型</param>
        ///// <param name="taskId">任务唯一辨识</param>
        //public static void AddJob<T>(string keyValue, int startAtSeconds, int intervalInMinutes, QuartzDataType taskStatus, object objData, string taskId = "") where T : IJob
        //{
        //    IJobDetail job = JobBuilder.Create<T>().WithIdentity(keyValue).Build();

        //    job.JobDataMap.Put(Configs.typeName, (int)taskStatus);
        //    job.JobDataMap.Put(Configs.taskIdName, taskId);
        //    if (objData != null)
        //        job.JobDataMap.Put(Configs.paramName, objData);

        //    ITrigger trigger = TriggerBuilder.Create()
        //        .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(startAtSeconds)))
        //        .WithSimpleSchedule(x => x.WithIntervalInMinutes(intervalInMinutes)
        //        .RepeatForever())
        //        .Build();
        //    _scheduler.ScheduleJob(job, trigger);
        //}
        #endregion

        #region 其他类型

        /// <summary>
        /// 时间间隔执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="seconds">时间间隔(单位：秒)</param>
        public static void AddMinuteIntervalJob<T>(string key, int startTimeOffsetMinutes, int intervalInMinutes, object objData = null) where T : IJob
        {
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create<T>().WithIdentity(key).Build();

            if (objData != null)
                job.JobDataMap.Put("CommHubBase", objData);

            //3、创建触发器
            ITrigger trigger = TriggerBuilder.Create()
                .StartAt(new DateTimeOffset(DateTime.Now.AddMinutes(startTimeOffsetMinutes)))//启动两分钟后开始发送首次心跳包
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(intervalInMinutes)//每次间隔10分钟
                .RepeatForever())
                .Build();
            //4、将任务加入到任务池
            _scheduler.ScheduleJob(job, trigger);
        }

        public static void AddSecondIntervalJob<T>(string key, int startTimeOffsetMinutes, int intervalInMinutes, object objData = null) where T : IJob
        {
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create<T>().WithIdentity(key).Build();

            if (objData != null)
                job.JobDataMap.Put("CommHubBase", objData);

            //3、创建触发器
            ITrigger trigger = TriggerBuilder.Create()
                .StartAt(new DateTimeOffset(DateTime.Now.AddSeconds(startTimeOffsetMinutes)))//启动两分钟后开始发送首次心跳包
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(intervalInMinutes)//每次间隔10分钟
                .RepeatForever())
                .Build();
            //4、将任务加入到任务池
            _scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// 指定时间执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="cronExpression">cron表达式，即指定时间点的表达式</param>
        public static async Task<bool> ExecuteByCron<T>(string cronExpression) where T : IJob
        {
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create<T>().Build();
            //3、创建触发器
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
            .StartNow()
            .WithCronSchedule(cronExpression)
            //.re
            .Build();
            //4、将任务加入到任务池
            await _scheduler.ScheduleJob(job, trigger);
            return true;
        }
        #endregion

        /// <summary>
        /// 异步删除任务
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteJobAsync(string key)
        {
            return await _scheduler.DeleteJob(new JobKey(key));

        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="key"></param>
        public static void DeleteJob(string key)
        {
            _scheduler.DeleteJob(new JobKey(key));
        }

        /// <summary>
        /// 删除任务通过JobKey
        /// </summary>
        /// <param name="key">JobKey</param>
        public static void DeleteJob(JobKey key)
        {
            if (key == null)
                return;
            _scheduler.DeleteJob(key);
        }
    }
}