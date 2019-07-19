
namespace HangFireRedis.Domain.Repository
{
    using HangFireRedis.Domain.Model;

    public interface IJobScheduleRepository
    {
        bool Add(JobSchedule jobSchedule);

        bool Remove(string jobScheduleId);
    }
}
