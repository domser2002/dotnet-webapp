using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;
using Frontend.Validators.Abstractions;
using Frontend.Validators;

namespace Api.Controllers
{
    [Route("api/inquires")]
    public class InquireController : ControllerBase
    {
        private readonly IInquireRepository repository;
        private readonly IInquireValidator validator;
        public InquireController(IInquireRepository repository, IInquireValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

    }
}
