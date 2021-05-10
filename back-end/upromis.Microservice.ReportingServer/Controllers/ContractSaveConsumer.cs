using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using uPromis.Services.Models;

namespace uPromis.Microservice.ReportingServer.Controllers
{
    public class ContractSaveConsumer : IConsumer<ContractDTO>
    {
        private readonly ILogger Logger;
        public ContractSaveConsumer(ILoggerProvider loggerProvider
            )
        {
            Logger = loggerProvider.CreateLogger("ContractSaveConsumer");
        }
        public Task Consume(ConsumeContext<ContractDTO> context)
        {
            Logger.LogInformation("Receive Contract save: {0} - {1}", context.Message.ID, context.Message.Code);
            return Task.CompletedTask;
        }
    }
}
