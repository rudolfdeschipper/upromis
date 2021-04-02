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
    public class EmailTransmitter : IMessageTransmitter
    {
        private readonly ILogger Logger;
        private readonly string Server;
        private readonly string Port;
        private readonly string Username;
        private readonly string Password;
        public SmtpClient Client { get; set; }
        public EmailTransmitter(ILogger logger, string server, string port, string username, string password)
        {
            Logger = logger;
            Server = server;
            Port = port;
            Username = username;
            Password = password;
            Client = new SmtpClient();
        }

        public List<string> CC { get; set; }
        public string Recipient { get; set; }
        public string Sender { get; set; }

        public void Transmit(string subject, string messageBody)
        {
            MailMessage message = new MailMessage();

            if (!string.IsNullOrWhiteSpace(Sender))
            {
                Logger.LogInformation($"Using provider sender {Sender}");
                message.From = new MailAddress(Sender.Trim());
            }
            else
            {
                Logger.LogInformation($"Using default sender {Resources.mailSender}");
                message.From = new MailAddress(Resources.mailSender);
            }

            if (Recipient != null)
            {
                Recipient = Recipient?.TrimEnd(';');
                foreach (string _to in Recipient?.Split(';'))
                {
                    message.To.Add(new MailAddress(_to.Trim()));
                }
                Logger.LogInformation($"Recipient(s): {Recipient}");
            }
            else
            {
                Logger.LogError($"Recipient(s) not specified.");
                return;
            }

            if (CC != null && CC.Any())
            {
                Logger.LogInformation($"Adding {CC.Count} CCs");
                foreach (string _cc in CC)
                {
                    message.CC.Add(new MailAddress(_cc.Trim()));
                }
            }
            Client.Host = Server;
            Client.Port = Convert.ToInt32(Port);
            message.Subject = subject;

            string htmlMaster = Resources.EmailMasterTemplate;
            message.Body = htmlMaster.Replace("{body}", messageBody);
            message.IsBodyHtml = true;

            if (Resources.mailAuthenticationNeeded == "true")
            {
                Logger.LogInformation($"Providing Mail Credentials");
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(Username, Password);
                Client.Credentials = credentials;
            }
            try
            {
                Logger.LogInformation($"Sending message with subject {message.Subject} to {message.To}");
                Client.Send(message);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while sending the mail: {ex.Message}");
            }
        }
    }
}
