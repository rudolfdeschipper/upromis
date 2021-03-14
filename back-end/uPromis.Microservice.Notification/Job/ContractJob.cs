using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using uPromis.Microservice.Notification.MessageFormatter;
using uPromis.Microservice.Notification.Properties;
using uPromis.Microservice.Notification.Transmitter;

namespace uPromis.Microservice.Notification.Job
{
    public class ContractJob : IJob
    {
        private readonly ILogger Logger;
        private readonly ILoggerProvider LoggerProvider;
        private readonly Data.NotificationDbContext DBContext;
        private readonly string NotificationType = Services.Notification.NotificationType.CONTRACTNOTIFICATION;
        public ContractJob(ILoggerProvider loggerProvider, Data.NotificationDbContext dbContext)
        {
            DBContext = dbContext;
            Logger = loggerProvider.CreateLogger(nameof(ContractJob));
            LoggerProvider = loggerProvider;
        }
        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"Execute {nameof(ContractJob)} job's task");
            var Subscriber = context.Trigger.Key.Name;

            IMessageTransmitter transmitter = new EmailTransmitter(LoggerProvider);

            var jobMailSender = new JobMailsender(LoggerProvider, DBContext, transmitter, NotificationType, Resources.ProjectReminderMailItem);

            jobMailSender.SendmailForJob(Subscriber, "Contract reminder", "Contract items");

            return Task.CompletedTask;
        }
    }
}
