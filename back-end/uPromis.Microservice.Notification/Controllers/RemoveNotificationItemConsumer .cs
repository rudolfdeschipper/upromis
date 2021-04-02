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
            Logger.LogDebug("RemoveNotificationItem entry: {0} - {1}", context.Message.ID, context.Message.Code);

            // get the entry - check if it exists, if not don't try to delete it
            var exists = await Repo.Get(context.Message.SubscriptionID, 
                context.Message.NotificationType, context.Message.URL);

            if (exists != null)
            {
                Logger.LogInformation("Removing Notification entry: {0} - {1}", context.Message.ID, context.Message.Code);
                await Repo.Delete(exists);
            }
            return;
        }
    }
}
