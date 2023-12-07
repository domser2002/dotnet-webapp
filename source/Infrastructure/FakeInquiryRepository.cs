using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Api.Infrastructure
{
    public class FakeInquiryRepository : IInquireRepository
    {
        private IDictionary<int, Inquiry> inquiries;
        public FakeInquiryRepository()
        {
            this.inquiries = new Dictionary<int,Inquiry>();
        }

        public List<Inquiry> GetAll()
        {
            return this.inquiries.Values.ToList();
        }

        public void AddInquiry(Inquiry inquiry)
        {
            inquiries.Add(inquiries.Count + 1, inquiry);
        }
    }
}
