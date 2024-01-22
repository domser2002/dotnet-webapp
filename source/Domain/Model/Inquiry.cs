namespace Domain.Model
{
    public class Inquiry : Base
    {
        public Package? Package { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Address? SourceAddress { get; set; }
        public Address? DestinationAddress { get; set; }
        public Priority Priority { get; set; }
        public bool DeliveryAtWeekend { get; set; }
        public bool Active { get; set; }
        public int OwnerId { get; set; }
        public object this[string fieldname]
        {
            set
            {
                switch (fieldname)
                {
                    case "PackageLength":
                        this.Package["Length"] = value;
                        break;
                    case "PackageWidth":
                        this.Package["Width"] = value;
                        break;
                    case "PackageHeight":
                        this.Package["Height"] = value;
                        break;
                    case "PackageWeight":
                        this.Package["Weight"] = value;
                        break;
                    case "PickupDate":
                        this.PickupDate = value == null ? null : (DateTime)value;
                        break;
                    case "DeliveryDate":
                        this.DeliveryDate = value == null ? null : (DateTime)value;
                        break;
                    case "SourceAddress":
                        this.SourceAddress = value == null ? null : (Address)value;
                        break;
                    case "DestinationAddress":
                        this.DestinationAddress = value == null ? null : (Address)value;
                        break;
                    case "Priority":
                        this.Priority = (Priority)value;
                        break;
                    case "DeliveryAtWeekend":
                        this.DeliveryAtWeekend = (bool)value;
                        break;
                    case "Active":
                        this.Active = (bool)value;
                        break;
                }
            }
        }
    }
    public class LectureInquiryAdapter
    {
        private Inquiry adaptee;
        public LectureInquiryAdapter(Inquiry adaptee)
        {
            this.adaptee = adaptee;
        }
        public struct Dims
        {
            public float Length;
            public float Height;
            public float Width;
            public string dimensionsUnit;
        }
        public Dims Dimensions
        {
            get
            {
                return new Dims
                {
                    Length = adaptee.Package?.Length ?? 0,
                    Width = adaptee.Package?.Width ?? 0,
                    Height = adaptee.Package?.Height ?? 0,
                    dimensionsUnit = "Meters"
                };
            }
        }
        public string Currency => "PLN";

        public float Weight => adaptee.Package?.Weight ?? 0;

        public string WeightUnit => "Kilograms";

        public LectureAddressAdapter Source => new(adaptee.SourceAddress);

        public LectureAddressAdapter Destination => new(adaptee.DestinationAddress);

        public string PickupDate => adaptee.PickupDate?.ToString("yyyy-MM-ddTHH:mm:ss");

        public string DeliveryDay => adaptee.DeliveryDate?.ToString("yyyy-MM-ddTHH:mm:ss");

        public bool DeliveryInWeekend => adaptee.DeliveryAtWeekend;

        public string Priority => adaptee.Priority.ToString();

        public bool VipPackage => false; // Adapt as needed

        public bool IsCompany => false; // Adapt as needed
    }
    public enum Priority { Low, High }
}