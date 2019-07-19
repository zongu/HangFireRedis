
namespace HangFireRedis.Persistent.Tests
{
    using System;
    using System.Linq;
    using HangFireRedis.Domain.Applibs;
    using HangFireRedis.Domain.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StackExchange.Redis;

    [TestClass]
    public class JobScheduleRepositoryTests
    {
        private JobScheduleRepository repo;

        [TestInitialize]
        public void Init()
        {
            var conn = ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(Applibs.ConfigHelper.RedisConn));
            var redis = conn.GetDatabase(15);
            var affixKey = "JobSchedule";

            this.repo = new JobScheduleRepository(conn, affixKey);

            var keys = conn.GetServer(Applibs.ConfigHelper.RedisConn).Keys(15, $"{affixKey}*", 10, CommandFlags.None).ToList();
            keys.ForEach(key => redis.KeyDelete(key));
        }

        [TestMethod]
        public void AddTest()
        {
            var addResult = this.repo.Add(new JobSchedule()
            {
                JobScheduleId = Guid.NewGuid().ToString(),
                JobScheduleName = "Test",
                JobScheduleContent = "TestContent",
                CreateDateTimeStamp = TimeStampHelper.ToUtcTimeStamp(DateTime.Now)
            });

            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var id = Guid.NewGuid().ToString();
            var addResult = this.repo.Add(new JobSchedule()
            {
                JobScheduleId = id,
                JobScheduleName = "Test",
                JobScheduleContent = "TestContent",
                CreateDateTimeStamp = TimeStampHelper.ToUtcTimeStamp(DateTime.Now)
            });

            Assert.IsTrue(addResult);

            var remoneResult = this.repo.Remove(id);

            Assert.IsTrue(remoneResult);
        }
    }
}
