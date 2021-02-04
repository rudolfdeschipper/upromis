using System;
using System.Collections.Generic;

namespace uPromis.Microservice.AttachmentAPI.Models
{
    public class AttachmentDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string UploadedBy { get; set; }
        public DateTimeOffset UploadedOn { get; set; }
    }

}