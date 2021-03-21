using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using uPromis.Microservice.Notification.Properties;
using uPromis.Microservice.Notification.Transmitter;

namespace uPromis.Microservice.Notification.Job
{
    public class ReminderJob : IJob
    {
        private readonly ILogger Logger;
        private readonly ILoggerProvider LoggerProvider;
        public IMessageTransmitter Transmitter { get; }
        private readonly Data.NotificationDbContext DBContext;
        private readonly string NotificationType = Services.Notification.NotificationType.REMINDERNOTIFICATION;

        public ReminderJob(ILoggerProvider loggerProvider, Data.NotificationDbContext dbContext, IMessageTransmitter transmitter)
        {
            DBContext = dbContext;
            Logger = loggerProvider.CreateLogger(nameof(ReminderJob));
            LoggerProvider = loggerProvider;
            Transmitter = transmitter;
        }
        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"Execute {nameof(ReminderJob)} job's task");
            var Subscriber = context.Trigger.Key.Name;

            var jobMailSender = new JobMailsender(LoggerProvider, DBContext, Transmitter, NotificationType, Resources.ProjectReminderMailItem);

            jobMailSender.SendmailForJob(Subscriber, "Reminders", "Items you need to look at");

            return Task.CompletedTask;
        }
    }
}
