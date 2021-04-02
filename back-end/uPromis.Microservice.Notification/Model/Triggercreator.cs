using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uPromis.Microservice.Notification.Model
{
    public class TriggerCreator
    {

        private readonly IScheduler Scheduler;

        public TriggerCreator(IScheduler scheduler)
        {
            Scheduler = scheduler;
        }

        public async Task<ITrigger> FillTriggerAsync(string subscriberID, string notificationType)
        {
            return await FillTriggerAsync(Services.Notification.NotificationSchedule.CreateDefaultSchedule(subscriberID, notificationType));
        }

        public async Task<ITrigger> FillTriggerAsync(Services.Notification.NotificationSchedule value)
        {
            DayOfWeek[] weekDays = new DayOfWeek[7];

            int i = 0;

            if (value.RunOnSunday)
            {
                weekDays[i++] = DayOfWeek.Sunday;
            }
            if (value.RunOnMonday)
            {
                weekDays[i++] = DayOfWeek.Monday;
            }
            if (value.RunOnTuesday)
            {
                weekDays[i++] = DayOfWeek.Tuesday;
            }
            if (value.RunOnWednesday)
            {
                weekDays[i++] = DayOfWeek.Wednesday;
            }
            if (value.RunOnThursday)
            {
                weekDays[i++] = DayOfWeek.Thursday;
            }
            if (value.RunOnFriday)
            {
                weekDays[i++] = DayOfWeek.Friday;
            }
            if (value.RunOnSaturday)
            {
                weekDays[i++] = DayOfWeek.Saturday;
            }

            if (await Scheduler.CheckExists(new TriggerKey(value.SubscriberID, value.NotificationType)) == true)
            {
                await Scheduler.DeleteJob(new JobKey(value.SubscriberID, value.NotificationType));
            }

            var trigger = TriggerBuilder.Create()
                .WithIdentity(value.SubscriberID, value.NotificationType)
                .WithSchedule(CronScheduleBuilder
                .AtHourAndMinuteOnGivenDaysOfWeek(value.RunOnHour, value.RunOnMinute, weekDays.Take(i).ToArray()))
                .ForJob(new JobKey(value.NotificationType))
                .Build();

            return trigger;
        }
    }
}
