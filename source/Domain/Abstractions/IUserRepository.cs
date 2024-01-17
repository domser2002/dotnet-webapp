using Domain.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetById(string Auth0Id);
        void AddUser(User user);
        void AddRequest(string userID, Request request);
        void Update(User user);
    }
}