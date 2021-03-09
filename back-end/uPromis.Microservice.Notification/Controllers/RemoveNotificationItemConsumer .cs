using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Quartz;
using System.Linq;
using uPromis.Microservice.Notification.Model;

namespace uPromis.Microservice.Notification.Controllers
{
    public class RemoveNotificationItemConsumer : IConsumer<Services.Notification.NotificationEntry>
    {
        private readonly ILogger Logger;
        private readonly IScheduler Scheduler;
        private readonly INotificationRepository Repo;
        public RemoveNotificationItemConsumer(ILoggerProvider loggerProvider, IScheduler scheduler, INotificationRepository repo)
        {
            Logger = loggerProvider.CreateLogger(nameof(RemoveNotificationItemConsumer));
            Scheduler = scheduler;
            Repo = repo;
        }
        public async Task Consume(ConsumeContext<Services.Notification.NotificationEntry> context)
        {
            Logger.LogInformation("Removing Notification entry: {0} - {1}", context.Message.ID, context.Message.Code);

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
                Startdate = from.Startdate,
                SubscriptionID = from.SubscriptionID,
                URL = from.URL
            };

            if (exists != null)
            {
                await Repo.Delete(rec);
            }
            return;
        }
    }
}
