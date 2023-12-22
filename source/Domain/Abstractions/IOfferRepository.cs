using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Abstractions
{
    public interface IOfferRepository
    {
        List<Offer> GetAll();
        void AddOffer(Offer offer);
        Offer GetByID(int id);
        void Deactivate(int id);
    }
}
