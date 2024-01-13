using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Domain.Model;

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
    }
}
