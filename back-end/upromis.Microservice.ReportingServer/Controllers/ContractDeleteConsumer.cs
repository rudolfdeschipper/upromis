using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upromis.Services.DTO;

namespace upromis.Microservice.ReportingServer.Controllers
{
    public class ContractDeleteConsumer : IConsumer<Services.DTO.ContractReportEntry>
    {
        private readonly ILogger Logger;
        public ContractDeleteConsumer(ILoggerProvider loggerProvider
            )
        {
            Logger = loggerProvider.CreateLogger("ContractDeleteConsumer");
        }
        public Task Consume(ConsumeContext<ContractReportEntry> context)
        {
            Logger.LogInformation("Receive Contract delete: {0} - {1}", context.Message.ID, context.Message.Code);
            return Task.CompletedTask;
        }
    }
}
