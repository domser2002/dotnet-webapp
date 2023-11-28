using Domain.Model;

namespace Frontend.Validators.Abstractions
{
    public interface IInquireValidator
    {
        public ValidationResults Validate(float? length, float? width, float? height, float? weight, Address? sourceAddress, 
            Address? destinationAddress, DateTime? pickupDate, DateTime? deliveryDate);
        public ValidationResults Validate(Package package, Address? sourceAddress, Address? destinationAddress, DateTime? pickupDate, DateTime? deliveryDate);
        public ValidationResults Validate(Inquiry inquiry);
    }
}
