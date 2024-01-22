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
    public class LectureAddressAdapter
    {
        private Address adaptee;
        public LectureAddressAdapter(Address adaptee)
        {
            this.adaptee = adaptee;
        }
        public string HouseNumber => adaptee.StreetNumber;
        public string ApartmentNumber => adaptee.FlatNumber;
        public string Street => adaptee.Street;
        public string City => adaptee.City;
        public string ZipCode => adaptee.PostalCode;
        public string Country => "Poland"; // we do not have country xd
    }
}