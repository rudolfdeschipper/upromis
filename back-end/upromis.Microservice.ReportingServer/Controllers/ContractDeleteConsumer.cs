using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using uPromis.Services.Models;

namespace uPromis.Microservice.ReportingServer.Controllers
{
    public class ContractDeleteConsumer : IConsumer<ContractDTO>
    {
        private readonly ILogger Logger;
        public ContractDeleteConsumer(ILoggerProvider loggerProvider
            )
        {
            Logger = loggerProvider.CreateLogger("ContractDeleteConsumer");
        }
        public Task Consume(ConsumeContext<ContractDTO> context)
        {
            Logger.LogInformation("Receive Contract delete: {0} - {1}", context.Message.ID, context.Message.Code);
            return Task.CompletedTask;
        }
    }
}
