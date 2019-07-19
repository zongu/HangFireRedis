
namespace HangFireRedis.Service
{
    using System;
    using HangFireRedis.Domain.Model;
    using Newtonsoft.Json;

    internal static class JobScheduleHandler
    {
        public static void Trigger<T>(T job)
        {
            Console.WriteLine($"{DateTime.Now}-JobScheduleHandler Trigger:{JsonConvert.SerializeObject(job)}");
            //// do something
        }
    }
}
