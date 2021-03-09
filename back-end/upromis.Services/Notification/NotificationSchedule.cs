using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace uPromis.Services.Notification
{
    public class NotificationSchedule
    {
        public static NotificationSchedule CreateDefaultSchedule(string subscriberID, string notificationType) => new NotificationSchedule() { SubscriberID = subscriberID, NotificationType = notificationType };

        public NotificationSchedule()
        {

        }

        public NotificationSchedule(string subscriber, string notificationType, string[] scheduleElements, bool enabled)
        {
            SubscriberID = subscriber;
            NotificationType = notificationType;
            RunOnSunday = scheduleElements[5].Contains("1");
            RunOnMonday = scheduleElements[5].Contains("2");
            RunOnTuesday = scheduleElements[5].Contains("3");
            RunOnWednesday = scheduleElements[5].Contains("4");
            RunOnThursday = scheduleElements[5].Contains("5");
            RunOnFriday = scheduleElements[5].Contains("6");
            RunOnSaturday = scheduleElements[5].Contains("7");

            RunOnHour = int.Parse(scheduleElements[2]);
            RunOnMinute = int.Parse(scheduleElements[1]);
            Enable = enabled;
        }

        public string SubscriberID { get; set; }
        public string NotificationType { get; set; }
        public bool Enable { get; set; } = true;

        [Range(minimum: 0, maximum: 23, ErrorMessage = "Hour to run should be between 0 and 23")]
        public int RunOnHour { get; set; } = 6;

        [Range(minimum: 0, maximum: 59, ErrorMessage = "Minute to run should be between 0 and 59")]
        public int RunOnMinute { get; set; } = 0;
        public bool RunOnMonday { get; set; } = true;
        public bool RunOnTuesday { get; set; } = true;
        public bool RunOnWednesday { get; set; } = true;
        public bool RunOnThursday { get; set; } = true;
        public bool RunOnFriday { get; set; } = true;
        public bool RunOnSaturday { get; set; } = true;
        public bool RunOnSunday { get; set; } = true;
    }
}
