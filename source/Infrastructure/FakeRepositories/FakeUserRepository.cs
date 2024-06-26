using Domain.Model;
using Domain.Abstractions;
using Microsoft.AspNetCore.JsonPatch;

namespace Infrastructure.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        private IDictionary<int, User> users;

        public FakeUserRepository()
        {
            Address address = new() { Street = "Prosta", City = "Warsaw", FlatNumber = "1", PostalCode = "13-337", StreetNumber = "2" };
            var users = new List<User>
            {
                new() { Id = 1, FirstName = "John", LastName = "Smith", Address=new Address(address), CompanyName="ABC", Email="a@gmail.com", DefaultSourceAddress=new Address(address), 
                    Auth0Id="1"},
                new() { Id = 2, FirstName = "Bob", LastName = "Smith", Address=new Address(address), CompanyName="ABC", Email="a@gmail.com", DefaultSourceAddress=new Address(address), 
                    Auth0Id="2"},
                new() { Id = 3, FirstName = "Ann", LastName = "Smith", Address=address, CompanyName="ABC", Email="a@gmail.com", DefaultSourceAddress=new Address(address), Auth0Id="3" }
            };
            this.users = users.ToDictionary(p => p.Id);
        }


        public List<User> GetAll()
        {
            return users.Values.ToList();
        }

        public void AddUser(User user)
        {
            users.Add(users.Count + 1, user);
        }

        public void AddRequest(string userID, Request request)
        {
            foreach (User user in users.Values)
            {
                if (user.Auth0Id == userID)
                {
                    user.Requests.Add(request);
                    return;
                }
            }
        }

        public void Update(User user)
        {
            users[user.Id] = user;
        }

        public User? GetById(string Auth0Id)
        {
            foreach(User user in users.Values) 
                if(user.Auth0Id==Auth0Id)
                    return user;
            return null;
        }
    }
}