using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using upromis.Service.BusinessLogging.DTO;

namespace upromis.Microservice.BusinessLogging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessLoggingController : ControllerBase
    {
        readonly LiteDB.LiteDatabase m_db;
        readonly LiteDB.ILiteCollection<LogEntry> m_col;
        private readonly ILogger<BusinessLoggingController> _logger;

        public BusinessLoggingController(LiteDB.LiteDatabase db, ILogger<BusinessLoggingController> logger)
        {
            m_db = db;
            m_col = m_db.GetCollection<LogEntry>("LogEntries");
            _logger = logger;

            if (m_col.Count() == 0)
            {
                m_col.Insert(new LogEntry()
                {
                    Level = 1,
                    Project = 1,
                    Topic = "Test",
                    Message = "Test message"
                });
                logger.LogInformation("Added default entry");
            }

        }

        // GET api/values
        [HttpGet("{project}")]
        public ActionResult<IEnumerable<LogEntry>> Get(int project)
        {
            _logger.LogInformation("Get({0})", project);
            return Ok(m_col.Find(x => x.Project == project ));
        }

        // GET api/values/5
        [HttpGet("{project}/{id}")]
        public ActionResult<string> Get(int project, int id)
        {
            _logger.LogInformation("Get({0}/{1})", project, id);
            if (m_col.Exists( x => x.ID == id && x.Project == project))
            {
                _logger.LogInformation("Get({0}/{1}) - found", project, id);
                return Ok(m_col.Find(x => x.ID == id && x.Project == project));
            }
            else
            {
                _logger.LogInformation("Get({0}/{1}) - Not found", project, id);
                return NotFound(id);
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] LogEntry value)
        {
            _logger.LogInformation("Post({0})", value.Message);
            m_col.Insert(value);
        }

        // DELETE api/values/5
        [HttpDelete("{project}")]
        public void Delete(int project)
        {
            _logger.LogInformation("Delete()");
            m_col.DeleteMany(x => x.ID == project);
        }
    }
}
