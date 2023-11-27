using Domain.Model;
using Frontend.Validators.Abstractions;

namespace Frontend.Validators
{
    public class ContactInformationValidator : IContactInformationValidator
    {
        public ContactInformation? ContactInformation { get; set; }

        public ContactInformationValidator(ContactInformation? contactInformation) { ContactInformation = contactInformation; }

        public ValidationResults Validate()
        {
            if (ContactInformation is null) return new ValidationResults("Contact information must be specified");
            if (ContactInformation.PersonalData.Length < 1) return new ValidationResults("Personal data must be specified");
            ValidationResults temp;
            temp = GenericValidators.Email(ContactInformation.Email);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(ContactInformation.Address);
            return temp;
        }
    }
}
