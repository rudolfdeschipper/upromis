using Microsoft.VisualStudio.TestTools.UnitTesting;
using uPromis.Microservice.Notification.Transmitter;
using Moq;
using Microsoft.Extensions.Logging;
using Quartz;
using uPromis.Microservice.Notification.Model;
using uPromis.Services.Notification;

namespace Test.uPromMIS.Microservice.Notification
{
    [TestClass]
    public class TriggerCreatorTests
    {
        [TestMethod]
        public void TestTriggerCreatorNormal() 
        {
            //var loggerMoq = new Mock<ILogger>();
            var quartzMock = new Mock<IScheduler>();

            //var logger = loggerMoq.Object;
            var quartz = quartzMock.Object;

            var triggerCreator = new TriggerCreator(quartz);

            var notificationSchedule = new NotificationSchedule();
            notificationSchedule.SubscriberID = "e@m.com";
            notificationSchedule.NotificationType = "notificationType";
            notificationSchedule.RunOnSunday = false;
            notificationSchedule.RunOnMonday = false;
            notificationSchedule.RunOnTuesday = false;
            notificationSchedule.RunOnWednesday = false;
            notificationSchedule.RunOnThursday = false;
            notificationSchedule.RunOnFriday = true;
            notificationSchedule.RunOnSaturday = false;
            notificationSchedule.RunOnHour = 23;
            notificationSchedule.RunOnMinute = 15;

            var trigger = triggerCreator.FillTriggerAsync(notificationSchedule);
            trigger.Wait();
            var itrigger = trigger.Result;

            Assert.AreEqual("e@m.com", itrigger.Key.Name);
            Assert.AreEqual("notificationType", itrigger.JobKey.Name);
            Assert.AreEqual<System.DateTimeOffset>(new System.DateTimeOffset(new System.DateTime(2021, 4, 2, 23, 15, 0)), itrigger.GetFireTimeAfter(new System.DateTimeOffset(new System.DateTime(2021, 4, 1, 23, 22, 0))).Value);
            Assert.AreEqual<System.DateTimeOffset>(new System.DateTimeOffset(new System.DateTime(2021, 4, 9, 23, 15, 0)), itrigger.GetFireTimeAfter(new System.DateTimeOffset(new System.DateTime(2021, 4, 2, 23, 22, 0))).Value);
        }
    }
}
