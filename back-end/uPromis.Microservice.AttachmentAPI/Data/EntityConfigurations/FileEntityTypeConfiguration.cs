using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uPromis.Microservice.AttachmentAPI.Models;

namespace uPromis.Microservice.AttachmentAPI.data.EntityConfigurations
{
    class AttachmentEntityTypeConfiguration
        : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("files");
            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasMaxLength(40);
            builder.Property(e => e.FileName)
                .IsRequired()
                .HasColumnName("filename")
                .HasMaxLength(256)
                .IsUnicode(false);
            builder.Property(e => e.Size)
                .IsRequired()
                .HasColumnName("size")
                .HasColumnType("bigint");
            builder.Property(e => e.ParentItem)
                .IsRequired()
                .HasColumnName("parentitem")
                .HasMaxLength(40);
            builder.Property(e => e.BlobContainer)
                .IsRequired()
                .HasColumnName("blobcontainer")
                .IsUnicode(false);
            builder.Property(e => e.BlobName)
                .IsRequired()
                .HasColumnName("blobname")
                .IsUnicode(false);
            builder.Property(e => e.UploadedBy)
                    .IsRequired()
                    .HasColumnName("uploadedby")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            builder.Property(e => e.UploadedOn)
                .HasColumnName("uploadedon")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}