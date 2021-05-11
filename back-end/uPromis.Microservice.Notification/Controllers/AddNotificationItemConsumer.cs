using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Quartz;
using System.Linq;
using uPromis.Microservice.Notification.Models;
using uPromis.Services.Models;

namespace uPromis.Microservice.Notification.Controllers
{
    public class AddNotificationItemConsumer : IConsumer<NotificationEntryDTO>
    {
        private readonly ILogger Logger;
        private readonly IScheduler Scheduler;
        private readonly INotificationEntryRepository Repo;
        public AddNotificationItemConsumer(ILoggerProvider loggerProvider, IScheduler scheduler, INotificationEntryRepository repo)
        {
            Logger = loggerProvider.CreateLogger(nameof(AddNotificationItemConsumer));
            Scheduler = scheduler;
            Repo = repo;
        }
        public async Task Consume(ConsumeContext<NotificationEntryDTO> context)
        {
            Logger.LogDebug("AddNotification entry: {0} - {1}", context.Message.ID, context.Message.Code);

            await SaveNotification(context);

            await CreateTriggerIfNotExists(context);

            return;
        }

        /// <summary>
        /// Creates the trigger for the user/notification type if it does not exist yet
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task CreateTriggerIfNotExists(ConsumeContext<NotificationEntryDTO> context)
        {
            // if not subscriber, no need to make a trigger
            if (string.IsNullOrEmpty(context.Message.SubscriptionID))
            {
                return;
            }
            // check if a job / trigger exist for this combination, if not create a default one
            TriggerKey k = new(context.Message.SubscriptionID, context.Message.NotificationType);
            var trigger = await Scheduler.GetTrigger(k);
            if (trigger == null)
            {
                var triggerCreator = new TriggerCreator(Scheduler);

                trigger = await triggerCreator.FillTriggerAsync(context.Message.SubscriptionID, context.Message.NotificationType);

                await Scheduler.ScheduleJob(trigger);
            }
            // if it exists, don't try to resume, as this will override the user's preference
        }

        /// <summary>
        /// Save the entry - check if it exists, if not create it
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Task<NotificationEntry> SaveNotification(ConsumeContext<NotificationEntryDTO> context)
        {
            var exists = Repo.GetAllByExternalID(context.Message.ExternalID).Where(
                r => r.NotificationType == context.Message.NotificationType
                && r.SubscriptionID == context.Message.SubscriptionID
                && r.NotificationSubtype == context.Message.NotificationSubtype).FirstOrDefault();

            var from = context.Message;

            if (exists == null)
            {
                Logger.LogInformation("Adding Notification entry: {0} - {1}", context.Message.ID, context.Message.Code);
                NotificationEntry rec = new()
                {
                    Code = from.Code,
                    ExternalID = from.ExternalID,
                    Description = from.Description,
                    Duedate = from.Duedate,
                    Enddate = from.Enddate,
                    ExpectedAction = from.ExpectedAction,

                    NotificationType = from.NotificationType,
                    NotificationSubtype = from.NotificationSubtype,
                    Salutation = from.Salutation,
                    Startdate = from.Startdate,
                    SubscriptionID = from.SubscriptionID,
                    SubscriptionGroup = from.SubscriptionGroup,
                   
                    URL = from.URL
                };

                return Repo.Post(rec);
            }
            else
            {
                Logger.LogInformation("Updating Notification entry: {0} - {1}", context.Message.ID, context.Message.Code);
                exists.Code = from.Code;
                exists.ExternalID = from.ExternalID;
                exists.Description = from.Description;
                exists.Duedate = from.Duedate;
                exists.Enddate = from.Enddate;
                exists.ExpectedAction = from.ExpectedAction;
                exists.Salutation = from.Salutation;
                exists.Startdate = from.Startdate;
                exists.SubscriptionGroup = from.SubscriptionGroup;
                exists.URL = from.URL;

                return Repo.Put(exists);
            }
        }
    }
}
