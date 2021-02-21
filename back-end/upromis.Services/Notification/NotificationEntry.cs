using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uPromis.Services.Notification
{
    class NotificationEntry
    {
        /// <summary>
        /// ID of the time for which we want notifications
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// URL so the user can immediate jump to the item
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Short item Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Long title of description of the notification item
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// used to group notifications by type
        /// </summary>
        public string NotificationType { get; set; }
        /// <summary>
        /// What job type this notification will be attached to
        /// </summary>
        public string JobType { get; set; }
        /// <summary>
        /// Item's start date
        /// </summary>
        public DateTime? Startdate { get; set; }
        /// <summary>
        /// Item's end date
        /// </summary>
        public DateTime? Enddate { get; set; }
        /// <summary>
        /// Item's due date
        /// </summary>
        public DateTime Duedate { get; set; }
    }
}
