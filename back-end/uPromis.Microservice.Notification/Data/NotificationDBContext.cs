using Microsoft.EntityFrameworkCore;
using uPromis.Microservice.Notification.Model;

namespace uPromis.Microservice.Notification.Data
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationEntry>()
                .HasKey(new string[] { "NotificationType", "SubscriptionID", "URL" });

        }
        public DbSet<NotificationEntry> NotificationEntries { get; set; }

    }
}                   
