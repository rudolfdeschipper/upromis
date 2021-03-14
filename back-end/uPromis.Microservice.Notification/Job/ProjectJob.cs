using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using uPromis.Microservice.Notification.Properties;

namespace uPromis.Microservice.Notification.Job
{
    public class ProjectJob : IJob
    {
        private readonly ILogger Logger;
        private readonly ILoggerProvider LoggerProvider;
        private readonly Data.NotificationDbContext DBContext;
        private readonly string NotificationType = Services.Notification.NotificationType.PROJECTNOTIFICATION;
        public ProjectJob(ILoggerProvider loggerProvider, Data.NotificationDbContext dbContext)
        {
            DBContext = dbContext;
            Logger = loggerProvider.CreateLogger(nameof(ProjectJob));
            LoggerProvider = loggerProvider;
        }
        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"Execute {nameof(ProjectJob)} job's task");
            var Subscriber = context.Trigger.Key.Name;

            var jobMailSender = new JobMailsender(LoggerProvider, DBContext, NotificationType, Resources.ProjectReminderMailItem);

            jobMailSender.SendmailForJob(Subscriber, "Project reminder", "Project items");

            return Task.CompletedTask;
        }
    }
}
