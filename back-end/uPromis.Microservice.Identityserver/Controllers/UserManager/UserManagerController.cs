using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using uPromis.APIUtils.APIMessaging;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using uPromis.Services.Models;
using uPromis.Microservice.Identityserver.Data;
using uPromis.Microservice.Identityserver.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using IdentityServerHost.Quickstart.UI;
using static IdentityServer4.IdentityServerConstants;

namespace uPromis.Microservice.Identityserver.Controllers.UserManager
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    [SecurityHeaders]
    public class UserManagerController : ControllerBase
    {
        private readonly UserManagerRepository Repository;
        private readonly ILogger Logger;
        private readonly IBus ReportServerBus;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserBootstrapper bootstrapper;

        public UserManagerController(UserManagerRepository repo, ILoggerProvider loggerProvider,
            IBus reportServerBus, UserManager<ApplicationUser> userManager,
            UserBootstrapper userBootstrapper)
        {
            Repository = repo;
            Logger = loggerProvider.CreateLogger(nameof(UserManagerController));
            ReportServerBus = reportServerBus;
            _userManager = userManager;

            bootstrapper = userBootstrapper;
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var res = Repository.GetDTO(id);

            if (res == null)
            {
                return NotFound(new APIResult<ApplicationUserDTO, string>() { ID = id, DataSubject = null, Message = "Get failed" });
            }

            return Ok(new APIResult<ApplicationUserDTO, string>() { ID = id, DataSubject = res, Message = "Get was performed" });
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] SaveMessage<ApplicationUserDTO, string> rec)
        {
            ApplicationUserDTO res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                //Models.ApplicationUser record = new() { 
                //    Id = rec.DataSubject.ID,
                //    Email = rec.DataSubject.Email,
                //    UserName = rec.DataSubject.UserName
                //};

                //BusinessRules.ApplyBusinessRules(record, rec.DataSubject, User);

                //if (BusinessRules.HasErrors())
                //{
                //    return UnprocessableEntity(new APIResult<UserDTO, string>() { ID = "", DataSubject = null, Message = "Validation failed", AdditionalInfo = BusinessRules.Result.ToArray() });
                //}

                res = Repository.Post(rec.DataSubject);

                await bootstrapper.EnsureAdminRoleExists(_userManager);

                Uri uri = new("rabbitmq://localhost/" + uPromis.Services.Queues.MessageBusQueueNames.REPORTSERVERSAVECLIENT);
                var endPoint = await ReportServerBus.GetSendEndpoint(uri);
                await endPoint.Send<ApplicationUserDTO>(res);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new APIResult<ApplicationUserDTO, string>() { ID = "", DataSubject = null, Message = ex.Message });
            }

            // return posted values
            return Ok(new APIResult<ApplicationUserDTO, string>() { ID = res.Id, DataSubject = res, Message = "New User was created." });
        }

        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] SaveMessage<ApplicationUserDTO, string> rec)
        {
            ApplicationUserDTO res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                res = Repository.Put(rec.DataSubject);

                await bootstrapper .EnsureAdminRoleExists(_userManager);

                Uri uri = new("rabbitmq://localhost/" + uPromis.Services.Queues.MessageBusQueueNames.REPORTSERVERSAVECLIENT);
                var endPoint = await ReportServerBus.GetSendEndpoint(uri);
                await endPoint.Send<ApplicationUserDTO>(res);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new APIResult<ApplicationUserDTO, string>() { ID = rec.ID, DataSubject = null, Message = ex.Message });
            }

            // return posted values
            return Ok(new APIResult<ApplicationUserDTO, string>() { ID = res.Id, DataSubject = res, Message = "User was saved." });
        }

        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody] SaveMessage<ApplicationUserDTO, string> rec)
        {
            bool res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                if (rec.DataSubject.UserName == UserBootstrapper.DefaultAdminUser)
                {
                    return BadRequest(new APIResult<ApplicationUserDTO, string>() { ID = "", DataSubject = null, Message = "This user may not be deleted." });
                }
                res = Repository.Delete(rec.ID);

                if (res == false)
                {
                    return NotFound(new APIResult<ApplicationUserDTO, string>() { ID = rec.ID, DataSubject = null, Message = "Delete failed - record not found" });
                }

                await bootstrapper .EnsureAdminRoleExists(_userManager);

                Uri uri = new("rabbitmq://localhost/" + uPromis.Services.Queues.MessageBusQueueNames.REPORTSERVERDELETECLIENT);
                var endPoint = await ReportServerBus.GetSendEndpoint(uri);
                await endPoint.Send<ApplicationUserDTO>(rec.DataSubject);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new APIResult<ApplicationUserDTO, string>() { ID = "", DataSubject = null, Message = ex.Message });
            }

            // return 
            return Ok(new APIResult<ApplicationUserDTO, string>() { ID = rec.ID, DataSubject = null, Message = "User was deleted." });
        }

        // TODO: transform into a get with a body (this is possible)
        [HttpPost("getlist")]
        public async Task<ActionResult> GetList([FromBody] SortAndFilterInformation sentModel)
        {
            var records = await Repository.GetList(sentModel, true);

            return Ok(new LoadResult<ApplicationUserDTO>() { Data = records.Item1.ToArray(), Pages = records.Item2, Message = "" });
        }

        // TODO: this can be a normal "get", with a filter on the header " 'Content-Type': 'application/excel' or something
        [HttpPost("getforexport")]
        public async Task<IActionResult> GetForExport([FromBody] SortAndFilterInformation sentModel)
        {
            using ExcelPackage package = new();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ApplicationUser List");
            //Add the headers
            int col = 0;
            int row = 1;
            col++;
            worksheet.Cells[row, col].Value = "ID";
            col++;
            worksheet.Cells[row, col].Value = "Username";
            col++;
            worksheet.Cells[row, col].Value = "Email";
            col++;
            worksheet.Cells[row, col].Value = "Firstname";
            col++;
            worksheet.Cells[row, col].Value = "Lastname";
            worksheet.Cells[1, 1, 1, col].Style.Font.Bold = true;

            var records = (await Repository.GetList(sentModel, true)).Item1;

            foreach (var item in records)
            {
                row++;
                col = 1;
                worksheet.Cells[row, col].Value = item.Id;
                col++;
                worksheet.Cells[row, col].Value = item.UserName;
                col++;
                worksheet.Cells[row, col].Value = item.Email;
                col++;
                worksheet.Cells[row, col].Value = item.FirstName;
                col++;
                worksheet.Cells[row, col].Value = item.LastName;
            }

            System.IO.MemoryStream fs = new();
            await package.SaveAsAsync(fs);

            return File(fs.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        // TODO: make into a get with a body
        [Route("getselectvalues")]
        [HttpPost]
        public ActionResult GetSelectValues([FromBody] ListValueInfo info)
        {
            //var EnumProducer = new SelectValueFromEnumProducer();
            List<ListValue> list = new();

            switch (info.ValueType)
            {
                case "Roles.Role":
                    list.Add(new ListValue() { Value = string.Empty, Label = "" });

                    list.AddRange(UserManagerRepository.RoleValues);
                    break;
                default:
                    break;
            }
            return Ok(new ListValues() { ValueType = info.ValueType, data = list });
        }
    }
}
