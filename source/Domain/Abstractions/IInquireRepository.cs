using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IInquireRepository
    {
        List<Offer> GetAll();
        void AddOffer(Offer offer);
    }
}
