using Domain.Abstractions;
using Domain.Model;

namespace Infrastructure.FakeRepositories
{
    public class FakeOfferRepository : IOfferRepository
    {
        private IDictionary<int, Offer> offers;
        public FakeOfferRepository()
        {
            Address address1 = new() { Street = "Prosta", City = "Warsaw", FlatNumber = "1", PostalCode = "13-337", StreetNumber = "2" };
            Address address2 = new() { Street = "Kręta", City = "Cracow", FlatNumber = "2", PostalCode = "31-733", StreetNumber = "1" };
            DateTime date1 = new(2025, 3, 1, 7, 0, 0);
            DateTime date2 = new(2026, 3, 1, 7, 0, 0);
            var offers = new List<Offer>
            {
                new() {Id=1, CompanyName="Company A", Price=100, Active=true, Begins=date1, Ends=date2, DeliveryTime=1, MaxDimension=100, MaxWeight=100, MinDimension=1, MinWeight=1 },
                new() {Id=2, CompanyName="Company B", Price=50, Active=true, Begins=date1, Ends=date2, DeliveryTime=2, MaxDimension=50, MaxWeight=80, MinDimension=10, MinWeight=10 },
                new() {Id=3, CompanyName="Company C", Price=150, Active=true, Begins=date1, Ends=date2, DeliveryTime=3, MaxDimension=10, MaxWeight=10, MinDimension=5, MinWeight=5 }
            };
            this.offers = offers.ToDictionary(p => p.Id);
        }

        public List<Offer> GetAll()
        {
            return offers.Values.Where(obiekt => obiekt.Active == true).ToList();
        }

        public void AddOffer(Offer offer)
        {
            offers.Add(offers.Count + 1, offer);
        }

        public Offer GetByID(int id)
        {
            return offers[id];
        }

        public void Deactivate(int id)
        {
            offers[id].Active = false;
            return;
        }
    }
}
