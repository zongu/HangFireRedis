
namespace HangFireRedis.Applibs
{
    using System.Reflection;
    using Autofac;
    using HangFireRedis.Domain.Repository;
    using HangFireRedis.Persistent;

    internal static class AutofacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if(container == null)
                {
                    Register();
                }

                return container;
            }
        }

        private static void Register()
        {
            var builder = new ContainerBuilder();

            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterType<JobScheduleRepository>()
                .WithParameter("conn", NoSqlService.RedisConnections)
                .WithParameter("affixKey", ConfigHelper.AffixKey)
                .As<IJobScheduleRepository>()
                .SingleInstance();

            container = builder.Build();
        }
    }
}
