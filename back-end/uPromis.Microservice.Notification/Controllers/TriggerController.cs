using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPromis.Services;
using Quartz;
using Microsoft.Extensions.Logging;
using uPromis.Microservice.Notification.Model;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace uPromis.Microservice.Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
        public IScheduler Scheduler { get; set; }
        public ILogger Logger { get; set; }
        public TriggerController(IScheduler scheduler, ILoggerProvider loggerProvider )
        {
            Scheduler = scheduler;
            Logger = loggerProvider.CreateLogger(nameof(TriggerController));
        }

        // GET: api/<TriggerController>
        [HttpGet("heartbeat")]
        [AllowAnonymous]
        public ActionResult<string> Heartbeat()
        {
            Logger.LogInformation($"Heartbeat");
            return Ok("Heartbeat");
        }


        // GET: api/<TriggerController>
        [HttpGet]
        public async Task<IEnumerable<Services.Notification.NotificationSchedule>> Get(string subscriber)
        {
            Logger.LogInformation($"Get Triggers for {subscriber}");
            var triggers = await Scheduler.GetTriggersOfJob(new JobKey(subscriber));

            return triggers.Select(t => new Services.Notification.NotificationSchedule(
               subscriber,
               t.Key.Group,
               (t as ICronTrigger).CronExpressionString.ToUpper().Split(" "),
               Scheduler.GetTriggerState(t.Key).Result == TriggerState.Paused)).AsEnumerable();
                
        }

        // GET api/<TriggerController>/5
        [HttpGet("{subscriber}/{notificationType}")]
        public async Task<Services.Notification.NotificationSchedule> Get(string subscriber, string notificationType)
        {
            Logger.LogInformation($"Get Trigger for {subscriber} - {notificationType}");

            var trigger = await Scheduler.GetTrigger(new TriggerKey(subscriber, notificationType)) as ICronTrigger;
            var scheduleElements = trigger.CronExpressionString.ToUpper().Split(" ");

            Services.Notification.NotificationSchedule retVal = new Services.Notification.NotificationSchedule(
                subscriber,
                notificationType,
                scheduleElements,
                Scheduler.GetTriggerState(trigger.Key).Result == TriggerState.Paused);

            return retVal;
        }

        // POST api/<TriggerController>
        [HttpPost]
        public async void Post([FromBody] Services.Notification.NotificationSchedule value)
        {
            Logger.LogInformation($"Post Trigger for {value.SubscriberID} - {value.NotificationType}");
            try
            {

                var triggerCreator = new TriggerCreator(Scheduler);

                var trigger = await triggerCreator.FillTriggerAsync(value);

                await Scheduler.Standby();
                await Scheduler.ScheduleJob(trigger);
                // created trigger is not started, so need to do that:
                if (value.Enable)
                {
                    await Scheduler.ResumeTrigger(trigger.Key);
                }
                else
                {
                    await Scheduler.PauseTrigger(trigger.Key);
                }
            }
            finally
            {
                await Scheduler.Start();
            }
        }


        // PUT api/<TriggerController>/5
        [HttpPut("{subscriber}/{notificationType}")]
        public async void Put([FromBody] Services.Notification.NotificationSchedule value)
        {
            Logger.LogInformation($"Put Trigger for {value.SubscriberID} - {value.NotificationType}");
            try
            {
                var triggerCreator = new TriggerCreator(Scheduler);

                var trigger = await triggerCreator.FillTriggerAsync(value);

                await Scheduler.Standby();
                await Scheduler.ScheduleJob(trigger);
                // created trigger is not started, so need to do that:
                if (value.Enable)
                {
                    await Scheduler.ResumeTrigger(trigger.Key);
                }
                else
                {
                    await Scheduler.PauseTrigger(trigger.Key);
                }
            }
            finally
            {
                await Scheduler.Start();
            }
        }

        // DELETE api/<TriggerController>/5
        [HttpDelete("{subscriber}/{notificationType}")]
        public async void Delete(string subscriber, string notificationType)
        {
            Logger.LogInformation($"Delete Trigger for {subscriber} - {notificationType}");

            await Scheduler.PauseTrigger(new TriggerKey(subscriber, notificationType));
            // do not delete the job as this will kill everything for all users
            //await Scheduler.DeleteJob(new JobKey(subscriber, notificationType));
        }
    }
}
