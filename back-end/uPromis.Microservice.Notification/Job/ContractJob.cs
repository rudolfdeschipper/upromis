using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace uPromis.Microservice.Notification.Job
{
    public class ContractJob : IJob
    {
        private readonly ILogger Logger;
        private readonly Data.NotificationDbContext DBContext;
        private readonly string NotificationType = Services.Notification.NotificationType.CONTRACTNOTIFICATION;
        public ContractJob(ILoggerProvider loggerProvider, Data.NotificationDbContext dbContext)
        {
            DBContext = dbContext;
            Logger = loggerProvider.CreateLogger(nameof(ContractJob));
        }
        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"Execute {nameof(ContractJob)} job's task");
            var Subscriber = context.Trigger.Key.Name;

            var items = DBContext.NotificationEntries.Where(n =>
                n.NotificationType == NotificationType
                && n.SubscriptionID == Subscriber
            );
            if (items.Any())
            {
                Logger.LogDebug($"Found {items.Count()} {NotificationType} items for subscriber {Subscriber}.");
                // get a formatter and format the message
                // should get it through DI
                // IMessageFormatter formatter = new EmailMessageFormatter();
                // var message = formatter.FormatMessage(Subscriber, NotificationType, items);
                // send formatted message to mail provider
                // should get it through DI
                // IMailProvider mailProvider = new MailProvider();
                // mailProvider.SetMailTemplate(NotificationType);
                // mailProvider.SendMail(Subscriber, message);
            }
            else
            {
                Logger.LogInformation($"No {NotificationType} items for subscriber {Subscriber}.");
            }

            return Task.CompletedTask;
        }
    }
}
