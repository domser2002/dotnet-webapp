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
        // POST api/inquiries
        [HttpPost]
        public ActionResult<Inquiry> Create([FromBody] Inquiry inquiry)
        {
            if (inquiry == null)
            {
                return BadRequest();
            }

            ValidationResults validationResults = validator.Validate(inquiry);
            if (validationResults == null)
                return BadRequest("could not validate");
            if (!validationResults.Success)
                return BadRequest(validationResults.Message);

            repository.AddInquiry(inquiry);
            return CreatedAtAction("GetById", new { id = inquiry.Id }, inquiry);
        }
    }
}
