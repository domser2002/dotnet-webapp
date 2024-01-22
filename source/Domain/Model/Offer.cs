namespace Domain.Model
{
    public class Offer : Base
    {
        public Offer() { }
        public Offer(LectureOffer lectureOffer,Inquiry inquiry) 
        {
            CompanyName = "LectureCompany";
            Price = (decimal)lectureOffer.totalPrice;
            Active = true;
            DeliveryTime = ((TimeSpan)(inquiry.DeliveryDate - inquiry.PickupDate)).Days;
            Begins = (DateTime)inquiry.PickupDate;
            Ends = (DateTime)inquiry.DeliveryDate;
            MinDimension = 0;
            MaxDimension = float.MaxValue;
            MinWeight = 0;
            MaxWeight = float.MaxValue;
        }
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
    public class LectureOffer
    {
        public class PriceBreakDownItem
        {
            public double? amount { get; set; }
            public string? currency { get; set; }
            public string? description { get; set; }
        }
        public Guid inquiryId { get; set; }
        public double totalPrice { get; set; }
        public string? currency { get; set; }
        public DateTime? expiringAt { get; set; }
        public List<PriceBreakDownItem> priceBreakDown { get; set; }
    }
}