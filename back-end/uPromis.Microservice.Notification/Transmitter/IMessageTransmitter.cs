using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uPromis.Microservice.Notification.Transmitter
{
    public interface IMessageTransmitter
    {
        public string Recipient { get; set; }
        public string Sender { get; set; }
        public void Transmit(string subject, string message);
    }
}
