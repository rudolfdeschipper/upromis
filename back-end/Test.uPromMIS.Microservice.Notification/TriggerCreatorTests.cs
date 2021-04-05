using Microsoft.VisualStudio.TestTools.UnitTesting;
using uPromis.Microservice.Notification.Transmitter;
using Moq;
using Microsoft.Extensions.Logging;
using Quartz;
using uPromis.Microservice.Notification.Model;
using uPromis.Services.Notification;

namespace uPromis.Microservice.Notification.Tests
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

            var notificationSchedule = new NotificationSchedule
            {
                SubscriberID = "e@m.com",
                NotificationType = "notificationType",
                RunOnSunday = false,
                RunOnMonday = false,
                RunOnTuesday = false,
                RunOnWednesday = false,
                RunOnThursday = false,
                RunOnFriday = true,
                RunOnSaturday = false,
                RunOnHour = 23,
                RunOnMinute = 15
            };

            var trigger = triggerCreator.FillTriggerAsync(notificationSchedule);
            trigger.Wait();
            var itrigger = trigger.Result;

            // trigger can only look into the future, so need to set up the dates based on today:
            var today = System.DateTime.Today;
            // find next thursday:
            var thursday = today.AddDays(System.DayOfWeek.Thursday - today.DayOfWeek);
            if (thursday < today)
            {
                thursday = thursday.AddDays(7);
            }

            var friday = thursday.AddDays(1);
            var fridayNextWeek = friday.AddDays(7);
            // thursday 23:22
            var referenceDate1 = new System.DateTime(thursday.Year, thursday.Month, thursday.Day, 23, 22, 0);
            // expect next friday, 23:15
            var expectDate1 = new System.DateTime(friday.Year, friday.Month, friday.Day, 23, 15, 0);

            // friday 23:22
            var referenceDate2 = new System.DateTime(friday.Year, friday.Month, friday.Day, 23, 22, 0);
            // expect next week friday 23:15
            var expectDate2 = new System.DateTime(fridayNextWeek.Year, fridayNextWeek.Month, fridayNextWeek.Day, 23, 15, 0);

            Assert.AreEqual("e@m.com", itrigger.Key.Name);
            Assert.AreEqual("notificationType", itrigger.JobKey.Name);
            Assert.AreEqual<System.DateTimeOffset>(new System.DateTimeOffset(expectDate1), itrigger.GetFireTimeAfter(new System.DateTimeOffset(referenceDate1)).Value);
            Assert.AreEqual<System.DateTimeOffset>(new System.DateTimeOffset(expectDate2), itrigger.GetFireTimeAfter(new System.DateTimeOffset(referenceDate2)).Value);
        }
    }
}
