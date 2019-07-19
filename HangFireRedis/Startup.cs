
namespace HangFireRedis
{
    using Hangfire;
    using HangFireRedis.Applibs;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration
                .Configuration
                .UseRedisStorage(NoSqlService.RedisConnections);

            //// 啟用HanfireServer
            app.UseHangfireServer();

            //// 啟用Hangfire的Dashboard
            app.UseHangfireDashboard();
        }
    }
}
