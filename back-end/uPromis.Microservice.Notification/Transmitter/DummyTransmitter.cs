using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using uPromis.Microservice.Notification.Properties;

namespace uPromis.Microservice.Notification.Transmitter
{
    /// <summary>
    /// Transmitter class that does not transmit anything, just logs the action
    /// Used for debugging and testing.
    /// </summary>
    public class DummyTransmitter : IMessageTransmitter
    {
        private readonly ILogger Logger;
        private readonly string Server;
        private readonly string Port;
        private readonly string Username;
        private readonly string Password;

        public DummyTransmitter(ILogger logger, string server, string port, string username, string password)
        {
            Logger = logger;
            Server = server;
            Port = port;
            Username = username;
            Password = password;
        }

        public string Recipient { get; set; }
        public string Sender { get; set; }

        public void Transmit(string subject, string message)
        {
            Logger.LogInformation($"Sending message {subject} {message} from {Sender} to {Recipient}. Server = {Server}:{Port} Username: {Username}");
        }
    }
}
