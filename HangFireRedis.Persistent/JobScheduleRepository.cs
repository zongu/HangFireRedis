
namespace HangFireRedis.Persistent
{
    using System;
    using HangFireRedis.Domain.Model;
    using HangFireRedis.Domain.Repository;
    using Newtonsoft.Json;
    using StackExchange.Redis;

    public class JobScheduleRepository : IJobScheduleRepository
    {
        private string affixKey;

        private ConnectionMultiplexer conn;

        public JobScheduleRepository(ConnectionMultiplexer conn, string affixKey)
        {
            this.affixKey = affixKey;
            this.conn = conn;
        }

        public bool Add(JobSchedule jobSchedule)
        {
            return UseConnection(redis =>
            {
                return redis.StringSet(
                    $"{this.affixKey}:{jobSchedule.JobScheduleId}",
                    JsonConvert.SerializeObject(jobSchedule));
            });
        }

        public bool Remove(string jobScheduleId)
        {
            return UseConnection(redis =>
            {
                return redis.KeyDelete($"{this.affixKey}:{jobScheduleId}");
            });
        }

        private T UseConnection<T>(Func<IDatabase, T> func)
        {
            var redis = conn.GetDatabase(15);
            return func(redis);
        }
    }
}
