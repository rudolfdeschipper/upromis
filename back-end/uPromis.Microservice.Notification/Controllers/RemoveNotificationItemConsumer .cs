using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Quartz;
using System.Linq;
using uPromis.Microservice.Notification.Models;
using uPromis.Services.Models;

namespace uPromis.Microservice.Notification.Controllers
{
    public class RemoveNotificationItemConsumer : IConsumer<NotificationEntryDTO>
    {
        private readonly ILogger Logger;
        private readonly IScheduler Scheduler;
        private readonly INotificationEntryRepository Repo;
        public RemoveNotificationItemConsumer(ILoggerProvider loggerProvider, IScheduler scheduler, INotificationEntryRepository repo)
        {
            Logger = loggerProvider.CreateLogger(nameof(RemoveNotificationItemConsumer));
            Scheduler = scheduler;
            Repo = repo;
        }
        public Task Consume(ConsumeContext<NotificationEntryDTO> context)
        {
            Logger.LogDebug("RemoveNotificationItem entry: {0} - {1}", context.Message.ID, context.Message.Code);

            // get the entry - check if it exists, if not don't try to delete it
            var exists = Repo.GetAllByExternalID(context.Message.ExternalID).Where(
                r => r.NotificationType == context.Message.NotificationType
                && r.SubscriptionID == context.Message.SubscriptionID
                && r.URL == context.Message.URL).FirstOrDefault();

            if (exists != null)
            {
                Logger.LogInformation("Removing Notification entry: {0} - {1}", context.Message.ID, context.Message.Code);
                return Repo.Delete(exists);
            }
            return new Task<bool>(() => true);
        }
    }
}
