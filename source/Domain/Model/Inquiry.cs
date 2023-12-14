namespace Domain.Model
{
    public class Inquiry: Base
    {
        public Package Package { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public Priority Priority { get; set; }
        public bool DeliveryAtWeekend { get; set; }
        public bool Active { get; set; }
    }

    public enum Priority { Low, High }
}