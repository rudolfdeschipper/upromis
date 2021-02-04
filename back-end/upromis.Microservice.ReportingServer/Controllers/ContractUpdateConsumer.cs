using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upromis.Services.DTO;

namespace upromis.Microservice.ReportingServer.Controllers
{
    public class ContractUpdateConsumer : IConsumer<Services.DTO.ContractReportEntry>
    {
        private readonly ILogger Logger;
        public ContractUpdateConsumer(ILoggerProvider loggerProvider
            )
        {
            Logger = loggerProvider.CreateLogger("ContractUpdateConsumer");
        }
        public Task Consume(ConsumeContext<ContractReportEntry> context)
        {
            Logger.LogInformation("Receive Contract update: {0} - {1}", context.Message.ID, context.Message.Code);
            return Task.CompletedTask;
        }
    }
}
