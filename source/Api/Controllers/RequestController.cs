using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Repositories;
using System.ComponentModel;
using System.IO;

namespace Api.Controllers
{
    [Route("api/requests")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository repository;
        private readonly BlobContainerClient containerClient;
        public RequestController(IRequestRepository repository)
        {
            this.repository = repository;
            BlobServiceClient blobServiceClient = new("DefaultEndpointsProtocol=https;AccountName=dotnetwebapp;" +
            "AccountKey=l8YwfKHD9jI0GRpxzKfJrhbJHpiavg5hQvN0MXhRmAB0BLOoqZY6+dG+xMApvt0w2YNXvTtxdzo++ASt0zLpNg==;EndpointSuffix=core.windows.net");
            containerClient = blobServiceClient.GetBlobContainerClient("container");
        }

        //GET api/requests
        [HttpGet]
        public ActionResult<List<Request>> Get()
        {
            var requests = repository.GetAll();
            return Ok(requests);
        }
        // GET api/requests/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Request> GetByID(int id)
        {
            var request = repository.GetById(id);
            if(request == null) { return BadRequest("Request not found."); }
            return Ok(request);
        }
        // GET api/requests/subs/{user_id}
        [HttpGet("subs/{user_id}")]
        [Authorize]
        public ActionResult<List<Request>> GetByUserID([FromRoute] string user_id)
        {
            var requests = repository.GetByOwner(user_id);
            return Ok(requests);
        }
        // GET api/requests/companies/{CompanyName}
        [HttpGet("companies/{CompanyName}")]
        [Authorize]
        public ActionResult<List<Request>> GetByCompany([FromRoute] string companyName)
        {
            var requests = repository.GetByCompany(companyName);
            return Ok(requests);
        }
        // POST api/requests
        [HttpPost]
        public ActionResult<User> Create([FromBody] Request request)
        {
            if (request == null)
            {
                return BadRequest();
            };

            repository.Add(request);
            return CreatedAtAction("GetById", new { id = request.Id }, request);
        }
        // DELETE /api/requests
        [HttpDelete]
        public ActionResult DeleteByID(int id)
        {
            repository.Delete(id);
            return Ok();
        }

        // PATCH /api/requests/{id}
        [HttpPatch("{id}")]
        [Authorize]
        public ActionResult PatchByID([FromRoute] int id, [FromBody] RequestPatchModel requestPatch)
        {
            if (requestPatch == null)
            {
                return BadRequest("Invalid request parameters");
            }
            Request? existingRequest = null;
            var requests = repository.GetAll();
            foreach (Request request in requests) if (request.Id == id) existingRequest = request;
            if (existingRequest == null)
            {
                return NotFound("Request not found");
            }
            if (requestPatch.SourceAddress != null)
            {
                existingRequest.SourceAddress = requestPatch.SourceAddress;
            }
            if (requestPatch.DestinationAddress != null)
            {
                existingRequest.DestinationAddress = requestPatch.DestinationAddress;
            }
            if (requestPatch.PickupDate != new DateTime())
            {
                existingRequest.PickupDate = requestPatch.PickupDate;
            }
            if (requestPatch.DeliveryDate != new DateTime())
            {
                existingRequest.DeliveryDate = requestPatch.DeliveryDate;
            }
            if (requestPatch.CancelDate != new DateTime())
            {
                existingRequest.CancelDate = requestPatch.CancelDate;
            }
            if(requestPatch.Status != RequestStatus.Idle)
            {
                existingRequest.Status = requestPatch.Status;
                Mailer.Mailer mailer = new();
                switch(requestPatch.Status)
                {
                    case RequestStatus.Accepted:
                        mailer.SendRequestAcceptedMail(existingRequest, $"agreement{id}.pdf", $"receipt{id}.pdf");
                        break;
                    case RequestStatus.Received:
                        mailer.SendPackagePickedUpMail(existingRequest);
                        break;
                    case RequestStatus.Delivered:
                        mailer.SendPackageDeveliredMail(existingRequest);
                        break;
                    case RequestStatus.CannotDeliver:
                        User? courier = new UserRepository().GetById(requestPatch.CourierId);
                        if (courier is not null) mailer.SendDeliveryFailedMail(existingRequest, courier, requestPatch.Message);
                        break;
                }
            }
            repository.Update(existingRequest);
            return Ok(existingRequest);
        }

        // POST api/requests/agreement/{requestId}
        [HttpPost("agreement/{id}")]
        [Authorize]
        public ActionResult<List<Request>> SendAgreement([FromRoute] string id, [FromForm] IFormFile agreement)
        {
            UploadFile(id, agreement, "agreement").Wait();
            return Ok();
        }

        // POST api/requests/receipt/{requestId}
        [HttpPost("receipt/{id}")]
        [Authorize]
        public ActionResult<List<Request>> SednReceipt([FromRoute] string id, [FromForm] IFormFile receipt)
        {
            UploadFile(id, receipt, "receipt").Wait();
            return Ok();
        }

        private async Task UploadFile(string id, IFormFile file, string name)
        {
            StreamReader streamReader = new(file.OpenReadStream());
            var blob = containerClient.GetBlobClient($"{name}{id}.pdf");
            await blob.UploadAsync(streamReader.BaseStream);
        }

    }
}
