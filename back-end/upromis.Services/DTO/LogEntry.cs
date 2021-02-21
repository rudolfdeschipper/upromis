using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uPromis.Services.BusinessLogging.DTO
{
    public class LogEntry
    {
        public LogEntry()
        {
            Timestamp = DateTime.Now;
        }

        public int ID { get; set; }
        public int Project { get; set; }
        public int Level { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
