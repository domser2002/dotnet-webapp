using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.Abstractions;
using Frontend.Validators.Abstractions;
using Frontend.Validators;

namespace Api.Controllers
{
    [Route("api/ContactInformation")]
    public class ContactInformationController : ControllerBase
    {
        private readonly IContactInformationRepository repository;
        private readonly IContactInformationValidator validator;
        public ContactInformationController(IContactInformationRepository repository, IContactInformationValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }
        // GET /api/contacts
        [HttpGet]
        public ActionResult<List<ContactInformation>> Get()
        {
            var contacts = repository.GetAll();

            return Ok(contacts);
        }
        // POST api/contacts
        [HttpPost]
        public ActionResult<ContactInformation> Create([FromBody] ContactInformation info)
        {
            if (info == null)
            {
                return BadRequest();
            }

            ValidationResults validationResults = validator.Validate(info);
            if (validationResults == null)
                return BadRequest("could not validate");
            if (!validationResults.Success)
                return BadRequest(validationResults.Message);

            repository.AddContactInformation(info);
            // return CreatedAtAction("GetById", new { id = info.Id }, info);
            return Ok();
        }
    }
}
