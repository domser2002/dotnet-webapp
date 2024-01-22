using Domain.Abstractions;
using Domain.Model;

namespace Infrastructure.FakeRepositories
{
    public class FakeContactInformationRepository : IContactInformationRepository
    {
        private IDictionary<int, ContactInformation> contacts;
        public FakeContactInformationRepository()
        {
            Address address = new() { Street = "Prosta", City = "Warsaw", FlatNumber = "1", PostalCode = "13-337", StreetNumber = "2" };
            var ci = new List<ContactInformation>
            {
                new() { Address=address, Email="mail1@gmail.com", PersonalData="Jan Kowalski", Id=1 },
                new() { Address=address, Email="mail2@gmail.com", PersonalData="Jakub Nowak", Id=2 },
                new() { Address=address, Email="mail3@gmail.com", PersonalData="Tomasz Kowalczyk", Id=3 }
            };
            contacts = ci.ToDictionary(p => p.Id);
        }
        public int AddContactInformation(ContactInformation contactInformation)
        {
            int index = contacts.Count + 1;
            contacts.Add(index, contactInformation);
            return index;
        }
        public List<ContactInformation> GetAll()
        {
            return contacts.Values.ToList();
        }
    }
}
