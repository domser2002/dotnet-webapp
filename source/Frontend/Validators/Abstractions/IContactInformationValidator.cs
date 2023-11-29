using Domain.Model;

namespace Frontend.Validators.Abstractions
{
    public interface IContactInformationValidator
    {
        public ValidationResults Validate(ContactInformation? contactInformation);
    }
}
