
namespace HangFireRedis.Domain.Model
{
    public class JobSchedule
    {
        public string JobScheduleId { get; set; }

        public string JobScheduleName { get; set; }

        public string JobScheduleContent { get; set; }

        public long CreateDateTimeStamp { get; set; }
    }
}