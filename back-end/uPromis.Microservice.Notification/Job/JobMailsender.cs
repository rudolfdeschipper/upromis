using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPromis.Microservice.Notification.MessageFormatter;
using uPromis.Microservice.Notification.Models;
using uPromis.Microservice.Notification.Properties;
using uPromis.Microservice.Notification.Transmitter;

namespace uPromis.Microservice.Notification.Job
{
    public class JobMailsender
    {
        private readonly ILogger Logger;
        private readonly ILoggerProvider LoggerProvider;
        private readonly NotificationDbContext DBContext;
        private readonly string NotificationType;
        private readonly string MailTemplate;
        private readonly IMessageTransmitter Transmitter;
        public JobMailsender(ILoggerProvider loggerProvider, NotificationDbContext dbContext, IMessageTransmitter transmitter, string notificationType, string mailTemplate)
        {
            DBContext = dbContext;
            Logger = loggerProvider.CreateLogger(nameof(ContractJob));
            LoggerProvider = loggerProvider;
            NotificationType = notificationType;
            MailTemplate = mailTemplate;
            Transmitter = transmitter;
        }
        public void SendmailForJob(string subscriber, string subject, string title)
        {
            var allItems = new List<Models.NotificationEntry>();

            GetItemsFromNotificationGroup(allItems, subscriber);

            var items = DBContext.NotificationEntries.Where(n =>
                n.NotificationType == NotificationType
                && n.SubscriptionID == subscriber
            );

            allItems.AddRange(items);

            items = allItems.AsQueryable()
                .OrderBy(n => n.Duedate)
                .ThenBy(n => n.Startdate)
                .ThenBy(n => n.Enddate);

            if (items.Any())
            {
                Logger.LogDebug($"Found {items.Count()} {NotificationType} items for subscriber {subscriber}.");

                INotificationFormatter formatter = new NotificationMailFormatter
                {
                    Template = MailTemplate,
                    TableTitle = title,
                    Salutation = items.First( n => !string.IsNullOrEmpty(n.Salutation) )?.Salutation ?? "team member"
                };

                var message = formatter.FormatMessage(items.ToList());

                Transmitter.Recipient = subscriber;
                Transmitter.Transmit(subject, message);
            }
            else
            {
                Logger.LogInformation($"No {NotificationType} items for subscriber {subscriber}.");
            }
        }

        private void GetItemsFromNotificationGroup(List<NotificationEntry> allItems, string subscriber)
        {
            var groupsForSubscriber = DBContext.NotificationGroups
                .Where(g => g.SubscriptionID == subscriber)
                .Select(g => g.SubscriptionGroup);

            foreach (string item in groupsForSubscriber)
            {
                var itemsForGoup = DBContext.NotificationEntries
                    .Where(e => e.SubscriptionGroup == item);

                Logger.LogDebug($"Found {itemsForGoup.Count()} {NotificationType} of group {item} items for subscriber {subscriber}.");

                allItems.AddRange(itemsForGoup);
            }
        }
    }
}
