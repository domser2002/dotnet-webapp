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
            ValidationResults temp;
            temp = GenericValidators.String(contactInformation!.PersonalData, minStringLength, maxStringLength, "Personal data");
            if (!temp.Success) return temp;
            temp = GenericValidators.Email(contactInformation.Email);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(contactInformation.Address, minStringLength, maxStringLength);
            return temp;
        }
    }
}
