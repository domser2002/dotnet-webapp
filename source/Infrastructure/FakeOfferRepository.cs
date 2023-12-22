using Domain.Abstractions;
using Domain.Model;

namespace Api.Infrastructure
{
    public class FakeOfferRepository : IOfferRepository
    {
        private IDictionary<int, Offer> offers;
        public FakeOfferRepository() 
        {
            var users = new List<Offer>{ new Offer {Id=1, CompanyName="Company A", Price=100, Active=true } ,
                            new Offer {Id=2, CompanyName="Company B", Price=50, Active=true },
                            new Offer {Id=3, CompanyName="Company C", Price=150, Active=true } };
            this.offers = users.ToDictionary(p => p.Id);
        }

        public List<Offer> GetAll()
        {
            return this.offers.Values.Where(obiekt => obiekt.Active == true).ToList(); 
        }

        public void AddOffer(Offer offer)
        {
            offers.Add(offers.Count+1,offer);
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
