using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace upromis.Microservice.ReportingServer.Controllers
{
    public class ContractSaveConsumer : IConsumer<uPromis.Microservice.ContractAPI.Models.ContractDTO>
    {
        private readonly ILogger Logger;
        public ContractSaveConsumer(ILoggerProvider loggerProvider
            )
        {
            Logger = loggerProvider.CreateLogger("ContractSaveConsumer");
        }
        public Task Consume(ConsumeContext<uPromis.Microservice.ContractAPI.Models.ContractDTO> context)
        {
            Logger.LogInformation("Receive Contract save: {0} - {1}", context.Message.ID, context.Message.Code);
            return Task.CompletedTask;
        }
    }
}
