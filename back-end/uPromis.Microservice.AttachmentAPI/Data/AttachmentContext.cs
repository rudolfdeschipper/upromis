using Microsoft.EntityFrameworkCore;
using uPromis.Microservice.AttachmentAPI.Models;
using uPromis.Microservice.AttachmentAPI.data.EntityConfigurations;

namespace uPromis.Microservice.AttachmentAPI
{ 
    public class AttachmentContext : DbContext
    {
        public AttachmentContext(DbContextOptions<AttachmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttachmentEntityTypeConfiguration());
        }

    }
}