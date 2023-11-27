using Domain.Model;
using Frontend.Validators.Abstractions;

namespace Frontend.Validators
{
    public class InquireValidator : IInquireValidator
    {
        public float? Length { get; set; }
        public float? Width { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public Address? SourceAddress { get; set; }
        public Address? DestinationAddress { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public InquireValidator(Package package, Address? sourceAddress, Address? destinationAddress, DateTime? pickupDate, DateTime? deliveryDate)
        {
            Length = package.Length;
            Width = package.Width;
            Height = package.Height;
            Weight = package.Weight;
            SourceAddress = sourceAddress;
            DestinationAddress = destinationAddress;
            PickupDate = pickupDate;
            DeliveryDate = deliveryDate;
        }

        public InquireValidator(float? length, float? width, float? height, float? weight, 
            Address? sourceAddress, Address? destinationAddress, DateTime? pickupDate, DateTime? deliveryDate)
        {
            Length = length;
            Width = width;
            Height = height;
            Weight = weight;
            SourceAddress = sourceAddress;
            DestinationAddress = destinationAddress;
            PickupDate = pickupDate;
            DeliveryDate = deliveryDate;
        }

        public ValidationResults Validate()
        {
            float minDimension = 0;
            float maxDimension = 200;
            ValidationResults temp = GenericValidators.Date(PickupDate, "Pickup");
            if (!temp.Success) return temp;
            temp = GenericValidators.Date(DeliveryDate, "Delivery");
            if (!temp.Success) return temp;
            if (DateTime.Compare((DateTime)PickupDate!, (DateTime)DeliveryDate!) >= 0) return new ValidationResults("Pickup cannot take place after delivery.");
            temp = GenericValidators.Dimension(Length, minDimension, maxDimension, "Length");
            if (!temp.Success) return temp;
            temp = GenericValidators.Dimension(Width, minDimension, maxDimension, "Width");
            if (!temp.Success) return temp;
            temp = GenericValidators.Dimension(Height, minDimension, maxDimension, "Height");
            if (!temp.Success) return temp;
            temp = GenericValidators.Dimension(Weight, 0, 1000, "Weight");
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(SourceAddress);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(DestinationAddress);
            return temp;
        }
    }
}
