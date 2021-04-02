using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Quartz;
using System.Linq;
using uPromis.Microservice.Notification.Model;

namespace uPromis.Microservice.Notification.Controllers
{
    public class AddNotificationItemConsumer : IConsumer<Services.Notification.NotificationEntry>
    {
        private readonly ILogger Logger;
        private readonly IScheduler Scheduler;
        private readonly INotificationRepository Repo;
        public AddNotificationItemConsumer(ILoggerProvider loggerProvider, IScheduler scheduler, INotificationRepository repo)
        {
            Logger = loggerProvider.CreateLogger(nameof(AddNotificationItemConsumer));
            Scheduler = scheduler;
            Repo = repo;
        }
        public async Task Consume(ConsumeContext<Services.Notification.NotificationEntry> context)
        {
            Logger.LogDebug("AddNotification entry: {0} - {1}", context.Message.ID, context.Message.Code);

            // save the entry - check if it exists, if not create it
            var exists = Repo.Get(context.Message.SubscriptionID, 
                context.Message.NotificationType, context.Message.URL);

            var from = context.Message;
            var rec = new Notification.Model.NotificationEntry()
            {
                Code = from.Code,
                Description = from.Description,
                Duedate = from.Duedate,
                Enddate = from.Enddate,
                ID = from.ID,
                NotificationType = from.NotificationType,
                Salutation = from.Salutation,
                Startdate = from.Startdate,
                SubscriptionID = from.SubscriptionID,
                URL = from.URL
            };

            if (exists != null)
            {
                Logger.LogInformation("Adding Notification entry: {0} - {1}", context.Message.ID, context.Message.Code);
                await Repo.Post(rec);
            }
            else
            {
                Logger.LogInformation("Updating Notification entry: {0} - {1}", context.Message.ID, context.Message.Code);
                await Repo.Put(rec);
            }

            // check if a job / trigger exist for this combination, if not create a default one
            TriggerKey k = new TriggerKey(context.Message.SubscriptionID, context.Message.NotificationType);
            if (await Scheduler.GetTrigger(k) == null)
            {
                // need to create the trigger, use standard settings
                var jobDetail = await Scheduler.GetJobDetail(new JobKey(context.Message.NotificationType));

                var triggerCreator = new TriggerCreator(Scheduler);
 
                var trigger = await triggerCreator.FillTriggerAsync(context.Message.SubscriptionID, context.Message.NotificationType);

                await Scheduler.ScheduleJob(jobDetail, trigger);
            }
            // if it exists, don't try to resume, as this will override the user's preference

            return;
        }
    }
}
