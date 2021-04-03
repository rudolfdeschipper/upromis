using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPromis.Microservice.Notification.Model;
using uPromis.Microservice.Notification.Properties;

namespace uPromis.Microservice.Notification.MessageFormatter
{
    public class NotificationMailFormatter : INotificationFormatter
    {
        public string Template { get; set; }
        public string Salutation { get; set; }
        public string TableTitle { get; set; }

        public string FormatMessage(IList<NotificationEntry> items)
        {
            if (string.IsNullOrEmpty(Template))
            {
                return string.Empty;
            }
            var tableBuilder = new HTMLTableBuilder<NotificationEntry>();
            var table = tableBuilder.Build(TableTitle,
                new string[] { "URL", "Code", "Description", "Due date", "Start date", "End date"},
                items,
                (item, tb) => {
                    tb.AddCell("", item.URL, item.ID.ToString());
                    tb.AddCell(item.Code);
                    tb.AddCell(item.Description);
                    tb.AddCell(item.Duedate);
                    tb.AddCell(item.Startdate);
                    tb.AddCell(item.Enddate);
                }
                );
            var message = Template;

            message = message.Replace("{Items}", table);
            message = message.Replace("{Salutation}", Salutation);

            return message;
        }
    }
}
