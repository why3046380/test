using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace QuartzDemo
{
    public class HelloJob:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
             Console.WriteLine("this is a Hello WorldTest");
        }
    }

    public class SimpleExample
    {
        public void Run()
        {
            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);

            ISchedulerFactory factory=new StdSchedulerFactory(); 
            IScheduler scheduler = factory.GetScheduler();
            IJobDetail job = JobBuilder.Create<HelloJob>().WithIdentity("job1", "group1").Build();
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1").StartAt(runTime).Build();
            //SimpleTriggerImpl trigger1 = new SimpleTriggerImpl("trigger2", "group1", DateTime.Now.AddMinutes(1), null,MisfireInstruction.SimpleTrigger.RescheduleNextWithExistingCount, TimeSpan.FromSeconds(10));
            var trigger1 = new SimpleTriggerImpl("trigger1", "group1", DateTime.Now.AddSeconds(10), null, -1,TimeSpan.FromMilliseconds(1000));
            scheduler.ScheduleJob(job, trigger1);
            scheduler.Start();

        }
    }
}
