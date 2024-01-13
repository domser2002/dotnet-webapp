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
        public ActionResult<List<Request>> GetByUserID(string userId)
        {
            var requests = repository.GetByOwner(userId);
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
    }
}
