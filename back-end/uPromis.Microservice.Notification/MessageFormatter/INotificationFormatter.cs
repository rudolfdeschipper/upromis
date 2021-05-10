using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uPromis.Microservice.Notification.MessageFormatter
{
    public interface INotificationFormatter
    {
        public string Template { get; set; }
        public string Salutation { get; set; }
        public string TableTitle { get; set; }
        public string FormatMessage(IList<Notification.Models.NotificationEntry> items);
    }
}
