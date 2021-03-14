using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using uPromis.Microservice.Notification.Properties;

namespace uPromis.Microservice.Notification.Transmitter
{
    public class EmailTransmitter : IMessageTransmitter
    {
        private readonly ILogger Logger;
        public EmailTransmitter(ILoggerProvider loggerProvider)
        {
            Logger = loggerProvider.CreateLogger(nameof(EmailTransmitter));
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

            Recipient = Recipient.TrimEnd(';');
            foreach (string _to in Recipient.Split(';'))
            {
                message.To.Add(new MailAddress(_to.Trim()));
            }
            Logger.LogInformation($"Recipient(s): {Recipient}");

            if (CC.Any())
            {
                Logger.LogInformation($"Adding {CC.Count} CCs");
                foreach (string _cc in CC)
                {
                    message.CC.Add(new MailAddress(_cc.Trim()));
                }
            }

            message.Subject = subject;

            string htmlMaster = Resources.EmailMasterTemplate;
            message.Body = htmlMaster.Replace("{body}", messageBody);
            message.IsBodyHtml = true;

            string server = Resources.mailServer;
            string port = Resources.mailServerPort;
            SmtpClient client = new SmtpClient(server, Convert.ToInt32(port));

            if (Resources.mailAuthenticationNeeded == "true")
            {
                Logger.LogInformation($"Providing Mail Credentials");
                string username = Resources.mailUsername;
                string password = Resources.mailPassword;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(username, password);
                client.Credentials = credentials;
            }
            try
            {
                Logger.LogInformation($"Sending message with subject {message.Subject} to {message.To}");
                client.Send(message);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred while sending the mail: {ex.Message}");
            }
        }
    }
}
