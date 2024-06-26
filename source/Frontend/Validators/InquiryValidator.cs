﻿using Domain.Model;
using Frontend.Validators.Abstractions;

namespace Frontend.Validators
{
    public class InquireValidator : IInquireValidator
    {
        private readonly float minDimension;
        private readonly float maxDimension;
        private readonly float minWeight;
        private readonly float maxWeight;
        private readonly int minStringLength;
        private readonly int maxStringLength;

        public InquireValidator(float minDimension, float maxDimension, float minWeight, float maxWeight, int minStringLength, int maxStringLength)
        {
            this.minDimension = minDimension;
            this.maxDimension = maxDimension;
            this.minWeight = minWeight;
            this.maxWeight = maxWeight;
            this.minStringLength = minStringLength;
            this.maxStringLength = maxStringLength;
        }

        public ValidationResults Validate(float? length, float? width, float? height, float? weight, Address? sourceAddress, Address? destinationAddress, 
            DateTime? pickupDate, DateTime? deliveryDate)
        {
            ValidationResults temp = GenericValidators.Date(pickupDate, "Pickup");
            if (!temp.Success) return temp;
            temp = GenericValidators.Date(deliveryDate, "Delivery");
            if (!temp.Success) return temp;
            if (DateTime.Compare((DateTime)pickupDate!, (DateTime)deliveryDate!) >= 0) return new ValidationResults("Delivery date must be set after pickup date.");
            temp = GenericValidators.Dimension(length, minDimension, maxDimension, "length");
            if (!temp.Success) return temp;
            temp = GenericValidators.Dimension(width, minDimension, maxDimension, "width");
            if (!temp.Success) return temp;
            temp = GenericValidators.Dimension(height, minDimension, maxDimension, "height");
            if (!temp.Success) return temp;
            temp = GenericValidators.Dimension(weight, minWeight, maxWeight, "weight");
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(sourceAddress, minStringLength, maxStringLength);
            if (!temp.Success) return temp;
            temp = GenericValidators.Address(destinationAddress, minStringLength, maxStringLength);
            return temp;
        }

        public ValidationResults Validate(Package package, Address? sourceAddress, Address? destinationAddress, DateTime? pickupDate, DateTime? deliveryDate)
        {
            return Validate(package.Length, package.Width, package.Height, package.Weight, sourceAddress, destinationAddress, pickupDate, deliveryDate);
        }
        public ValidationResults Validate(Inquiry inquiry)
        {
            return Validate(inquiry.Package!,inquiry.SourceAddress,inquiry.DestinationAddress,inquiry.PickupDate, inquiry.DeliveryDate);
        }
    }
}
