namespace Domain.Model
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
    }
}
