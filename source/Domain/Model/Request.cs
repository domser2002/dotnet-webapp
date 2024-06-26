﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public enum RequestStatus
    {
        Idle,
        Accepted,
        Received,
        Delivered,
        CannotDeliver,
        Cancelled
    }
    public class Request:Base
    {
        public Package Package { get; set; }
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        public RequestStatus Status { get; set; }
        public ContactInformation Owner { get; set; }
        public Request(Offer offer,Inquiry inquiry)
        {
            Package = inquiry.Package;
            SourceAddress = inquiry.SourceAddress;
            DestinationAddress = inquiry.DestinationAddress;
            PickupDate = (DateTime)inquiry.PickupDate;
            DeliveryDate = ((DateTime)(inquiry.PickupDate)).AddDays(offer.DeliveryTime);
            CompanyName = offer.CompanyName;
            Price = offer.Price;
            Status = RequestStatus.Idle;
        }
        public Request() { }
    }
    public class RequestPatchModel
    {
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public DateTime PickupDate { get;set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CancelDate { get; set; }    
        public RequestStatus Status { get; set; }
        public string Message { get; set; }
        public string CourierId { get; set; }
    }
}
