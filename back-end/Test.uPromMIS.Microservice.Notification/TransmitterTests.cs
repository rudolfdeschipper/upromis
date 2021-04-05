using Microsoft.VisualStudio.TestTools.UnitTesting;
using uPromis.Microservice.Notification.Transmitter;
using Moq;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace uPromis.Microservice.Notification.Tests
{
    [TestClass]
    public class TransmitterTests
    {
        [TestMethod]
        public void TestTransmitterNormal()
        {
            var loggerMoq = new Mock<ILogger>();
            var smtpMoq = new Mock<SmtpClient>();
            //smtpMoq.Setup(smtpMoq => smtpMoq.Send(It.IsAny<MailMessage>())).Verifiable();

            var logger = loggerMoq.Object;

            var transmitter = new EmailTransmitter(logger, "server", "100", "user", "password")
            {
                Client = smtpMoq.Object,
                Recipient = "to@mail.com"
            };
            transmitter.Transmit("", "");

            Assert.AreEqual("server", transmitter.Client.Host);
            Assert.AreEqual(100, transmitter.Client.Port);

        }
        [TestMethod]
        public void TestTransmitterNoRecipient()
        {
            var loggerMoq = new Mock<ILogger>();
            var smtpMoq = new Mock<SmtpClient>();
            //smtpMoq.Setup(smtpMoq => smtpMoq.Send(It.IsAny<MailMessage>())).Verifiable();

            var logger = loggerMoq.Object;

            var transmitter = new EmailTransmitter(logger, "server", "100", "user", "password")
            {
                Client = smtpMoq.Object
            };
            transmitter.Transmit("", "");

            Assert.AreNotEqual("server", transmitter.Client.Host);
            Assert.AreNotEqual(100, transmitter.Client.Port);

        }
        [TestMethod]
        public void TestDummyTransmitterNormal()
        {
            var loggerMoq = new Mock<ILogger>();
            var logger = loggerMoq.Object;
            var transmitter = new DummyTransmitter(logger, "server", "100", "user", "password")
            {
                Recipient = "to@mail.com"
            };
            transmitter.Transmit("", "");

            Assert.AreEqual("to@mail.com", transmitter.Recipient);

        }
    }
}
