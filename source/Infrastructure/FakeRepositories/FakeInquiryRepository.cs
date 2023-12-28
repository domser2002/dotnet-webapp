using Domain.Abstractions;
using Domain.Model;

namespace Infrastructure.FakeRepositories
{
    public class FakeInquiryRepository : IInquireRepository
    {
        private IDictionary<int, Inquiry> inquiries;
        public FakeInquiryRepository()
        {
            Address address1 = new() { Street = "Prosta", City = "Warsaw", FlatNumber = "1", PostalCode = "13-337", StreetNumber = "2" };
            Address address2 = new() { Street = "Kręta", City = "Cracow", FlatNumber = "2", PostalCode = "31-733", StreetNumber = "1" };
            DateTime date1 = new(2025, 3, 1, 7, 0, 0);
            DateTime date2 = new(2026, 3, 1, 7, 0, 0);
            Package package = new() { Weight = 1, Height = 1, Length = 1, Width = 1 };
            var inquiries = new List<Inquiry>
            {
                new() { Id = 1, SourceAddress = new Address(address1), DestinationAddress = new Address(address2), Active=true, Priority=Priority.Low, DeliveryAtWeekend=true, 
                    PickupDate = date1, DeliveryDate = date2, Package = new(package), OwnerId = 1},
                new() { Id = 2, SourceAddress = new Address(address1), DestinationAddress = new Address(address2), Active=false, Priority=Priority.High, DeliveryAtWeekend=false,
                    PickupDate = date1, DeliveryDate = date2, Package = new(package), OwnerId = 2},
                new() { Id = 3, SourceAddress = new Address(address1), DestinationAddress = new Address(address2), Active=true, Priority=Priority.High, DeliveryAtWeekend=true,
                    PickupDate = date1, DeliveryDate = date2, Package = new(package), OwnerId = 3}
            };
            this.inquiries = inquiries.ToDictionary(p => p.Id);
        }

        public List<Inquiry> GetAll()
        {
            return inquiries.Values.ToList();
        }

        public void AddInquiry(Inquiry inquiry)
        {
            inquiries.Add(inquiries.Count + 1, inquiry);
        }
    }
}
