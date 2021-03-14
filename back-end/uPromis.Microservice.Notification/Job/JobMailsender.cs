using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPromis.Microservice.Notification.MessageFormatter;
using uPromis.Microservice.Notification.Properties;
using uPromis.Microservice.Notification.Transmitter;

namespace uPromis.Microservice.Notification.Job
{
    public class JobMailsender
    {
        private readonly ILogger Logger;
        private readonly ILoggerProvider LoggerProvider;
        private readonly Data.NotificationDbContext DBContext;
        private readonly string NotificationType;
        private readonly string MailTemplate;
        public JobMailsender(ILoggerProvider loggerProvider, Data.NotificationDbContext dbContext, string notificationType, string mailTemplate)
        {
            DBContext = dbContext;
            Logger = loggerProvider.CreateLogger(nameof(ContractJob));
            LoggerProvider = loggerProvider;
            NotificationType = notificationType;
            MailTemplate = mailTemplate;
        }
        public void SendmailForJob(string subscriber, string subject, string title)
        {
            var items = DBContext.NotificationEntries.Where(n =>
                n.NotificationType == NotificationType
                && n.SubscriptionID == subscriber
            ).OrderBy(o => o.Duedate)
            .ThenBy(o => o.Startdate)
            .ThenBy(o => o.Enddate);

            if (items.Any())
            {
                Logger.LogDebug($"Found {items.Count()} {NotificationType} items for subscriber {subscriber}.");

                INotificationFormatter formatter = new NotificationMailFormatter
                {
                    Template = MailTemplate,
                    TableTitle = title,
                    Salutation = items.First().SubscriptionID
                };

                var message = formatter.FormatMessage(items.ToList());

                IMessageTransmitter transmitter = new EmailTransmitter(LoggerProvider)
                {
                    Recipient = items.First().SubscriptionID
                };
                transmitter.Transmit(subject, message);
            }
            else
            {
                Logger.LogInformation($"No {NotificationType} items for subscriber {subscriber}.");
            }
        }
    }
}
