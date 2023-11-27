using Domain.Model;

namespace Frontend.Validators.Abstractions
{
    public interface IContactInformationValidator : IValidator
    {
        public ContactInformation? ContactInformation { get; set; }
    }
}
