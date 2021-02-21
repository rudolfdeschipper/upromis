using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace uPromis.Services
{
    public interface IBusinessLogging
    {
        [Get("/api/BusinessLogging/{project}")]
        Task<IEnumerable<BusinessLogging.DTO.LogEntry>> Get(int project);

        [Post("/api/BusinessLogging")]
        Task Post(BusinessLogging.DTO.LogEntry log);
    }

    public class BusinessLoggingService : IBusinessLoggingService
    {
        public BusinessLoggingService()
        {
            Service = RestService.For<IBusinessLogging>("https://localhost:5051");
        }

        public IBusinessLogging Service { get; set; }
    }
}
