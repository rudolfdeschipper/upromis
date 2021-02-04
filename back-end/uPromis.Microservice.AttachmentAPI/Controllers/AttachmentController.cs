using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uPromis.Microservice.AttachmentAPI.Models;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using LiteX.Storage.Core;
using System.Linq.Dynamic.Core;
using System.Linq;
using uPromis.APIUtils.APIMessaging;

namespace uPromis.Microservice.AttachmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttachmentController : ControllerBase
    {
        private readonly AttachmentContext _context;
        private readonly long _fileSizeLimit;
        // private readonly ILogger<AttachmentController> _logger;

        private readonly ILiteXBlobServiceAsync _provider;

        public AttachmentController(AttachmentContext context, IConfiguration config, ILiteXBlobServiceAsync provider)
        {
            _context = context;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _provider = provider;
        }

        // GET: api/Attachment/
        // Return the list of all the attachments related to a link
        [HttpGet("{id}")]
        public async Task<ActionResult<LoadResult<AttachmentDTO>>> GetAttachments(Guid id, int page, int pageSize)
        {
            var records = await _context.attachments.Where(a => a.ParentItem == id)
                        .Select(a => new AttachmentDTO()
                        {
                            Id = a.Id,
                            FileName = a.FileName,
                            Size = a.Size,
                            UploadedBy = a.UploadedBy,
                            UploadedOn = a.UploadedOn
                        })
                        .ToListAsync();
            double recordsCount = records.Count();

            return Ok(new LoadResult<AttachmentDTO>()
            {
                Data = records.Skip(page * pageSize).Take(pageSize).ToArray(),
                Pages = Math.Ceiling(recordsCount / pageSize),
                Message = ""
            });
        }



        // GET: api/Attachment/5
        [HttpGet("Download/{id}")]
        public async Task<IActionResult> DownloadAttachment(Guid id)
        {
            var attachment = await _context.attachments.FindAsync(id);

            if (attachment == null)
            {
                return NotFound();
            }

            Stream stream = null;
            try
            {
                //stream = new FileStream(attachment.FilePath, FileMode.Open);
                stream = await _provider.GetBlobAsync(attachment.BlobContainer, attachment.BlobName);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 10).");
                // Log error
                return BadRequest(ModelState);
            }
            if (stream == null)
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 10).");
                // Log error
                return BadRequest(ModelState);
            }

            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, attachment.FileName);

        }

        // POST: api/Attachment
        [HttpPost]
        public async Task<IActionResult> PostAttachment([FromForm] FileFormData model)
        {
            //Validate the Model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Boolean isUploaded = false;
            //Check if the file is not empty
            if (model.File.Length > 0)
            {
                Stream stream = model.File.OpenReadStream();
                string contentType = model.File.ContentType;
                BlobProperties properties = new BlobProperties
                {
                    ContentType = contentType,
                    Security = BlobSecurity.Public
                };
                string blobContainer = model.ParentItem.ToString();
                string blobName = Path.GetRandomFileName();
                isUploaded = await _provider.UploadBlobAsync(blobContainer, blobName, stream, properties);

                //store the data
                var attachment = new Attachment()
                {
                    UploadedBy = model.UploadedBy,
                    ParentItem = model.ParentItem,
                    FileName = model.File.FileName,
                    Size = model.File.Length,
                    BlobContainer = blobContainer,
                    BlobName = blobName,
                    UploadedOn = DateTime.UtcNow
                };
                _context.attachments.Add(attachment);


                if (isUploaded)
                {
                    await _context.SaveChangesAsync();
                    return Created(nameof(AttachmentController), new AttachmentDTO()
                    {
                        Id = attachment.Id,
                        FileName = attachment.FileName,
                        Size = attachment.Size,
                        UploadedBy = attachment.UploadedBy,
                        UploadedOn = attachment.UploadedOn
                    });
                }
            }

            ModelState.AddModelError("File", $"The request couldn't be processed (Error 20).");
            // Log error
            return BadRequest(ModelState);


        }

        // DELETE: api/Attachment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AttachmentDTO>> DeleteAttachment(Guid id)
        {
            var attachment = await _context.attachments.FindAsync(id);

            if (attachment == null)
            {
                return NotFound();
            }

            //System.IO.File.Delete(attachment.FilePath);
            var isDeleted = await _provider.DeleteBlobAsync(attachment.BlobContainer, attachment.BlobName);
            if (isDeleted)
            {
                _context.attachments.Remove(attachment);
                await _context.SaveChangesAsync();
                return new AttachmentDTO()
                {
                    Id = attachment.Id,
                    FileName = attachment.FileName,
                    Size = attachment.Size,
                    UploadedBy = attachment.UploadedBy,
                    UploadedOn = attachment.UploadedOn
                };
            }
            else
            {
                ModelState.AddModelError("File", $"The request couldn't be processed (Error 20).");
                // Log error
                return BadRequest(ModelState);
            }
        }
    }
}
