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

    public enum Priority { Low, High }
}