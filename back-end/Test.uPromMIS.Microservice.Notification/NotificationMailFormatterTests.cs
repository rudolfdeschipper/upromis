using Microsoft.VisualStudio.TestTools.UnitTesting;
using uPromis.Microservice.Notification.Transmitter;
using Moq;
using Microsoft.Extensions.Logging;
using uPromis.Microservice.Notification.MessageFormatter;
using uPromis.Services.Notification;

namespace Test.uPromMIS.Microservice.Notification
{
    [TestClass]
    public class NotificationMailFormatterTests
    {
        [TestMethod]
        public void NotificationMailFormatterNormal()
        {
            var notificationMailFormatter = new NotificationMailFormatter
            {
                Salutation = "John",
                TableTitle = "Table title",
                Template = "{TableTitle}|{Salutation}|{Items}"
            };

            var items = new System.Collections.Generic.List<uPromis.Microservice.Notification.Model.NotificationEntry>
            {
                new uPromis.Microservice.Notification.Model.NotificationEntry()
                {
                    Code = "Code 1",
                    Description = "Description 1",
                    Duedate = new System.DateTime(2021, 4, 3),
                    Enddate = new System.DateTime(2021, 4, 3),
                    ID = 1,
                    NotificationType = "Contract",
                    Salutation = "John",
                    Startdate = new System.DateTime(2021, 4, 2),
                    SubscriptionID = "e@m.com",
                    URL = "http://www.google.com/"
                },
                new uPromis.Microservice.Notification.Model.NotificationEntry()
                {
                    Code = "Code 2",
                    Description = "Description 2",
                    Duedate = new System.DateTime(2021, 4, 3),
                    Enddate = new System.DateTime(2021, 4, 3),
                    ID = 1,
                    NotificationType = "Contract",
                    Salutation = "John",
                    Startdate = new System.DateTime(2021, 4, 2),
                    SubscriptionID = "e@m.com",
                    URL = "http://www.google.com/"
                }
            };
            var message = notificationMailFormatter.FormatMessage(items);

            var messageParts = message.Split('|');

            Assert.AreEqual("Table title", messageParts[0]);
            Assert.AreEqual("John", messageParts[1]);
            Assert.IsTrue(messageParts[2].Contains("Code 1"));
            Assert.IsTrue(messageParts[2].Contains("Code 2"));

        }
        [TestMethod]
        public void NotificationMailFormatterNoTemplate()
        {
            var notificationMailFormatter = new NotificationMailFormatter
            {
                Salutation = "John",
                TableTitle = "Table title"
            };

            var items = new System.Collections.Generic.List<uPromis.Microservice.Notification.Model.NotificationEntry>
            {
                new uPromis.Microservice.Notification.Model.NotificationEntry()
                {
                    Code = "Code 1",
                    Description = "Description 1",
                    Duedate = new System.DateTime(2021, 4, 3),
                    Enddate = new System.DateTime(2021, 4, 3),
                    ID = 1,
                    NotificationType = "Contract",
                    Salutation = "John",
                    Startdate = new System.DateTime(2021, 4, 2),
                    SubscriptionID = "e@m.com",
                    URL = "http://www.google.com/"
                },
                new uPromis.Microservice.Notification.Model.NotificationEntry()
                {
                    Code = "Code 2",
                    Description = "Description 2",
                    Duedate = new System.DateTime(2021, 4, 3),
                    Enddate = new System.DateTime(2021, 4, 3),
                    ID = 1,
                    NotificationType = "Contract",
                    Salutation = "John",
                    Startdate = new System.DateTime(2021, 4, 2),
                    SubscriptionID = "e@m.com",
                    URL = "http://www.google.com/"
                }
            };
            var message = notificationMailFormatter.FormatMessage(items);

            Assert.AreEqual(string.Empty, message);
        }
    }
}
