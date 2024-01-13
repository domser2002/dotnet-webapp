using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

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
    }
}
