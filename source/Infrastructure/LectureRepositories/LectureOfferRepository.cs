using Domain.Abstractions;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.LectureRepositories
{
    public class LectureOfferRepository : IOfferRepository
    {
        public void AddOffer(Offer offer)
        {
            throw new NotImplementedException();
        }
        public void Deactivate(int id)
        {
            throw new NotImplementedException();
        }

        public List<Offer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Offer GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Offer> GetByInquiry(Inquiry inquiry)
        {
            throw new NotImplementedException();
        }
    }
}
