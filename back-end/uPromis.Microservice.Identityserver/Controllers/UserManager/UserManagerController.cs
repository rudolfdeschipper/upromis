﻿using Microsoft.AspNetCore.Mvc;
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
using uPromis.Microservice.IdentityServer.Models;
using uPromis.Microservice.Identityserver.Models;

namespace uPromis.Microservice.Identityserver.Controllers.UserManager
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly UserManagerRepository Repository;
        private readonly ILogger Logger;
        private readonly IBus ReportServerBus;

        public UserManagerController(UserManagerRepository repo, ILoggerProvider loggerProvider,
            IBus reportServerBus)
        {
            Repository = repo;
            Logger = loggerProvider.CreateLogger(nameof(UserManagerController));
            ReportServerBus = reportServerBus;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var res = Repository.GetDTO(id);

            if (res == null)
            {
                return NotFound(new APIResult<UserDTO, string>() { ID = id, DataSubject = null, Message = "Get failed" });
            }

            return Ok(new APIResult<UserDTO, string>() { ID = id, DataSubject = res, Message = "Get was performed" });
        }

        [HttpPost()]
        public async Task<ActionResult> Post([FromBody] SaveMessage<UserDTO, string> rec)
        {
            UserDTO res;

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

                Uri uri = new("rabbitmq://localhost/" + uPromis.Services.Queues.MessageBusQueueNames.REPORTSERVERSAVECLIENT);
                var endPoint = await ReportServerBus.GetSendEndpoint(uri);
                await endPoint.Send<UserDTO>(res);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new APIResult<UserDTO, string>() { ID = "", DataSubject = null, Message = ex.Message });
            }

            // return posted values
            return Ok(new APIResult<UserDTO, string>() { ID = res.ID, DataSubject = res, Message = "New Client was created." });
        }

        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] SaveMessage<UserDTO, string> rec)
        {
            UserDTO res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {
                res = Repository.Put(rec.DataSubject);

                Uri uri = new("rabbitmq://localhost/" + uPromis.Services.Queues.MessageBusQueueNames.REPORTSERVERSAVECLIENT);
                var endPoint = await ReportServerBus.GetSendEndpoint(uri);
                await endPoint.Send<UserDTO>(res);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new APIResult<UserDTO, string>() { ID = rec.ID, DataSubject = null, Message = ex.Message });
            }

            // return posted values
            return Ok(new APIResult<UserDTO, string>() { ID = res.ID, DataSubject = res, Message = "Client was saved." });
        }

        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody] SaveMessage<UserDTO, string> rec)
        {
            bool res;

            Logger.Log(LogLevel.Information, rec.Action + "/" + rec.SubAction);

            try
            {

                res = Repository.Delete(rec.ID);

                if (res == false)
                {
                    return NotFound(new APIResult<UserDTO, string>() { ID = rec.ID, DataSubject = null, Message = "Delete failed - record not found" });
                }

                Uri uri = new("rabbitmq://localhost/" + uPromis.Services.Queues.MessageBusQueueNames.REPORTSERVERDELETECLIENT);
                var endPoint = await ReportServerBus.GetSendEndpoint(uri);
                await endPoint.Send<UserDTO>(rec.DataSubject);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new APIResult<UserDTO, string>() { ID = "", DataSubject = null, Message = ex.Message });
            }

            // return 
            return Ok(new APIResult<UserDTO, string>() { ID = rec.ID, DataSubject = null, Message = "Client was deleted." });
        }

        // TODO: transform into a get with a body (this is possible)
        [HttpPost("getlist")]
        public async Task<ActionResult> GetList([FromBody] SortAndFilterInformation sentModel)
        {
            var records = await Repository.GetList(sentModel, true);
            return Ok(new LoadResult<UserDTO>() { Data = records.Item1.ToArray(), Pages = records.Item2, Message = "" });
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
                worksheet.Cells[row, col].Value = item.ID;
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
        public async Task<ActionResult> GetSelectValues([FromBody] ListValueInfo info)
        {
            //var EnumProducer = new SelectValueFromEnumProducer();
            List<ListValue> list = new();

            switch (info.ValueType)
            {
                default:
                    break;
            }
            return Ok(new ListValues() { ValueType = info.ValueType, data = list });
        }
    }
}
