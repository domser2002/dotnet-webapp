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

    }
}
