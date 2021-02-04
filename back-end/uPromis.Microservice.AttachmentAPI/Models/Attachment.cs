using System;

namespace uPromis.Microservice.AttachmentAPI.Models
    {public class Attachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string BlobContainer { get; set; }
        public string BlobName { get; set; }
        public string UploadedBy { get; set; }
        public Guid ParentItem { get; set; }
        public DateTimeOffset UploadedOn { get; set; }
    }
}