using Domain.Model;

namespace Frontend.Validators.Abstractions
{
    public interface IRegistrationValidator
    {
        public ValidationResults Validate(string? firstName, string? lastName, string? email, Address? address, Address? defaultSourceAddress);
        public ValidationResults Validate(User User);
    }
}
