using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace uPromis.Microservice.Notification.Controllers
{
    public class AddNotificationItemConsumer : IConsumer<Services.Notification.NotificationEntry>
    {
        private readonly ILogger Logger;
        public AddNotificationItemConsumer(ILoggerProvider loggerProvider
            )
        {
            Logger = loggerProvider.CreateLogger("AddNotificationItemConsumer");
        }
        public Task Consume(ConsumeContext<Services.Notification.NotificationEntry> context)
        {
            Logger.LogInformation("Adding Notification entry: {0} - {1}", context.Message.ID, context.Message.Code);
            return Task.CompletedTask;
        }
    }
}
