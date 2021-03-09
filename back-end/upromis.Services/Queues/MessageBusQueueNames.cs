using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uPromis.Services.Queues
{
    public class MessageBusQueueNames
    {
        public const string REPORTSERVERSAVECONTRACT = "ReportServer.Contract.Save.Queue";
        public const string REPORTSERVERDELETECONTRACT = "ReportServer.Contract.Delete.Queue";

        public const string REPORTSERVERSAVEACCOUNTINFO = "Report.Server.AccountInfo.Save.Queue";
        public const string REPORTSERVERDELETEACCOUNTINFO = "ReportServer.AccountInfo.Delete.Queue";

        public const string REPORTSERVERSAVEREQUEST = "ReportServer.Request.Save.Queue";
        public const string REPORTSERVERDELETEREQUEST = "ReportServer.Request.Delete.Queue";
        
        public const string REPORTSERVERSAVEPROPOSAL = "ReportServer.Proposal.Save.Queue";
        public const string REPORTSERVERDELETEPROPOSAL = "ReportServer.Proposal.Delete.Queue";

        public const string ADDNOTIFYITEM = "Notification.AddNotifyItem.Queue";
        public const string REMOVENOTIFYITEM = "Notification.RemoveNotifyItem.Queue";
    }
}
