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
        private readonly IMessageTransmitter Transmitter;
        public ContractJob(ILoggerProvider loggerProvider, Data.NotificationDbContext dbContext, IMessageTransmitter transmitter)
        {
            DBContext = dbContext;
            Logger = loggerProvider.CreateLogger(nameof(ContractJob));
            LoggerProvider = loggerProvider;
            Transmitter = transmitter;
        }
        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"Execute {nameof(ContractJob)} job's task");
            var Subscriber = context.Trigger.Key.Name;

            var jobMailSender = new JobMailsender(LoggerProvider, DBContext, Transmitter, NotificationType, Resources.ProjectReminderMailItem);

            jobMailSender.SendmailForJob(Subscriber, "Contract reminder", "Contract items");

            return Task.CompletedTask;
        }
    }
}
