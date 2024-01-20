namespace Domain.Model
{
    public class Offer : Base
    {
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public int DeliveryTime { get; set; }
        public DateTime Begins { get; set; }
        public DateTime Ends { get; set; }
        public float MinDimension { get; set; }
        public float MaxDimension { get; set; }
        public float MinWeight { get; set; }
        public float MaxWeight { get; set; }
        public bool MatchesInquiry(Inquiry inquiry)
        {
            return (inquiry.Package is not null) && (inquiry.Package.Weight < MaxWeight) &&
                (inquiry.Package.Weight > MinWeight) && (inquiry.Package.Height < MaxDimension) &&
                (inquiry.Package.Height > MinDimension) && (inquiry.Package.Width < MaxDimension) &&
                (inquiry.Package.Width > MinDimension) && (inquiry.Package.Length < MaxDimension) &&
                (inquiry.Package.Length > MinDimension) && (inquiry.DeliveryDate < Ends) &&
                (inquiry.PickupDate > Begins) &&
                (((TimeSpan)(inquiry.DeliveryDate - inquiry.PickupDate)).Days<DeliveryTime);
        }
    }
}