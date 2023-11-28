using Domain.Model;
using Frontend.Validators.Abstractions;

namespace Frontend.Validators
{
    public class ContactInformationValidator : IContactInformationValidator
    {
        public readonly int minStringLength;
        public readonly int maxStringLength;

        public ContactInformationValidator(int minStringLength, int maxStringLength)
        {
            this.minStringLength = minStringLength;
            this.maxStringLength = maxStringLength;
        }

        public ValidationResults Validate(ContactInformation? contactInformation)
        {
            if (contactInformation is null) return new ValidationResults("Contact information must be specified");
            if (contactInformation.PersonalData.Length < minStringLength) 
                return new ValidationResults($"Personal data must consist of at least {minStringLength} characters.");
            if (contactInformation.PersonalData.Length > maxStringLength) 
                return new ValidationResults($"Personal data cannot consist of more than {maxStringLength} characters.");
            ValidationResults temp;
            temp = GenericValidators.Email(contactInformation.Email);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(contactInformation.Address);
            return temp;
        }
    }
}
