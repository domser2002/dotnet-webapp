using Domain.Model;
using Domain.Abstractions;

namespace Api.Infrastructure
{
    public class FakeUserRepository : IUserRepository
    {
        private IDictionary<int, User> users;

        public FakeUserRepository()
        {
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "John", LastName = "Smith",
                    Address=new Address {Street="Prosta", City="Warsaw", FlatNumber="1", PostalCode="13-337", StreetNumber="2" }, CompanyName="ABC", Email="a@gmail.com", Inquiries=new List<int>()},
                new User { Id = 2, FirstName = "Bob", LastName = "Smith",
                    Address=new Address {Street="Prosta", City="Warsaw", FlatNumber="1", PostalCode="13-337", StreetNumber="2" }, CompanyName="ABC", Email="a@gmail.com", Inquiries=new List<int>()},
                new User { Id = 3, FirstName = "Ann", LastName = "Smith",
                    Address=new Address {Street="Prosta", City="Warsaw", FlatNumber="1", PostalCode="13-337", StreetNumber="2" }, CompanyName="ABC", Email="a@gmail.com", Inquiries=new List<int>()},
            };

            this.users = users.ToDictionary(p => p.Id);
        }



        public List<User> GetAll()
        {
            return users.Values.ToList();
        }

        public void AddUser(User user)
        {
            users.Add(users.Count+1, user);
        }

        public void AddOffer(int id, int offerID)
        {
            users[id].Inquiries.Add(offerID);
        }
    }
}