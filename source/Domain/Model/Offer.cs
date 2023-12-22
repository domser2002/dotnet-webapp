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
    }
}