using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace uPromis.Microservice.Notification.Job
{
    public class ReminderJob : IJob
    {
        private readonly ILogger Logger;
        private readonly Data.NotificationDbContext DBContext;
        public ReminderJob(ILoggerProvider loggerProvider, Data.NotificationDbContext dbContext)
        {
            DBContext = dbContext;
            Logger = loggerProvider.CreateLogger(nameof(ReminderJob));
        }
        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation("Execute job's task");

            return Task.CompletedTask;
        }
    }
}
