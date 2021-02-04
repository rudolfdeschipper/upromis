using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upromis.Services.DTO
{
    public class ContractReportEntry
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string Status { get; set; }
        public decimal Budget { get; set; }
        public string Proposal { get; set; }
    }
}
