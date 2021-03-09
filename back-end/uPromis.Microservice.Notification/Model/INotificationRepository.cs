using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uPromis.Microservice.Notification.Model
{
    public interface INotificationRepository
    {
        IQueryable<Notification.Model.NotificationEntry> List { get; }

        Task<Notification.Model.NotificationEntry> Get(string SubscriptionID, string NotificationType, string URL);

        Task<Notification.Model.NotificationEntry> Post(Notification.Model.NotificationEntry rec);
        Task<Notification.Model.NotificationEntry> Put(Notification.Model.NotificationEntry rec);
        Task<bool> Delete(Notification.Model.NotificationEntry rec);

    }
}