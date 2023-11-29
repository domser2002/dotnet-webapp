using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Api.Infrastructure
{
    public class FakeOfferRepository : IOfferRepository
    {
        private IDictionary<int, Offer> offers;
        public FakeOfferRepository() 
        {
            var users = new List<Offer>{ new Offer {Id=1, CompanyName="Company A", Price=100 } ,
                            new Offer {Id=2, CompanyName="Company B", Price=50 },
                            new Offer {Id=3, CompanyName="Company C", Price=150 } };
            this.offers = users.ToDictionary(p => p.Id);
        }

        public List<Offer> GetAll()
        {
            return this.offers.Values.ToList();
        }

        public void AddOffer(Offer offer)
        {
            offers.Add(offers.Count+1,offer);
        }
    }
}
