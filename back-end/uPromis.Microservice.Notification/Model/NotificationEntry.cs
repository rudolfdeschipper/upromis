using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace uPromis.Microservice.Notification.Model
{
    public class NotificationEntry
    {
        /// <summary>
        /// ID of the time for which we want notifications
        /// </summary>
        [Required]
        public int ID { get; set; }
        /// <summary>
        /// URL so the user can immediate jump to the item
        /// </summary>
        [Required, Url]
        public string URL { get; set; }
        /// <summary>
        /// Short item Code
        /// </summary>
        [Required, MaxLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// Long title of description of the notification item
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// What notification type (Contract, Project, Reminder)
        /// </summary>
        [Required, MaxLength(50)]
        public string NotificationType { get; set; }
        /// <summary>
        /// What Subscriber this receive the notification
        /// </summary>
        [Required, EmailAddress]
        public string SubscriptionID { get; set; }
        /// <summary>
        /// Item's start date
        /// </summary>
        [Required, DataType(DataType.DateTime)]
        public DateTime? Startdate { get; set; }
        /// <summary>
        /// Item's end date
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? Enddate { get; set; }
        /// <summary>
        /// Item's due date
        /// </summary>
        [Required, DataType(DataType.DateTime)]
        public DateTime Duedate { get; set; }
    }
}
