using Domain.Model;
using Frontend.Validators.Abstractions;

namespace Frontend.Validators
{
    public class RegistrationValidator : IRegistrationValidator
    {
        private readonly int minStringLength;
        private readonly int maxStringLength;

        public RegistrationValidator(int minStringLength, int maxStringLength)
        {
            this.minStringLength = minStringLength;
            this.maxStringLength = maxStringLength;
        }

        public ValidationResults Validate(string? firstName, string? lastName, string? email, Address? address, Address? defaultSourceAddress, string? companyName)
        {
            ValidationResults temp;
            temp = GenericValidators.Name(firstName, minStringLength, maxStringLength, "First name");
            if (!temp.Success) return temp;
            temp = GenericValidators.Name(lastName, minStringLength, maxStringLength, "Last name");
            if (!temp.Success) return temp;
            temp = GenericValidators.Name(companyName, minStringLength, maxStringLength, "Company name");
            if (!temp.Success) return temp;
            temp = GenericValidators.Email(email);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(address, minStringLength, maxStringLength);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(defaultSourceAddress, minStringLength, maxStringLength);
            return temp;
        }

        public ValidationResults Validate(User user)
        {
            return Validate(user.FirstName, user.LastName, user.Email, user.Address, user.DefaultSourceAddress, user.CompanyName);
        }
    }
}
