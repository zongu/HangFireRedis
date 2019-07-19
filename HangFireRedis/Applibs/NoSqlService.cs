
namespace HangFireRedis.Applibs
{
    using System;
    using StackExchange.Redis;

    internal static class NoSqlService
    {
        private static Lazy<ConnectionMultiplexer> lazyRedisConnections;

        public static ConnectionMultiplexer RedisConnections
        {
            get
            {
                if (lazyRedisConnections == null)
                {
                    NoSqlInit();
                }

                return lazyRedisConnections.Value;
            }
        }


        private static void NoSqlInit()
        {
            lazyRedisConnections = new Lazy<ConnectionMultiplexer>(() =>
            {
                var options = ConfigurationOptions.Parse(ConfigHelper.RedisConn);
                options.AbortOnConnectFail = false;

                var muxer = ConnectionMultiplexer.Connect(options);
                muxer.ConnectionFailed += (sender, e) =>
                {
                    Console.WriteLine("redis failed: " + EndPointCollection.ToString(e.EndPoint) + "/" + e.ConnectionType);
                };
                muxer.ConnectionRestored += (sender, e) =>
                {
                    Console.WriteLine("redis restored: " + EndPointCollection.ToString(e.EndPoint) + "/" + e.ConnectionType);
                };

                return muxer;
            });
        }
    }
}
