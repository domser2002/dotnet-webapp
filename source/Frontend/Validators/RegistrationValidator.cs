using Domain.Model;
using Frontend.Validators.Abstractions;

namespace Frontend.Validators
{
    public class RegistrationValidator : IRegistrationValidator
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Address? Address { get; set; }
        public Address? DefaultSourceAddress { get; set; }

        public RegistrationValidator(string? firstName, string? lastName, string? email, Address? address, Address? defaultSourceAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            DefaultSourceAddress = defaultSourceAddress;
        }

        public RegistrationValidator(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Address = user.Address;
            DefaultSourceAddress = user.DefaultSourceAddress;
        }

        public ValidationResults Validate()
        {
            int min = 2;
            int max = 100;
            ValidationResults temp;
            temp = GenericValidators.Name(FirstName, min, max, "First name");
            if (!temp.Success) return temp;
            temp = GenericValidators.Name(LastName, min, max, "Last name");
            if (!temp.Success) return temp;
            temp = GenericValidators.Email(Email);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(Address);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(DefaultSourceAddress);
            return temp;
        }
    }
}
