namespace Frontend.Validators.Abstractions
{
    public interface IInquireValidator : IValidator
    {
        public float? Length { get; set; }
        public float? Width { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public Domain.Model.Address? SourceAddress { get; set; }
        public Domain.Model.Address? DestinationAddress {  get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate {  get; set; }
    }
}
