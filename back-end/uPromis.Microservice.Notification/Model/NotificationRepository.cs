using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPromis.Microservice.Notification.Data;

namespace uPromis.Microservice.Notification.Model
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext Context;

        public NotificationRepository(NotificationDbContext context)
        {
            Context = context;
        }

        public IQueryable<NotificationEntry> List => Context.NotificationEntries;

        public async Task<bool> Delete(NotificationEntry rec)
        {
            var record = await Context.NotificationEntries.Where(
                c => c.SubscriptionID == rec.SubscriptionID 
                && c.NotificationType == rec.NotificationType
                && c.URL == rec.URL
                )
                .FirstOrDefaultAsync();

            if (record == null)
            {
                return false;
            }

            Context.NotificationEntries.Remove(record);

            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<NotificationEntry> Get(string SubscriptionID, string NotificationType, string URL)
        {
            var record = await Context.NotificationEntries.Where(
                c => c.SubscriptionID == SubscriptionID 
                && c.NotificationType == NotificationType
                && c.URL == URL)
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<NotificationEntry> Post(NotificationEntry rec)
        {
            var record = rec;

            Context.NotificationEntries.Add(record);

            await Context.SaveChangesAsync();

            return record;
        }

        public async Task<NotificationEntry> Put(NotificationEntry rec)
        {
            Context.NotificationEntries.Attach(rec);

            await Context.SaveChangesAsync();

            return rec;
        }
    }
}
