
namespace HangFireRedis.Service
{
    using System;
    using Autofac;
    using Hangfire;
    using HangFireRedis.Domain.Applibs;
    using HangFireRedis.Domain.Model;
    using HangFireRedis.Domain.Repository;
    using Newtonsoft.Json;

    public class JobScheduleService
    {
        public string AddJobSchedule<T>(T job, TimeSpan timeSpan)
        {   
            var id = BackgroundJob.Schedule(
                    () => JobScheduleHandler.Trigger(job),
                    timeSpan);

            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var jobName = job.GetType().Name;
                var repo = scope.Resolve<IJobScheduleRepository>();
                var jobSchedule = new JobSchedule()
                {
                    JobScheduleId = id,
                    JobScheduleName = jobName,
                    JobScheduleContent = JsonConvert.SerializeObject(job),
                    CreateDateTimeStamp = TimeStampHelper.ToUtcTimeStamp(DateTime.Now)
                };

                repo.Add(jobSchedule);

                Console.WriteLine($"{DateTime.Now}-JobScheduleService AddJobSchedule:{JsonConvert.SerializeObject(jobSchedule)}");
            }

            return id;
        }

        public string AddCycleJobSchedule<T>(T job, string cronExpression)
        {
            var id = Guid.NewGuid().ToString();
            RecurringJob.AddOrUpdate(
                id,
                () => JobScheduleHandler.Trigger(job),
                cronExpression);

            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var jobName = job.GetType().Name;
                var repo = scope.Resolve<IJobScheduleRepository>();
                var jobSchedule = new JobSchedule()
                {
                    JobScheduleId = id,
                    JobScheduleName = jobName,
                    JobScheduleContent = JsonConvert.SerializeObject(job),
                    CreateDateTimeStamp = TimeStampHelper.ToUtcTimeStamp(DateTime.Now)
                };

                repo.Add(jobSchedule);

                Console.WriteLine($"{DateTime.Now}-JobScheduleService AddJobSchedule:{JsonConvert.SerializeObject(jobSchedule)}");
            }

            return id;
        }
    }
}
