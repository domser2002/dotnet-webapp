namespace Frontend.Validators.Abstractions
{
    public interface IRegistrationValidator : IValidator
    {
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Domain.Model.Address? Address {  get; set; }
        public Domain.Model.Address? DefaultSourceAddress { get; set; }
    }
}
