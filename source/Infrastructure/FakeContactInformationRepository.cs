using Domain.Abstractions;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class FakeContactInformationRepository : IContactInformationRepository
    {
        private IDictionary<int, ContactInformation> contacts;
        public FakeContactInformationRepository() 
        { 
            contacts=new Dictionary<int, ContactInformation>();
        }
        public void AddContactInformation(ContactInformation contactInformation)
        {
            contacts.Add(contacts.Count+1, contactInformation);
        }
        public List<ContactInformation> GetAll()
        {
            return contacts.Values.ToList();
        }
    }
}
