using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Frontend.Validators;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [Route("api/requests")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository repository;
        public RequestController(IRequestRepository repository)
        {
            this.repository = repository;
        }

        //GET api/requests
        [HttpGet]
        public ActionResult<List<Request>> Get()
        {
            var requests = repository.GetAll();
            return Ok(requests);
        }
        // GET api/requests/{user_id}
        [HttpGet("{user_id}")]
        public ActionResult<List<Request>> GetByUserID(string userId)
        {
            var requests = repository.GetByOwner(userId);
            return Ok(requests);
        }
        // GET api/requests/{CompanyName}
        [HttpGet("{CompanyName}")]
        public ActionResult<List<Request>> GetByCompany(string companyName)
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
        public ActionResult PatchByID(int id, [FromBody] RequestPatchModel requestPatch)
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
            }
            repository.Update(existingRequest);
            return Ok(existingRequest);
        }
    }
}
