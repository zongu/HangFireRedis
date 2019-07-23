
namespace HangFireRedis
{
    using System;
    using Hangfire;
    using HangFireRedis.Model;
    using HangFireRedis.Service;
    using Microsoft.Owin.Hosting;

    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:8089"))
            {
                var str = Console.ReadLine();

                var message = new MessageJob()
                {
                    Content = str
                };

                var svc = new JobScheduleService();
                svc.AddRecurJobSchedule(message, Cron.MinuteInterval(1));

                //svc.AddJobSchedule(message, DateTime.Now.AddSeconds(30));

                Console.Read();
            }   
        }
    }
}
