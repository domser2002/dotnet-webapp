using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Request:Base
    {
        public Package Package { get; set; }
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
    }
}
