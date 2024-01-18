﻿namespace Domain.Model
{
    public class Address
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string FlatNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Address() { }
        public Address(Address address)
        {
            Street = address.Street;
            StreetNumber = address.StreetNumber;
            FlatNumber = address.FlatNumber;
            PostalCode = address.PostalCode;
            City = address.City;
        }
        public object this[string fieldname]
        {
            set
            {
                switch (fieldname)
                {
                    case "Street":
                        this.Street = (string)value;
                        break;
                    case "StreetNumber":
                        this.StreetNumber = (string)value;
                        break;
                    case "FlatNumber":
                        this.FlatNumber = (string)value;
                        break;
                    case "PostalCode":
                        this.PostalCode = (string)value;
                        break;
                    case "City":
                        this.City = (string)value;
                        break;
                    default:
                        return;
                }
            }
        }
    }
}