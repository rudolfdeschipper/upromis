using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upromis.Services.DTO;

namespace upromis.Microservice.ReportingServer.Controllers
{
    public class ContractCreateConsumer : IConsumer<Services.DTO.ContractReportEntry>
    {
        private readonly ILogger Logger;
        public ContractCreateConsumer(ILoggerProvider loggerProvider
            )
        {
            Logger = loggerProvider.CreateLogger("ContractCreateConsumer");
        }
        public Task Consume(ConsumeContext<ContractReportEntry> context)
        {
            Logger.LogInformation("Receive Contract create: {0} - {1}", context.Message.ID, context.Message.Code);
            return Task.CompletedTask;
        }
    }
}
